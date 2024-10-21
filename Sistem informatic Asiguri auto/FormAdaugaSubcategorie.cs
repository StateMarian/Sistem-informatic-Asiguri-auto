using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sistem_informatic_Asiguri_auto
{
    public partial class FormAdaugaSubcategorie : Form
    {
        public FormAdaugaSubcategorie()
        {
            InitializeComponent();
            AddCategoriiToListBox();
            Verificari.Listbox(listBoxSubcategorii);
            comboBoxLocuri.SelectedIndex = -1;
            comboBoxMasaMin.SelectedIndex = -1;
            comboBoxMasaMax.SelectedIndex = -1;
            this.FormBorderStyle = FormBorderStyle.None;

        }

        private void buttonInapoi_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }

        List<Categorii> listaCategorii = DatabaseAcces.ExtrageCategoriisiSubcategorii().Where(d => d.status_categorie == true).ToList();
        List<Subcategorii> listaSubcategorii = DatabaseAcces.ExtrageSubcategorii().Where(d => d.status_subcategorie == true).ToList();
        #region DateControale
        void AddCategoriiToListBox()
        {
            comboBoxCategorii.DataSource = null;
            comboBoxCategorii.DataSource = listaCategorii;
            comboBoxCategorii.DisplayMember = "Denumire_categorie";
        }
        void AddSubcategoriiToListBox()
        {
            listBoxSubcategorii.DataSource = null;
            var listaSubcategoriiFiltrata = listaSubcategorii
                .Where(s => s.Id_categorie == ((Categorii)comboBoxCategorii.SelectedItem).Id_categorie)
                .Select(d => d.Denumire_Subcategorie).ToList()
                .Distinct().ToList();
            listBoxSubcategorii.DataSource = listaSubcategoriiFiltrata;
            listBoxSubcategorii.DisplayMember = "Denumire_Subcategorie";
        }

        void AddNumarLocuriComboBox()
        {
            var subcategorieSelectata = listBoxSubcategorii.SelectedItem as string;
            if (listBoxSubcategorii.SelectedItem != null)
            {
                comboBoxLocuri.Items.Clear();
                comboBoxLocuri.Sorted = true;
                foreach (Subcategorii sub in listaSubcategorii)
                {
                    if (sub.Denumire_Subcategorie == subcategorieSelectata)
                    {
                        var nr_locuri = sub.Numar_locuri;
                        if (!comboBoxLocuri.Items.Contains(nr_locuri))
                        {
                            comboBoxLocuri.Items.Add(nr_locuri);
                        }
                    }
                }
                comboBoxLocuri.SelectedIndex = 0;
            }
        }
        void AddGreutateMinimaComboBox()
        {
            var nr_locuri = comboBoxLocuri.SelectedItem as string;
            var subcategorieSelectata = listBoxSubcategorii.SelectedItem as string;
            if (comboBoxLocuri.SelectedItem != null && listBoxSubcategorii.SelectedItem != null)
            {
                comboBoxMasaMin.Items.Clear();
                foreach (Subcategorii sub in listaSubcategorii)
                {
                    if (sub.Numar_locuri == nr_locuri && sub.Denumire_Subcategorie == subcategorieSelectata)
                    {
                        var greutate_min = sub.Masa_min;
                        comboBoxMasaMin.Items.Add(greutate_min);
                    }
                }
                comboBoxMasaMin.SelectedIndex = 0;
            }
        }
        void AddGreutateMaximaComboBox()
        {
            var nr_locuri = comboBoxLocuri.SelectedItem as string;
            var subcategorieSelectata = listBoxSubcategorii.SelectedItem as string;
            if (comboBoxLocuri.SelectedItem != null && listBoxSubcategorii.SelectedItem != null)
            {
                comboBoxMasaMax.Items.Clear();
                foreach (Subcategorii sub in listaSubcategorii)
                {
                    if (sub.Numar_locuri == nr_locuri && sub.Denumire_Subcategorie == subcategorieSelectata)
                    {
                        var greutate_max = sub.Masa_max;
                        comboBoxMasaMax.Items.Add(greutate_max);
                    }
                }
                comboBoxMasaMax.SelectedIndex = 0;
            }
        }
        bool checkSubExistenta()
        {
            string den_sub;
            if (checkBoxDeblocare.Checked)
            {
                den_sub = textBoxDenumire.Text;
            }
            else
            {
                den_sub = Convert.ToString(listBoxSubcategorii.SelectedItem);
            }
            string locuri = Convert.ToString(comboBoxLocuri.SelectedItem);
            string masamin = Convert.ToString(comboBoxMasaMin.SelectedItem);
            string masamax = Convert.ToString(comboBoxMasaMax.SelectedItem);
            foreach (Subcategorii sub in listaSubcategorii)
            {
                if (sub.Denumire_Subcategorie == den_sub && sub.Numar_locuri == locuri && sub.Masa_min == masamin && sub.Masa_max == masamax)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if (listBoxSubcategorii.SelectedItem == null && checkBoxDeblocare.Checked == false)
            {
                MessageBox.Show("Trebuie sa selecta-ti o subcategorie sau sa o introduce-ti in formatul corect!");
            }
            else
            {
                if (comboBoxCategorii.SelectedItem == null)
                {
                    MessageBox.Show("Va rugam sa selectati o categorie");
                }
                else
                {
                    if (!Verificari.checkName(textBoxDenumire.Text) || string.IsNullOrEmpty(textBoxDenumire.Text))
                    {
                        MessageBox.Show("Denumirea subcategoriei nu este corecta va rugam introduce-ti categoria in formatul corect!!");
                    }
                    else
                    {
                        if (comboBoxLocuri.Text == "")
                        {
                            MessageBox.Show("Numarul de locuri nu respecta formatul impus!");
                        }
                        else
                        {
                            if (comboBoxMasaMin.Text == "" || !Verificari.checkCnp(comboBoxMasaMin.Text))
                            {
                                MessageBox.Show("Masa introdusa nu respecta formatul impus!");
                            }
                            else
                            {
                                if (comboBoxMasaMax.Text == "" || !Verificari.checkCnp(comboBoxMasaMax.Text))
                                {
                                    MessageBox.Show("Masa introdusa nu respecta formatul impus!");
                                }
                                else
                                {
                                    if (!checkSubExistenta())
                                    {
                                        MessageBox.Show("Subcategoria pe care doriti sa o adaugati exista deja!");
                                    }
                                    else
                                    {
                                        int id_Subcategorie = 1;
                                        List<Subcategorii> listaFullSubcategorii = DatabaseAcces.ExtrageSubcategorii();
                                        if (listaFullSubcategorii.Count > 0)
                                        {
                                            id_Subcategorie = listaFullSubcategorii.Max(d => d.Id_subcategorie) + 1;
                                        }
                                        string denumire_subcategorie = string.Empty;
                                        if (checkBoxDeblocare.Checked)
                                        {
                                            if (!Verificari.checkName(textBoxDenumire.Text) || string.IsNullOrEmpty(textBoxDenumire.Text))
                                            {
                                                MessageBox.Show("Denumirea subcategoriei nu este corecta va rugam introduce-ti categoria in formatul corect!!");
                                            }
                                            else
                                            {
                                                denumire_subcategorie = textBoxDenumire.Text;
                                            }
                                        }
                                        else
                                        {
                                            denumire_subcategorie = Convert.ToString(listBoxSubcategorii.SelectedItem);
                                        }
                                        if (!string.IsNullOrEmpty(denumire_subcategorie))
                                        {
                                            DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati subcategoria", "Confirmare", MessageBoxButtons.YesNo);
                                            if (dialog == DialogResult.Yes)
                                            {
                                                Subcategorii subCat = new Subcategorii()
                                                {
                                                    Id_subcategorie = id_Subcategorie,
                                                    Denumire_Subcategorie = denumire_subcategorie,
                                                    Numar_locuri = comboBoxLocuri.Text,
                                                    Masa_min = comboBoxMasaMin.Text,
                                                    Masa_max = comboBoxMasaMax.Text,
                                                    Id_categorie = ((Categorii)comboBoxCategorii.SelectedItem).Id_categorie,
                                                    status_subcategorie = true
                                                };
                                                DatabaseAcces.AddSubCategorii(subCat);
                                                listaSubcategorii.Add(subCat);
                                                AddSubcategoriiToListBox();
                                                textBoxDenumire.Clear();
                                                checkBoxDeblocare.Checked = false;
                                                MessageBox.Show("Subcategorie adaugata cu succes!");
                                            }
                                            else
                                            {
                                                MessageBox.Show("Adaugare anulata!");
                                                checkBoxDeblocare.Checked = false;
                                                textBoxDenumire.Clear();
                                                AddSubcategoriiToListBox();
                                            }
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        int Cod_subcategorie()
        {
            int cod_subcat = 0;
            string den_sub = Convert.ToString(listBoxSubcategorii.SelectedItem);
            string locuri = Convert.ToString(comboBoxLocuri.SelectedItem);
            string masamin = Convert.ToString(comboBoxMasaMin.SelectedItem);
            string masamax = Convert.ToString(comboBoxMasaMax.SelectedItem);
            foreach (Subcategorii sub in listaSubcategorii)
            {
                if (sub.Denumire_Subcategorie == den_sub && sub.Numar_locuri == locuri && sub.Masa_min == masamin && sub.Masa_max == masamax)
                {
                    cod_subcat = sub.Id_subcategorie;
                }
            }
            return cod_subcat;
        }
        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (listBoxSubcategorii.SelectedItem == null)
            {
                MessageBox.Show("Pentru a sterge o subcategorie trebuie sa selectati denumirea din lista!");
            }
            else
            {
                if (comboBoxLocuri.SelectedItem == null)
                {
                    MessageBox.Show("Pentru a sterge o subcategorie trebuie sa selectati numarul de locuri!");
                }
                else
                {
                    if (comboBoxMasaMin.SelectedItem == null)
                    {
                        MessageBox.Show("Pentru a sterge o subcategorie trebuie sa selectati masa minima!");
                    }
                    else
                    {
                        if (comboBoxMasaMax.SelectedItem == null)
                        {
                            MessageBox.Show("Pentru a sterge o subcategorie trebuie sa selectati masa maxima!");
                        }
                        else
                        {
                            int cod = Cod_subcategorie();
                            bool status = false;
                            if(cod==0)
                            {
                                MessageBox.Show("Datele selectate in controale nu sunt concordante!");
                            }
                            else
                            {
                                DialogResult dialog = MessageBox.Show("Sigur doriti sa stergeti subcategoria", "Confirmare", MessageBoxButtons.YesNo);
                                if (dialog == DialogResult.Yes)
                                {
                                    DatabaseAcces.ModificaStatusSubcategorie(cod, status);
                                    listaSubcategorii = DatabaseAcces.ExtrageSubcategorii().Where(d => d.status_subcategorie == true).ToList();
                                    AddSubcategoriiToListBox();
                                    MessageBox.Show("Stergere realizata cu succes!");
                                }
                                else
                                {
                                    listaSubcategorii = DatabaseAcces.ExtrageSubcategorii().Where(d => d.status_subcategorie == true).ToList();
                                    AddSubcategoriiToListBox();
                                    MessageBox.Show("Stergere anulata");
                                }
                            }
                            
                        }
                    }
                }
            }

        }
        #region Evenimente
        private void comboBoxCategorii_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddSubcategoriiToListBox();
            Verificari.Listbox(listBoxSubcategorii);
            listBoxSubcategorii.SelectedIndex = -1;
            comboBoxLocuri.SelectedIndex = -1;
            comboBoxMasaMin.SelectedIndex = -1;
            comboBoxMasaMax.SelectedIndex = -1;
        }

        private void listBoxSubcategorii_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddNumarLocuriComboBox();
        }

        private void checkBoxDeblocare_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxDeblocare.Checked)
            {
                labelDenumire.Enabled = false;
                textBoxDenumire.Enabled = false;
                checkBoxDeblocare.Checked = false;
                comboBoxLocuri.SelectedIndex = -1;
                comboBoxMasaMin.SelectedIndex = -1;
                comboBoxMasaMin.SelectedIndex = -1;
            }
            else
            {
                labelDenumire.Enabled = true;
                textBoxDenumire.Enabled = true;
                checkBoxDeblocare.Checked = true;
                comboBoxLocuri.SelectedIndex = -1;
                comboBoxMasaMin.SelectedIndex = -1;
                comboBoxMasaMax.SelectedIndex = -1;
                Verificari.Listbox(listBoxSubcategorii);
            }
        }

        private void comboBoxLocuri_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddGreutateMinimaComboBox();
            AddGreutateMaximaComboBox();
        }

        #endregion

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
                int mousex = MousePosition.X - 510;
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

