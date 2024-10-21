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
    public partial class FormCategoriiAuto : Form
    {
        public FormCategoriiAuto()
        {
            InitializeComponent();
            AddCategoriiToListBox();
            Verificari.Listbox(listBoxCategorii);
            this.FormBorderStyle = FormBorderStyle.None;
        }

        List<Categorii> listaCategorii = DatabaseAcces.ExtrageCategorii().Where(d=>d.status_categorie==true).ToList();

        private void buttonBack_Click(object sender, EventArgs e)
        {
            FormAngajat frm = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            frm.ShowDialog();
        }
        void AddCategoriiToListBox()
        {
            listBoxCategorii.DataSource = null;
            listBoxCategorii.Sorted = true;
            listBoxCategorii.DataSource = listaCategorii;
            listBoxCategorii.DisplayMember = "Denumire_Categorie";
        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBoxDenumire.Text))
            {
                MessageBox.Show("Va rog sa completati campul pentru denumire!!!");    
            }
            else
            {
                if(string.IsNullOrEmpty(textBoxCod.Text) )
                {
                    MessageBox.Show("Va rog sa completati campul pentru cod!!!");
                }
                else
                {
                    int id_categorie=1;
                    List <Categorii> listaToateCategoriile = DatabaseAcces.ExtrageCategorii();
                    if(listaToateCategoriile.Count>0)
                    {
                        id_categorie = listaToateCategoriile.Max(d => d.Id_categorie) + 1;
                    }
                    DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati categoria?", "Confirmare", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        Categorii cat = new Categorii()
                        {
                            Id_categorie=id_categorie,
                            Denumire_categorie = textBoxDenumire.Text.TrimEnd(),
                            Cod_categorie=textBoxCod.Text.TrimEnd(),
                            status_categorie=true
                        };
                        listaCategorii.Add(cat);
                        DatabaseAcces.AddCategorie(cat);
                        AddCategoriiToListBox();
                        textBoxDenumire.Clear();
                        textBoxCod.Clear();
                        Verificari.Listbox(listBoxCategorii);
                    }
                    else
                    {
                        MessageBox.Show("Adaugare anulata!!");
                        Verificari.Listbox(listBoxCategorii);
                        textBoxDenumire.Clear();
                        textBoxCod.Clear();
                    }
                }
            }
        }

        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (listBoxCategorii.SelectedItem != null)
            {
                int indexDelete = ((Categorii)listBoxCategorii.SelectedItem).Id_categorie;
                bool status = false;
                DialogResult dialogResult = MessageBox.Show($"Sigur doriti sa stergeti categoria?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    DatabaseAcces.ModificaStatusCategorie(indexDelete,status);
                    listaCategorii = DatabaseAcces.ExtrageCategorii().Where(d => d.status_categorie == true).ToList();
                    AddCategoriiToListBox();
                    Verificari.Listbox(listBoxCategorii);
                }
                else
                {
                    MessageBox.Show("Stergere anulata!!");
                    Verificari.Listbox(listBoxCategorii);
                }
            }
            else
            {
                MessageBox.Show("Selectati o categorie pentru a o sterge!");
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
                int mousex = MousePosition.X - 680;
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
