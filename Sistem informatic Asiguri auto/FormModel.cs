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
    public partial class FormModel : Form
    {
        public FormModel()
        {
            InitializeComponent();
            AddMarcaToListBox();
            ComboOption();
            this.FormBorderStyle = FormBorderStyle.None;
        }
        List<Marca> listaMarci = DatabaseAcces.ExtrageMarca().Where(d => d.status_marca == true).ToList();
        List<Model> listaModele = DatabaseAcces.ExtrageModel().Where(d => d.status_model == true).ToList();

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }
        void AddMarcaToListBox()
        {
            listBoxMarca.DataSource = null;
            listBoxMarca.Sorted = true;
            listBoxMarca.DataSource = listaMarci;
            listBoxMarca.DisplayMember = "Denumire_marca";
        }
        void AddModelToCombo()
        {
            comboBoxModel.DataSource = null;
            var listaFiltrata = listaModele
                .Where(d => d.Id_marca == ((Marca)listBoxMarca.SelectedItem).Id_marca)
                .Select(d => d.Denumire_model)
                .Distinct()
                .ToList();
            listaFiltrata.Sort();
            listaFiltrata.Add("Adauga nou...");
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
                listaMod.Add("Adauga nou...");
                comboBoxVarianta.DataSource = listaMod;      
            }
        }
        bool verificExistModel()
        {
            var modelSelectat = comboBoxModel.SelectedItem as string;
            var varianta = comboBoxVarianta.SelectedItem as string;
            var idMarca = ((Marca)listBoxMarca.SelectedItem).Id_marca;
            foreach (Model mod in listaModele)
            {
                if(mod.Denumire_model==modelSelectat && mod.Varianta==varianta && mod.Id_marca==idMarca)
                {
                    return false;
                }
            }
            return true;
        }
        void ComboOption()
        {
            comboBoxTipauto.Items.Clear();
            List<string> valoriPredefinite = new List<string> { "NORMAL", "LUX" };
            comboBoxTipauto.DataSource = valoriPredefinite;
            comboBoxTipauto.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if (listBoxMarca.SelectedItem == null)
            {
                MessageBox.Show("Va rog selectati o marca pentru a adauga un model!");
            }
            else
            {
                if (comboBoxModel.Text == "")
                {
                    MessageBox.Show("Va rog introduce-ti denumirea modelului in format corespunzator, acesta nu poate fi gol!");
                }
                else
                {
                    if (comboBoxVarianta.Text == "")
                    {
                        MessageBox.Show("Va rog sa introduceti varianta!");
                    }
                    else
                    {
                        if (!verificExistModel())
                        {
                            MessageBox.Show("Modelul pe care doriti sa il adaugati exista deja!");
                        }
                        else
                        {
                            int id_Model = 1;
                            List<Model> listaFullModel = DatabaseAcces.ExtrageModel();
                            if (listaFullModel.Count > 0)
                            {
                                id_Model = listaFullModel.Max(d => d.Id_model) + 1;
                            }

                            DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati modelul", "Confirmare", MessageBoxButtons.YesNo);
                            if (dialog == DialogResult.Yes)
                            {
                                Model mod = new Model()
                                {
                                    Id_model = id_Model,
                                    Denumire_model = comboBoxModel.Text,
                                    Varianta = comboBoxVarianta.Text,
                                    Tip_auto = comboBoxTipauto.Text,
                                    Id_marca = ((Marca)listBoxMarca.SelectedItem).Id_marca,
                                    status_model = true
                                };
                                listaModele.Add(mod);
                                DatabaseAcces.AdaugaModel(mod);
                                listBoxMarca.SetSelected(0, true);
                                MessageBox.Show("Modelul a fost adaugat!");
                            }
                            else
                            {
                                MessageBox.Show("Adaugare anulata!");
                                listBoxMarca.SetSelected(0, true);
                            }
                        }
                    }
                }
            }
        }
        int cod_model()
        {
            int cod_model = 0;
            var modelSelectat = comboBoxModel.SelectedItem as string;
            var varianta = comboBoxVarianta.SelectedItem as string;
            var idMarca = ((Marca)listBoxMarca.SelectedItem).Id_marca;
            foreach (Model mod in listaModele)
            {
                if (mod.Denumire_model == modelSelectat && mod.Varianta == varianta && mod.Id_marca == idMarca)
                {
                    cod_model = mod.Id_model;
                }
            }
            return cod_model;
        }
        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (listBoxMarca.SelectedItem == null)
            {
                MessageBox.Show("Pentru a sterge un model este necesar sa selectati o marca!");
            }
            else
            {
                if (cod_model() == 0)
                {
                    MessageBox.Show("Modelul selectat pentru stergere nu exista!");
                }
                else
                {
                    int indexDelete = cod_model();
                    bool status = false;
                    DialogResult dialogResult = MessageBox.Show($"Sigur doriti sa sterge-ti modelul", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DatabaseAcces.ModificastatusModel(indexDelete, status);
                        listaModele = DatabaseAcces.ExtrageModel().Where(d => d.status_model == true).ToList();
                        listBoxMarca.SetSelected(0, true);
                        MessageBox.Show("Stergerea a fost realizata cu succes!!");
                    }
                    else
                    {
                        MessageBox.Show("Stergere anulata!!");
                        listBoxMarca.SetSelected(0, true);
                    }
                }
            }   
        }

        private void listBoxMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddModelToCombo();
        }

        private void comboBoxModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Convert.ToString(comboBoxModel.SelectedItem)==null)
            {
                comboBoxVarianta.DataSource = null;
            }    
            else
            {
                AddVariantaToCombo();
            }
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
                int mousex = MousePosition.X - 317;
                int mousey = MousePosition.Y - 17;
                this.SetDesktopLocation(mousex, mousey);
            }
        }

        private void panelBar_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }
    }
}
