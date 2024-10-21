using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sistem_informatic_Asiguri_auto
{
    public partial class FormAdaugaCASCO : Form
    {
        public FormAdaugaCASCO()
        {
            InitializeComponent();
            AddMarcaCombobox();
            AddDurataToComboBox();
            AddCategoriiListBox();
            AddCapacitatiListBox();
            AddTipCascoToListBox();
            AddGrupeVarstaCombobox();
            AddZonaGeograficaToCombo();
            AddDomeniiUtilizareCombobox();
            AddToDataGrid();   
        }

        List<Categorii> listaCategorii = DatabaseAcces.ExtrageCategoriisiSubcategorii().Where(d => d.status_categorie == true).ToList();
        List<CapacitateCilindrica> listaCapacitati = DatabaseAcces.ExtrageCapacitate().Where(d => d.status_capacitate == true).ToList();
        List<Tip_casco> listaCasco = DatabaseAcces.ExtrageTipCasco().Where(d => d.status_tipCasco == true).ToList();
        List<GrupeVarsta> listaGrupeVarsta = DatabaseAcces.ExtrageGrupe().Where(d => d.status_grupa == true).ToList();
        List<ZonaGeografica> listaZoneGeografice = DatabaseAcces.ExtrageZoneGeografice().Where(d => d.status_zona == true).ToList();
        List<DomeniiUtilizare> listaDomeniiUtilizare = DatabaseAcces.ExtrageDomeniiUtilizare().Where(d => d.status_domeniu == true).ToList();
        List<Marca> listaMarca = DatabaseAcces.ExtrageMarcasiModele().Where(d => d.status_marca == true).ToList();
        List<DurataAsigurare> listaDurate = DatabaseAcces.ExtrageDurataAsigurare().Where(d => d.status_durata == true).ToList();
        List<Subcategorii> listaSubcategorii = DatabaseAcces.ExtrageSubcategorii().Where(d => d.status_subcategorie == true).ToList();
        List<Model> listaModele = DatabaseAcces.ExtrageModel().Where(d => d.status_model == true).ToList();
        List<Calcul_prima_Casco> listaAsigurariCasco = DatabaseAcces.ExtrageAsigurareCasco().Where(d => d.status_casco == true).ToList();
        List<Fransiza> listaFransiza = DatabaseAcces.ExtrageFransiza().Where(d => d.status_fransiza == true).ToList();

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }
        void AddDurataToComboBox()
        {
            comboBoxDurata.DataSource = null;
            comboBoxDurata.Sorted = true;
            var listaFiltrata = listaDurate
                .Where(d => d.Tip_asigurare == "CASCO")
                .ToList();
            comboBoxDurata.DataSource = listaFiltrata;
            comboBoxDurata.DisplayMember = "Durata";
        }
        void AddCategoriiListBox()
        {
            comboBoxCategorii.DataSource = null;
            comboBoxCategorii.DataSource = listaCategorii;
            comboBoxCategorii.DisplayMember = "Denumire_categorie";
        }

        void AddSubcategoriiToComboBox()
        {
            comboBoxDenSubcategorie.DataSource = null;
            var listaSubcategoriiFiltrata = listaSubcategorii
                .Where(s => s.Id_categorie == ((Categorii)comboBoxCategorii.SelectedItem).Id_categorie)
                .Select(d => d.Denumire_Subcategorie).ToList()
                .Distinct().ToList();
            comboBoxDenSubcategorie.DataSource = listaSubcategoriiFiltrata;
            comboBoxDenSubcategorie.DisplayMember = "Denumire_Subcategorie";
        }
        void AddNumarLocuriComboBox()
        {
            var subcategorieSelectata = comboBoxDenSubcategorie.SelectedItem as string;
            if (comboBoxDenSubcategorie.SelectedItem != null)
            {
                comboBoxNumarLocuri.Items.Clear();
                comboBoxNumarLocuri.Sorted = true;
                foreach (Subcategorii sub in listaSubcategorii)
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
        int Cod_subcategorie()
        {
            int cod_subcat = 0;
            string den_sub = Convert.ToString(comboBoxDenSubcategorie.SelectedItem);
            string locuri = Convert.ToString(comboBoxNumarLocuri.SelectedItem);
            string masamin = Convert.ToString(comboBoxGreutateMinima.SelectedItem);
            string masamax = Convert.ToString(comboBoxGreutateMaxima.SelectedItem);
            foreach (Subcategorii sub in listaSubcategorii)
            {
                if (sub.Denumire_Subcategorie == den_sub && sub.Numar_locuri == locuri && sub.Masa_min == masamin && sub.Masa_max == masamax)
                {
                    cod_subcat = sub.Id_subcategorie;
                }
            }
            return cod_subcat;
        }
        void AddGreutateMinimaComboBox()
        {
            var nr_locuri = comboBoxNumarLocuri.SelectedItem as string;
            var subcategorieSelectata = comboBoxDenSubcategorie.SelectedItem as string;
            if (comboBoxNumarLocuri.SelectedItem != null && comboBoxDenSubcategorie.SelectedItem != null)
            {
                comboBoxGreutateMinima.Items.Clear();
                foreach (Subcategorii sub in listaSubcategorii)
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
            var subcategorieSelectata = comboBoxDenSubcategorie.SelectedItem as string;
            var greutateMinima = comboBoxGreutateMinima.SelectedItem as string;
            if (comboBoxNumarLocuri.SelectedItem != null && comboBoxDenSubcategorie.SelectedItem != null && comboBoxGreutateMinima.SelectedItem != null)
            {
                comboBoxGreutateMaxima.Items.Clear();
                foreach (Subcategorii sub in listaSubcategorii)
                {
                    if (sub.Numar_locuri == nr_locuri && sub.Denumire_Subcategorie == subcategorieSelectata && sub.Masa_min == greutateMinima)
                    {
                        var greutate_max = sub.Masa_max;
                        comboBoxGreutateMaxima.Items.Add(greutate_max);
                    }
                }
                comboBoxGreutateMaxima.SelectedIndex = 0;
            }
        }
        void AddCapacitatiListBox()
        {
            comboBoxCapacitati.DataSource = null;
            comboBoxCapacitati.DataSource = listaCapacitati;
            comboBoxCapacitati.DisplayMember = "FullCap";
        }

        void AddTipCascoToListBox()
        {
            comboBoxPachetCasco.DataSource = null;
            var listaCascoFiltrata = listaCasco
                .Select(d => d.Denumire_casco)
                .Distinct()
                .ToList();
            listaCascoFiltrata.Sort();
            comboBoxPachetCasco.DataSource = listaCascoFiltrata;
            comboBoxPachetCasco.DisplayMember = "Denumire_casco";
        }

        void AddDenFransiza()
        {
            List<Fransiza> listaFran = DatabaseAcces.ExtrageFransizaDupaCasco(comboBoxPachetCasco.Text);
            comboBoxFransiza.DataSource = null;
            var listaFransiza = listaFran
                .Select(d => d.Tip_fransiza)
                .Distinct()
                .ToList();
            listaFransiza.Sort();
            comboBoxFransiza.DataSource = listaFransiza;
            comboBoxFransiza.DisplayMember = "Tip_Fransiza";
        }
        void AddProcentFransiza()
        {
            var tip_fransiza = comboBoxFransiza.Text as string;
            List<Fransiza> listaFran = DatabaseAcces.ExtrageFransizaDupaCasco(comboBoxPachetCasco.Text);
            comboBoxProcent.Items.Clear();
            if (comboBoxFransiza.SelectedItem != null)
            {
                foreach (Fransiza fran in listaFran)
                {
                    if (fran.Tip_fransiza == tip_fransiza)
                    {
                        var procentFran = fran.Procent;
                        string procent = Convert.ToString(procentFran);
                        comboBoxProcent.Items.Add(procent);
                        comboBoxProcent.SelectedIndex = 0;
                    }
                }
            }
        }
        void AddGrupeVarstaCombobox()
        {
            comboBoxGrupeVarsta.DataSource = null;
            comboBoxGrupeVarsta.Sorted = true;
            comboBoxGrupeVarsta.DataSource = listaGrupeVarsta;
            comboBoxGrupeVarsta.DisplayMember = "StringLista";
        }
        void AddZonaGeograficaToCombo()
        {
            comboBoxZona.DataSource = null;
            comboBoxZona.Sorted = true;
            var listaZone = listaZoneGeografice
                .Where(d => d.Tip_Asigurare == "CASCO")
                .ToList();
            comboBoxZona.DataSource = listaZone;
            comboBoxZona.DisplayMember = "Judet";
        }
        void AddDomeniiUtilizareCombobox()
        {
            comboBoxUtilizare.DataSource = null;
            comboBoxUtilizare.Sorted = true;
            comboBoxUtilizare.DataSource = listaDomeniiUtilizare;
            comboBoxUtilizare.DisplayMember = "Domeniu";
        }
        void AddMarcaCombobox()
        {
            comboBoxMarca.DataSource = null;
            comboBoxMarca.DataSource = listaMarca;
            comboBoxMarca.DisplayMember = "denumire_marca";
        }
        void AddModelToCombo()
        {
            comboBoxModel.DataSource = null;
            var listaFiltrata = listaModele
                .Where(d => d.Id_marca == ((Marca)comboBoxMarca.SelectedItem).Id_marca)
                .Select(d => d.Denumire_model)
                .Distinct()
                .ToList();
            comboBoxModel.DataSource = listaFiltrata;
            comboBoxModel.DisplayMember = "Denumire_model";
        }
        void AddVariantaToCombo()
        {
            var modelSelectat = comboBoxModel.SelectedItem as string;
            List<string> listaMod = new List<string>();
            if (comboBoxModel.SelectedItem != null)
            {
                comboBoxVarianta.DataSource = null;
                foreach (Model mod in listaModele)
                {
                    if (mod.Denumire_model == modelSelectat)
                    {
                        var varianta = mod.Varianta;
                        listaMod.Add(varianta);
                    }
                }
                listaMod.Sort();
                comboBoxVarianta.DataSource = listaMod;
            }
        }
        void AddTipAuto()
        {
            var modelSelectat = comboBoxModel.SelectedItem as string;
            var variantaSelectata = comboBoxVarianta.SelectedItem as string;
            comboBoxTiAuto.Items.Clear();
            foreach (Model mod in listaModele)
            {
                if (mod.Denumire_model == modelSelectat && mod.Varianta == variantaSelectata)
                {
                    comboBoxTiAuto.Items.Add(mod.Tip_auto);
                    comboBoxTiAuto.SelectedIndex = 0;
                }
            }
        }

        int cod_model()
        {
            int cod_model=0;
            int cod_marca = ((Marca)comboBoxMarca.SelectedItem).Id_marca;
            var modelSelectat = comboBoxModel.SelectedItem as string;
            var variantaSelectata = comboBoxVarianta.SelectedItem as string;
            foreach(Model mod in listaModele)
            {
                if(mod.Denumire_model==modelSelectat && mod.Varianta==variantaSelectata && mod.Id_marca==cod_marca)
                {
                    cod_model = mod.Id_model;
                }
            }
            return cod_model;
        }
        int cod_Casco()
        {
            int cod_casco = 0;
            string denFran = comboBoxFransiza.Text as string;
            string denCasc = comboBoxPachetCasco.Text as string;
            int procent = Convert.ToInt32(comboBoxProcent.Text);
            int cod_fran = 0;
            foreach (Fransiza fran in listaFransiza)
            {
                if (fran.Tip_fransiza == denFran && fran.Procent == procent)
                {
                    cod_fran = fran.Id_fransiza;
                }
            }
            foreach(Tip_casco casc in listaCasco)
            {
                if(casc.Denumire_casco==denCasc && casc.Id_fransiza==cod_fran)
                {
                    cod_casco = casc.Id_casco;
                }
            }
            return cod_casco;
        }
        int cod_cap()
        {
            int cod_cap = 0;
            if (comboBoxCapacitati.SelectedItem != null)
            {
                cod_cap = ((CapacitateCilindrica)comboBoxCapacitati.SelectedItem).Id_capacitate;
            }
            return cod_cap;
        }
        int cod_grupa()
        {
            int cod_grupa = 0;
            if (comboBoxGrupeVarsta.SelectedItem != null)
            {
                cod_grupa = ((GrupeVarsta)comboBoxGrupeVarsta.SelectedItem).Id_grupa;
            }
            return cod_grupa;
        }
        void AddToDataGrid()
        {
            dataGridViewCasco.DataSource = null;
            var listaFiltrata = listaAsigurariCasco
                .Where(d => d.Id_subcategorie == Cod_subcategorie())
                .Where(d => d.Id_grupa == cod_grupa())
                .Where(d => d.Id_capacitate == cod_cap())
                .Where(d => d.Id_casco == cod_Casco())
                .Where(d => d.Id_model == cod_model())
                .ToList();
            dataGridViewCasco.DataSource = listaFiltrata;
            dataGridViewCasco.Columns[0].Visible = false;
            dataGridViewCasco.Columns[1].Visible = false;
            dataGridViewCasco.Columns[2].Visible = false;
            dataGridViewCasco.Columns[3].Visible = false;
            dataGridViewCasco.Columns[4].Visible = false;
            dataGridViewCasco.Columns[5].Visible = false;
            dataGridViewCasco.Columns[6].Visible = false;
            dataGridViewCasco.Columns[7].Visible = false;
            dataGridViewCasco.Columns[8].Visible = false;
            dataGridViewCasco.Columns[9].HeaderText = "Valoare prima casco(lei)";
            dataGridViewCasco.Columns[10].Visible = false;
            dataGridViewCasco.Columns[11].Visible = false;
            dataGridViewCasco.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCasco.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewCasco.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCasco.RowHeadersWidth = 4;
            dataGridViewCasco.DefaultCellStyle.Font = new Font("Times new roman", 16);
        }

        private void comboBoxMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddModelToCombo();
            AddTipAuto();
        }
        bool checkExist()
        {
            foreach(Calcul_prima_Casco casc in listaAsigurariCasco)
            {
                if(casc.Id_subcategorie==Cod_subcategorie() && casc.Id_grupa==cod_grupa() && casc.Id_capacitate==cod_cap() && casc.Id_casco==cod_Casco() && casc.Id_model==cod_model())
                {
                    return false;
                }
            }
            return true;
        }
        private void buttonAdauga_Click(object sender, EventArgs e)
        {

            if (!Verificari.checkCnp(textBoxValoareCasco.Text) || string.IsNullOrEmpty(textBoxValoareCasco.Text))
            {
                MessageBox.Show("Va rog introduce-ti valoarea casco in formatul corespunzator, acesta nu poate fi gol!");
            }
            else
            {
                if (!checkExist())
                {
                    MessageBox.Show("Valoarea primei Casco pe care doriti sa o adaugati pentru parametri selectati exista deja pentru anul in curs,contactati managerul pentru modificarea acesteia!");
                }
                else
                {
                    int id_asigurare_casco = 1;
                    List<Calcul_prima_Casco> listaFullCasco = DatabaseAcces.ExtrageAsigurareCasco();
                    if (listaFullCasco.Count > 0)
                    {
                        id_asigurare_casco = listaFullCasco.Max(d => d.Calcul_Casco_PrimaryKey) + 1;
                    }
                    DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati Asigurarea Casco", "Confirmare", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        Calcul_prima_Casco casc = new Calcul_prima_Casco()
                        {
                            Calcul_Casco_PrimaryKey = id_asigurare_casco,
                            Id_subcategorie = Cod_subcategorie(),
                            Id_grupa = ((GrupeVarsta)comboBoxGrupeVarsta.SelectedItem).Id_grupa,
                            Id_capacitate = ((CapacitateCilindrica)comboBoxCapacitati.SelectedItem).Id_capacitate,
                            Id_durata = ((DurataAsigurare)comboBoxDurata.SelectedItem).Id_durata,
                            Id_zona = ((ZonaGeografica)comboBoxZona.SelectedItem).Id_zona,
                            Id_utilizare = ((DomeniiUtilizare)comboBoxUtilizare.SelectedItem).Id_utilizare,
                            Id_model = cod_model(),
                            Id_casco = cod_Casco(),
                            Valoare_prima_casco = Convert.ToSingle(textBoxValoareCasco.Text),
                            status_casco = true,
                            Data_adaugare = Convert.ToString(DateTime.Now)
                        };
                        DatabaseAcces.AdaugaCasco(casc);
                        listaAsigurariCasco.Add(casc);
                        AddToDataGrid();
                        textBoxValoareCasco.Clear();
                        MessageBox.Show("Asigurare casco adaugata cu succes!");
                    }
                    else
                    {
                        MessageBox.Show("Adaugare anulata!");
                        AddToDataGrid();
                        textBoxValoareCasco.Clear();
                    }
                }
            }


        }

        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (dataGridViewCasco.CurrentRow != null)
            {
                int cod = Convert.ToInt32(dataGridViewCasco.CurrentRow.Cells[0].Value);
                bool status = false;
                DialogResult dialog = MessageBox.Show("Sigur doriti sa stergeti valoarea asigurarea casco", "Confirmare", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    DatabaseAcces.ModificaStatusAsigurareCasco(cod, status);
                    listaAsigurariCasco = DatabaseAcces.ExtrageAsigurareCasco().Where(d => d.status_casco == true).ToList();
                    AddToDataGrid();
                    dataGridViewCasco.AutoResizeColumns();
                    MessageBox.Show("Stergere realizata cu succes!");
                }
                else
                {
                    MessageBox.Show("Stergere anulata");
                }
            }
            else
            {
                MessageBox.Show("Trebuie sa selecta-ti o asigurare casco inainte de a o sterge!!");
            }
        }

        private void comboBoxCategorii_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddSubcategoriiToComboBox();
        }

        private void comboBoxDenSubcategorie_SelectedIndexChanged(object sender, EventArgs e)
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
        private void comboBoxPachetCasco_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddDenFransiza();
            AddToDataGrid();
        }
        private void comboBoxFransiza_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddProcentFransiza();
        }

        private void comboBoxModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddVariantaToCombo();
            AddTipAuto();
            AddToDataGrid();
        }

        private void comboBoxVarianta_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddTipAuto();
            AddToDataGrid();
        }

        private void comboBoxGrupeVarsta_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddToDataGrid();
        }

        private void comboBoxCapacitati_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddToDataGrid();
        }
    }
}
