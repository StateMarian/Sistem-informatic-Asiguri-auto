using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dapper;
using System.Data.SqlClient;


namespace Sistem_informatic_Asiguri_auto
{
    public partial class FormIndicatoriGrupaCapacitate : Form
    {
        public FormIndicatoriGrupaCapacitate()
        {
            InitializeComponent();
            Denumire_Indicator();
            AddCategoriiToListBox();
            AddCapacitatiToListbox();
            AddGrupaToCombo();
            AddIndicatorToCombo();
            this.FormBorderStyle = FormBorderStyle.None;
        }

     
        List<Categorii> listaCategorii = DatabaseAcces.ExtrageCategorii().Where(d => d.status_categorie == true).ToList();
        List<Subcategorii> listasubcategorii = DatabaseAcces.ExtrageSubcategorii().Where(d => d.status_subcategorie == true).ToList();
        List<CapacitateCilindrica> listaCapacitati = DatabaseAcces.ExtrageCapacitate().Where(d => d.status_capacitate == true).ToList();
        List<GrupeVarsta> listaGrupa = DatabaseAcces.ExtrageGrupe().Where(d => d.status_grupa == true).ToList();
        List<IndicatoriSuplimentari> listaIndicatori = DatabaseAcces.ExtrageIndicatoriSuplimentari().Where(d => d.status_indicator == true).ToList();
        List<IndicatoriGrupaCapacitate> listaIndicatoriCapacitate = DatabaseAcces.ExtrageIndicatoriDupaGrupaCapacitate().Where(d=>d.status==true).ToList();
        void AddCategoriiToListBox()
        {
            comboBoxCategorii.DataSource = null;
            comboBoxCategorii.DataSource = listaCategorii;
            comboBoxCategorii.DisplayMember = "Denumire_categorie";
        }

        void AddGrupaToCombo()
        {
            comboBoxGrupa.DataSource = null;
            comboBoxGrupa.Sorted = true;
            comboBoxGrupa.DataSource = listaGrupa;
            comboBoxGrupa.DisplayMember = "StringLista";
        }
        void AddIndicatorToCombo()
        {
            comboBoxIndicatori.DataSource = null;
            comboBoxIndicatori.Sorted = true;
            comboBoxIndicatori.DataSource = listaIndicatori;
            comboBoxIndicatori.DisplayMember = "Denumire_indicator";
        }

        void AddCapacitatiToListbox()
        {
            comboBoxCapacitati.DataSource = null;
            comboBoxCapacitati.DataSource = listaCapacitati;
            comboBoxCapacitati.DisplayMember = "FullCap";
        }
        void AddSubcategoriiToListBox()
        {
            comboBoxSubcategorii.DataSource = null;
            var listaSubcategoriiFiltrata = listasubcategorii
                .Where(s => s.Id_categorie == ((Categorii)comboBoxCategorii.SelectedItem).Id_categorie)
                .Select(d => d.Denumire_Subcategorie).ToList()
                .Distinct().ToList();
            comboBoxSubcategorii.DataSource = listaSubcategoriiFiltrata;
            comboBoxSubcategorii.DisplayMember = "Denumire_Subcategorie";
        }
        void AddNumarLocuriComboBox()
        {
            var subcategorieSelectata = comboBoxSubcategorii.SelectedItem as string;
            if (comboBoxSubcategorii.SelectedItem != null)
            {
                comboBoxNumarLocuri.Items.Clear();
                comboBoxNumarLocuri.Sorted = true;
                foreach (Subcategorii sub in listasubcategorii)
                {
                    if (sub.Denumire_Subcategorie == subcategorieSelectata)
                    {
                        var nr_locuri = sub.Numar_locuri;
                        if (!comboBoxNumarLocuri.Items.Contains(nr_locuri))
                        {
                            comboBoxNumarLocuri.Items.Add(nr_locuri);
                        }
                    }
                }
                comboBoxNumarLocuri.SelectedIndex = 0;
            }
        }
        void AddGreutateMinimaComboBox()
        {
            var nr_locuri = comboBoxNumarLocuri.SelectedItem as string;
            var subcategorieSelectata = comboBoxSubcategorii.SelectedItem as string;
            if (comboBoxNumarLocuri.SelectedItem != null && comboBoxSubcategorii.SelectedItem != null)
            {
                comboBoxGreutateMinima.Items.Clear();
                foreach (Subcategorii sub in listasubcategorii)
                {
                    if (sub.Numar_locuri == nr_locuri && sub.Denumire_Subcategorie == subcategorieSelectata)
                    {
                        var greutate_min = sub.Masa_min;
                        comboBoxGreutateMinima.Items.Add(greutate_min);
                    }
                }
                comboBoxGreutateMinima.SelectedIndex = 0;
            }
        }
        void AddGreutateMaximaComboBox()
        {
            var nr_locuri = comboBoxNumarLocuri.SelectedItem as string;
            var subcategorieSelectata = comboBoxSubcategorii.SelectedItem as string;
            var greutateMinima = comboBoxGreutateMinima.SelectedItem as string;
            if (comboBoxNumarLocuri.SelectedItem != null && comboBoxSubcategorii.SelectedItem != null && comboBoxGreutateMinima.SelectedItem!=null)
            {
                comboBoxGreutateMaxima.Items.Clear();
                foreach (Subcategorii sub in listasubcategorii)
                {
                    if (sub.Numar_locuri == nr_locuri && sub.Denumire_Subcategorie == subcategorieSelectata && sub.Masa_min==greutateMinima)
                    {
                        var greutate_max = sub.Masa_max;
                        comboBoxGreutateMaxima.Items.Add(greutate_max);
                    }
                }
                comboBoxGreutateMaxima.SelectedIndex = 0;
            }
        }
        int Cod_subcategorie()
        {
            int cod_subcat = 0;
            string den_sub = Convert.ToString(comboBoxSubcategorii.SelectedItem);
            string locuri = Convert.ToString(comboBoxNumarLocuri.SelectedItem);
            string masamin = Convert.ToString(comboBoxGreutateMinima.SelectedItem);
            string masamax = Convert.ToString(comboBoxGreutateMaxima.SelectedItem);
            foreach (Subcategorii sub in listasubcategorii)
            {
                if (sub.Denumire_Subcategorie == den_sub && sub.Numar_locuri == locuri && sub.Masa_min == masamin && sub.Masa_max == masamax)
                {
                    cod_subcat = sub.Id_subcategorie;
                }
            }
            return cod_subcat;
        }

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }
        void Denumire_Indicator()
        {
            foreach (IndicatoriGrupaCapacitate indic in listaIndicatoriCapacitate)
            {
                IndicatoriSuplimentari indicatorAsociat = listaIndicatori.FirstOrDefault(ind => ind.Id_indicatori == indic.Id_indicatori);

                if (indicatorAsociat != null)
                {
                    indic.denumire_indicator = indicatorAsociat.Denumire_indicator;
                }
            }
        }
        int cod_cap()
        {
            int cod_cap = 0;
            if(comboBoxCapacitati.SelectedItem!=null)
            {
                cod_cap = ((CapacitateCilindrica)comboBoxCapacitati.SelectedItem).Id_capacitate;
            }
            return cod_cap;
        }
        int cod_grupa ()
        {
            int cod_grupa = 0;
            if (comboBoxGrupa.SelectedItem != null)
            {
                cod_grupa = ((GrupeVarsta)comboBoxGrupa.SelectedItem).Id_grupa;
            }
            return cod_grupa;
        }

        void AddToDataGrid()
        {
            dataGridViewDate.DataSource = null;
            var listaFiltrata = listaIndicatoriCapacitate
                .Where(d => d.Id_subcategorie == Cod_subcategorie())
                .Where(d => d.Id_capacitate == cod_cap())
                .Where(d=>d.Id_grupa== cod_grupa())
                .ToList();
            int cod = cod_cap();
            dataGridViewDate.DataSource = listaFiltrata;
            dataGridViewDate.Columns[0].Visible = false;
            dataGridViewDate.Columns[1].Visible = false;
            dataGridViewDate.Columns[2].Visible = false;
            dataGridViewDate.Columns[3].Visible = false;
            dataGridViewDate.Columns[4].Visible = false;
            dataGridViewDate.Columns[5].HeaderText = "Denumire indicator";
            dataGridViewDate.Columns[7].Visible = false;
            dataGridViewDate.Columns[6].HeaderText = "Procent indicator";
            dataGridViewDate.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDate.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewDate.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewDate.RowHeadersWidth = 4;
            dataGridViewDate.DefaultCellStyle.Font = new Font("Times new roman", 16);
        }

        bool verificaExistentaIndicator()
        {
            int grupa = ((GrupeVarsta)comboBoxGrupa.SelectedItem).Id_grupa;
            int capacitate = ((CapacitateCilindrica)comboBoxCapacitati.SelectedItem).Id_capacitate;
            int indicator = ((IndicatoriSuplimentari)comboBoxIndicatori.SelectedItem).Id_indicatori;
            foreach(IndicatoriGrupaCapacitate ind in listaIndicatoriCapacitate)
            {
                if(ind.Id_subcategorie==Cod_subcategorie() && ind.Id_grupa==grupa && ind.Id_capacitate==capacitate && ind.Id_indicatori==indicator)
                {
                    return false;
                }
            }
            return true;
        }
        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            int id_ind_grupa = 1;
            List<IndicatoriGrupaCapacitate> listaFullIndicatoriGrupa= DatabaseAcces.ExtrageIndicatoriDupaGrupaCapacitate();
            if (listaFullIndicatoriGrupa.Count > 0)
            {
                id_ind_grupa = listaFullIndicatoriGrupa.Max(d => d.Cod_Indicatori_PrimaryKey) + 1;
            }
            if(comboBoxCategorii.SelectedItem == null)
            {
                MessageBox.Show("Va rog selecta-ti o categorie!");
            }
            else
            {
                if(comboBoxSubcategorii.SelectedItem==null)
                {
                    MessageBox.Show("Va rog selecta-ti o subcategorie!");
                }
                else
                {
                    if (comboBoxCapacitati.SelectedItem == null)
                    {
                        MessageBox.Show("Va rog selecta-ti capacitatea sau puterea!");
                    }
                    else
                    {
                        if (comboBoxGrupa.SelectedItem == null)
                        {
                            MessageBox.Show("Va rog selecta-ti grupa de varsta!");
                        }
                        else
                        {
                            if (!verificaExistentaIndicator())
                            {
                                MessageBox.Show("Indicatorul a fost adaugat pentru parametri selectati, " +
                                    "daca doriti sa schimbati valoarea va trebuie sa il stergeti intai!");
                            }
                            else
                            {
                                DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati valoarea indicatorului", "Confirmare", MessageBoxButtons.YesNo);
                                if (dialog == DialogResult.Yes)
                                {
                                    IndicatoriGrupaCapacitate ind = new IndicatoriGrupaCapacitate()
                                    {
                                        Cod_Indicatori_PrimaryKey = id_ind_grupa,
                                        Id_subcategorie = Cod_subcategorie(),
                                        Id_capacitate = ((CapacitateCilindrica)comboBoxCapacitati.SelectedItem).Id_capacitate,
                                        Id_grupa = ((GrupeVarsta)comboBoxGrupa.SelectedItem).Id_grupa,
                                        Id_indicatori = ((IndicatoriSuplimentari)comboBoxIndicatori.SelectedItem).Id_indicatori,
                                        Procent_indicator = Convert.ToSingle(numericUpDownProcent.Value),
                                        status = true
                                    };
                                    listaIndicatoriCapacitate.Add(ind);
                                    DatabaseAcces.AdaugaIndicatoriCapacitate(ind);
                                    Denumire_Indicator();
                                    AddToDataGrid();
                                    numericUpDownProcent.Value = Convert.ToDecimal(1.0);
                                    MessageBox.Show("Valoarea indicatorului a fost adaugata cu succes!");
                                }
                                else
                                {
                                    numericUpDownProcent.Value = Convert.ToDecimal(1.0);
                                    MessageBox.Show("Adaugare anulata!");
                                }
                            }
                        }
                    }
                }
            }         
        }

        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (dataGridViewDate.CurrentRow != null)
            {
                int cod = Convert.ToInt32(dataGridViewDate.CurrentRow.Cells[0].Value);
                bool status = false;
                DialogResult dialog = MessageBox.Show("Sigur doriti sa stergeti valoarea indicatorului", "Confirmare", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    DatabaseAcces.ModificaStatusValoareIndicatori(cod, status);
                    listaIndicatoriCapacitate = DatabaseAcces.ExtrageIndicatoriDupaGrupaCapacitate().Where(d => d.status == true).ToList();
                    Denumire_Indicator();
                    AddToDataGrid();
                    dataGridViewDate.AutoResizeColumns();
                    comboBoxGrupa.SelectedIndex = 0;
                    comboBoxCapacitati.SelectedIndex = 0;
                    comboBoxSubcategorii.SelectedIndex = 0;
                    MessageBox.Show("Stergere realizata cu succes!");
                }
                else
                {
                    MessageBox.Show("Stergere anulata");
                    comboBoxGrupa.SelectedIndex = 0;
                    comboBoxCapacitati.SelectedIndex = 0;
                    comboBoxSubcategorii.SelectedIndex = 0;
                }
            }
            else
            {
                MessageBox.Show("Trebuie sa selectati un indicator inainte de a-l sterge!!");
            }
        }

        private void comboBoxCategorii_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddSubcategoriiToListBox();
        }

        private void comboBoxSubcategorii_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddNumarLocuriComboBox();
            AddToDataGrid();
        }

        private void comboBoxNumarLocuri_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddGreutateMinimaComboBox();
            AddToDataGrid();
        }

        private void comboBoxGreutateMinima_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddGreutateMaximaComboBox();
            AddToDataGrid();
        }

        private void comboBoxCapacitati_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddToDataGrid();
        }

        private void comboBoxGrupa_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddToDataGrid();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        bool mousedown;
        private void panelBar_MouseDown(object sender, MouseEventArgs e)
        {
            mousedown = true;
        }

        private void panelBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousedown)
            {
                int mousex = MousePosition.X - 512;
                int mousey = MousePosition.Y - 20;
                this.SetDesktopLocation(mousex, mousey);
            }
        }

        private void panelBar_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }
    }
}
