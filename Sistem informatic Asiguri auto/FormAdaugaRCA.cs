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
    public partial class FormAdaugaRCA : Form
    {
        public FormAdaugaRCA()
        {
            InitializeComponent();
            AddCategoriiListBox();
            AddGrupeToCombo();
            AddCapacitatiComboBox();
            AddBeneficiiListBox();   
            AddBonusMalusToCombo();
            AddDurataToComboBox();
            AddZonaGeograficaToCombo();
            AddDomeniiUtilizareToCombo();
            AddDiscountToCombo();
            AddToDataGrid();
        }

        List<Categorii> listaCategorii = DatabaseAcces.ExtrageCategoriisiSubcategorii().Where(d => d.status_categorie == true).ToList();
        List<CapacitateCilindrica> listaCapacitati = DatabaseAcces.ExtrageCapacitate().Where(d => d.status_capacitate == true).ToList();
        List<GrupeVarsta> listaGrupeVarsta = DatabaseAcces.ExtrageGrupe().Where(d => d.status_grupa == true).ToList();
        List<Bonus_Malus_Class> listaBonusMalus = DatabaseAcces.ExtrageClaseBonusMalus().Where(d => d.status_bonus == true).ToList();
        List<ZonaGeografica> listaZoneGeografice = DatabaseAcces.ExtrageZoneGeografice().Where(d => d.status_zona == true).ToList();
        List<DomeniiUtilizare> listaDomeniiUtilizare = DatabaseAcces.ExtrageDomeniiUtilizare().Where(d => d.status_domeniu == true).ToList();
        List<Discount_RCA> listaDiscRca = DatabaseAcces.ExtrageDiscount().Where(d => d.status_discount == true).ToList();
        List<BeneficiiSuplimentare> listaBeneficii = DatabaseAcces.ExtrageBeneficii().Where(d => d.status_beneficiu == true).ToList();
        List<Subcategorii> listaSubcategorii = DatabaseAcces.ExtrageSubcategorii().Where(d => d.status_subcategorie == true).ToList();
        List<DurataAsigurare> listaDurate = DatabaseAcces.ExtrageDurataAsigurare().Where(d => d.status_durata == true).ToList();
        List<Calcul_prima_RCA> listaAsigurari = DatabaseAcces.ExtrageAsigurareRCA().Where(d => d.status_asigurare == true).ToList();
        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
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

        void AddCapacitatiComboBox()
        {
            comboBoxCapacitati.DataSource = null;
            comboBoxCapacitati.DataSource = listaCapacitati;
            comboBoxCapacitati.DisplayMember = "FullCap";
        }
        void AddBeneficiiListBox()
        {
            comboBoxBeneficiiSuplimentare.DataSource = null;
            comboBoxBeneficiiSuplimentare.Sorted = true;
            comboBoxBeneficiiSuplimentare.DataSource = listaBeneficii;
            comboBoxBeneficiiSuplimentare.DisplayMember = "Beneficiu";
        }


        void AddGrupeToCombo()
        {
            comboBoxGrupaVarsta.DataSource = null;
            comboBoxGrupaVarsta.Sorted = true;
            comboBoxGrupaVarsta.DataSource = listaGrupeVarsta;
            comboBoxGrupaVarsta.DisplayMember = "StringLista";
        }
        void AddBonusMalusToCombo()
        {
            comboBoxBonusMalus.DataSource = null;
            comboBoxBonusMalus.Sorted = true;
            comboBoxBonusMalus.DataSource = listaBonusMalus;
            comboBoxBonusMalus.DisplayMember = "FullClass";
        }
        void AddZonaGeograficaToCombo()
        {
            comboBoxZonaGeografica.DataSource = null;
            comboBoxZonaGeografica.Sorted = true;
            var listaZone = listaZoneGeografice
                .Where(d => d.Tip_Asigurare == "RCA")
                .ToList();
            comboBoxZonaGeografica.DataSource = listaZone;
            comboBoxZonaGeografica.DisplayMember = "Judet";
        }
        void AddDomeniiUtilizareToCombo()
        {
            comboBoxUtilizare.DataSource = null;
            comboBoxUtilizare.Sorted = true;
            comboBoxUtilizare.DataSource = listaDomeniiUtilizare;
            comboBoxUtilizare.DisplayMember = "Domeniu";
        }
        void AddDiscountToCombo()
        {
            comboBoxDiscount.DataSource = null;
            comboBoxDiscount.Sorted = true;
            comboBoxDiscount.DataSource = listaDiscRca;
            comboBoxDiscount.DisplayMember = "Discount";
        }
        void AddDurataToComboBox()
        {
            comboBoxDurata.DataSource = null;
            comboBoxDurata.Sorted = true;
            var listaFiltrata = listaDurate
                .Where(d => d.Tip_asigurare == "RCA")
                .ToList();
            comboBoxDurata.DataSource = listaFiltrata;
            comboBoxDurata.DisplayMember = "Durata";
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
            int cod_grupa= 0;
            if (comboBoxGrupaVarsta.SelectedItem != null)
            {
                cod_grupa = ((GrupeVarsta)comboBoxGrupaVarsta.SelectedItem).Id_grupa;
            }
            return cod_grupa;
        }
        bool checkExist()
        {
            foreach(Calcul_prima_RCA rca in listaAsigurari)
            {
                if(rca.Id_subcategorie==Cod_subcategorie() && rca.Id_grupa==cod_grupa() && rca.Id_capacitate==cod_cap())
                {
                    return false;
                }
            }
            return true;
        }
        void AddToDataGrid()
        {
            dataGridViewAsigurariCreate.DataSource = null;
            var listaFiltrata = listaAsigurari
                .Where(d => d.Id_subcategorie == Cod_subcategorie())
                .Where(d => d.Id_capacitate == cod_cap())
                .Where(d => d.Id_grupa == ((GrupeVarsta)comboBoxGrupaVarsta.SelectedItem).Id_grupa)
                .ToList();
            dataGridViewAsigurariCreate.DataSource = listaFiltrata;
            dataGridViewAsigurariCreate.Columns[0].Visible = false;
            dataGridViewAsigurariCreate.Columns[1].Visible = false;
            dataGridViewAsigurariCreate.Columns[2].Visible = false;
            dataGridViewAsigurariCreate.Columns[3].Visible = false;
            dataGridViewAsigurariCreate.Columns[4].Visible = false;
            dataGridViewAsigurariCreate.Columns[5].Visible = false;
            dataGridViewAsigurariCreate.Columns[6].Visible = false;
            dataGridViewAsigurariCreate.Columns[7].Visible = false;
            dataGridViewAsigurariCreate.Columns[8].Visible = false;
            dataGridViewAsigurariCreate.Columns[9].Visible = false;
            dataGridViewAsigurariCreate.Columns[10].HeaderText = "Valoare prima de risc(lei)";
            dataGridViewAsigurariCreate.Columns[11].Visible = false;
            dataGridViewAsigurariCreate.Columns[12].Visible = false; 
            dataGridViewAsigurariCreate.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewAsigurariCreate.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewAsigurariCreate.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewAsigurariCreate.DefaultCellStyle.Font = new Font("Times new roman", 16);
        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if (!Verificari.checkCnp(textBoxPrimadeRisc.Text) || string.IsNullOrEmpty(textBoxPrimadeRisc.Text))
            {
                MessageBox.Show("Va rog sa introduce-ti valoarea primei de risc in forma corespunzator");
            }
            else
            {
                if (!checkExist())
                {
                    MessageBox.Show("Valoarea primei Rca pe care doriti sa o adaugati pentru parametri selectati exista deja pentru anul in curs," +
                        "contactati managerul pentru modificarea acesteia!");
                }
                else
                {
                    int id_asigurare = 1;
                    List<Calcul_prima_RCA> listaFullRCA = DatabaseAcces.ExtrageAsigurareRCA();
                    if (listaFullRCA.Count > 0)
                    {
                        id_asigurare = listaFullRCA.Max(d => d.Calcul_RCA_PrimaryKey) + 1;
                    }
                    DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati Asigurarea", "Confirmare", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        Calcul_prima_RCA asig = new Calcul_prima_RCA()
                        {
                            Calcul_RCA_PrimaryKey = id_asigurare,
                            Id_subcategorie = Cod_subcategorie(),
                            Id_grupa = ((GrupeVarsta)comboBoxGrupaVarsta.SelectedItem).Id_grupa,
                            Id_capacitate = ((CapacitateCilindrica)comboBoxCapacitati.SelectedItem).Id_capacitate,
                            Id_durata = ((DurataAsigurare)comboBoxDurata.SelectedItem).Id_durata,
                            Id_zona = ((ZonaGeografica)comboBoxZonaGeografica.SelectedItem).Id_zona,
                            Id_beneficiu = ((BeneficiiSuplimentare)comboBoxBeneficiiSuplimentare.SelectedItem).Id_beneficiu,
                            Id_discount = ((Discount_RCA)comboBoxDiscount.SelectedItem).Id_discount,
                            Id_utilizare = ((DomeniiUtilizare)comboBoxUtilizare.SelectedItem).Id_utilizare,
                            Id_bonus_malus = ((Bonus_Malus_Class)comboBoxBonusMalus.SelectedItem).Id_bonus_malus,
                            Prima_de_risc = Convert.ToInt32(textBoxPrimadeRisc.Text),
                            status_asigurare = true,
                            Data_adaugare = Convert.ToString(DateTime.Now)
                        };
                        DatabaseAcces.AdaugaRCA(asig);
                        listaAsigurari = DatabaseAcces.ExtrageAsigurareRCA().Where(d => d.status_asigurare == true).ToList();
                        AddToDataGrid();
                        textBoxPrimadeRisc.Clear();
                        MessageBox.Show("Asigurare adaugata cu succes!");
                    }
                    else
                    {
                        MessageBox.Show("Adaugare anulata!");
                        AddToDataGrid();
                        textBoxPrimadeRisc.Clear();
                    }
                }
            }
        }

        private void buttonSterge_Click(object sender, EventArgs e)
        {

            if (dataGridViewAsigurariCreate.CurrentRow != null)
            {
                int cod = Convert.ToInt32(dataGridViewAsigurariCreate.CurrentRow.Cells[0].Value);
                bool status = false;
                DialogResult dialog = MessageBox.Show("Sigur doriti sa stergeti valoarea asigurarea", "Confirmare", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    DatabaseAcces.ModificaStatusAsigurareRCA(cod, status);
                    listaAsigurari = DatabaseAcces.ExtrageAsigurareRCA().Where(d => d.status_asigurare == true).ToList();
                    AddToDataGrid();
                    dataGridViewAsigurariCreate.AutoResizeColumns();
                    MessageBox.Show("Stergere realizata cu succes!");
                }
                else
                {
                    MessageBox.Show("Stergere anulata");
                }
            }
            else
            {
                MessageBox.Show("Trebuie sa selecta-ti o asigurare inainte de a o sterge!!");
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

        private void comboBoxGreutateMinima_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddGreutateMaximaComboBox();
            AddToDataGrid();
        }

        private void comboBoxNumarLocuri_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddGreutateMinimaComboBox();
            AddToDataGrid();
        }

        private void comboBoxGrupaVarsta_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddToDataGrid();
        }

        private void comboBoxCapacitati_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddToDataGrid();
        }
    }
}
