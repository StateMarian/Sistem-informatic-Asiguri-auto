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
    public partial class FormMarca : Form
    {
        public FormMarca()
        {
            InitializeComponent();
            AddMarciToListBox();
            Verificari.Listbox(listBoxMarca);
            this.FormBorderStyle = FormBorderStyle.None;
        }

        List<Marca> listaMarci = DatabaseAcces.ExtrageMarca().Where(d => d.status_marca == true).ToList();
        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat from = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            from.ShowDialog();
        }

        void AddMarciToListBox()
        {
            listBoxMarca.DataSource = null;
            listBoxMarca.Sorted = true;
            listBoxMarca.DataSource = listaMarci;
            listBoxMarca.DisplayMember = "Denumire_marca";
        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if(!Verificari.checkName(textBoxDenumireMarca.Text) || string.IsNullOrEmpty(textBoxDenumireMarca.Text))
            {
                MessageBox.Show("Va rog introduce-ti marca in format corespunzator, nu poate contine cifre sau sa fie gol!");
            }
            else
            {
                int id_Marca = 1;
                List<Marca> listaFullMarca = DatabaseAcces.ExtrageMarca();
                if (listaFullMarca.Count > 0)
                {
                    id_Marca = listaFullMarca.Max(d => d.Id_marca) + 1;
                }
                DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati marca", "Confirmare", MessageBoxButtons.YesNo);
                if(dialog==DialogResult.Yes)
                {
                    Marca marc = new Marca()
                    {
                        Id_marca = id_Marca,
                        Denumire_marca = textBoxDenumireMarca.Text,
                        status_marca = true
                    };
                    listaMarci.Add(marc);
                    DatabaseAcces.AdaugaMarca(marc);
                    AddMarciToListBox();
                    textBoxDenumireMarca.Clear();
                    Verificari.Listbox(listBoxMarca);
                    MessageBox.Show("Marca a fost adaugata!");
                }
                else
                {
                    textBoxDenumireMarca.Clear();
                    Verificari.Listbox(listBoxMarca);
                    MessageBox.Show("Adaugare anulata!");
                }
            }
        }

        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (listBoxMarca.SelectedItem != null)
            {
                int indexDelete = ((Marca)listBoxMarca.SelectedItem).Id_marca;
                bool status = false;
                DialogResult dialogResult = MessageBox.Show($"Sigur doriti sa sterge-ti marca", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    DatabaseAcces.ModificastatusMarca(indexDelete, status);
                    listaMarci = DatabaseAcces.ExtrageMarca().Where(d => d.status_marca == true).ToList();
                    AddMarciToListBox();
                    Verificari.Listbox(listBoxMarca);
                    MessageBox.Show("Stergerea a fost realizata cu succes!!");
                }
                else
                {
                    MessageBox.Show("Stergere anulata!!");
                    Verificari.Listbox(listBoxMarca);
                }
            }
            else
            {
                MessageBox.Show("Selecta-ti o marca pentru a o sterge!");
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
                int mousex = MousePosition.X - 326;
                int mousey = MousePosition.Y - 16;
                this.SetDesktopLocation(mousex, mousey);
            }
        }

        private void panelBar_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }
    }
}
