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
    public partial class FormDomeniuUtilizare : Form
    {
        public FormDomeniuUtilizare()
        {
            InitializeComponent();
            AddDomeniuToListBox();
            Verificari.Listbox(listBoxDomeniiUtilizare);
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }

        List<DomeniiUtilizare> listaDomenii = DatabaseAcces.ExtrageDomeniiUtilizare().Where(d => d.status_domeniu == true).ToList();


        void AddDomeniuToListBox()
        {
            listBoxDomeniiUtilizare.DataSource = null;
            listBoxDomeniiUtilizare.Sorted = true;
            listBoxDomeniiUtilizare.DataSource = listaDomenii;
            listBoxDomeniiUtilizare.DisplayMember = "Domeniu";
        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if(!Verificari.checkName(textBoxDenumire.Text) || string.IsNullOrEmpty(textBoxDenumire.Text))
            {
                MessageBox.Show("Va rog introduce-ti domeniul in format corespunzator, nu poate contine cifre sau sa fie gol!");
                textBoxDenumire.Clear();
            }
            else
            {
                int id_dom = 1;
                List<DomeniiUtilizare> listaToateDomeniile = DatabaseAcces.ExtrageDomeniiUtilizare();
                if (listaToateDomeniile.Count > 0)
                {
                    id_dom = listaToateDomeniile.Max(d => d.Id_utilizare) + 1;
                }
                DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati Domeniul", "Confirmare", MessageBoxButtons.YesNo);
                if(dialog==DialogResult.Yes)
                {
                    DomeniiUtilizare dom = new DomeniiUtilizare()
                    {
                        Id_utilizare = id_dom,
                        Denumire_utilizare = textBoxDenumire.Text,
                        Procent_utilizare = Convert.ToSingle(numericUpDownProcentDomeniu.Value),
                        status_domeniu = true
                    };
                    listaDomenii.Add(dom);
                    DatabaseAcces.AdaugaDomeniu(dom);
                    AddDomeniuToListBox();
                    textBoxDenumire.Clear();
                    numericUpDownProcentDomeniu.Value = Convert.ToDecimal(1.0);
                    Verificari.Listbox(listBoxDomeniiUtilizare);
                    MessageBox.Show("Domeniul a fost adaugat!");
                }
                else
                {
                    MessageBox.Show("Adaugare anulata");
                    Verificari.Listbox(listBoxDomeniiUtilizare);
                    numericUpDownProcentDomeniu.Value = Convert.ToDecimal(1.0);
                    textBoxDenumire.Clear();
                }
            }
        }

        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (listBoxDomeniiUtilizare.SelectedItem != null)
            {
                int indexDelete = ((DomeniiUtilizare)listBoxDomeniiUtilizare.SelectedItem).Id_utilizare;
                bool status = false;
                DialogResult dialogResult = MessageBox.Show($"Sigur doriti sa stergeti domeniul", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    DatabaseAcces.ModificaStautsDomeniuUtilizare(indexDelete, status);
                    listaDomenii = DatabaseAcces.ExtrageDomeniiUtilizare().Where(d => d.status_domeniu == true).ToList();
                    AddDomeniuToListBox();
                    Verificari.Listbox(listBoxDomeniiUtilizare);
                    MessageBox.Show("Stergerea a fost realizata cu succes!!");
                }
                else
                {
                    MessageBox.Show("Stergere anulata!!");
                    Verificari.Listbox(listBoxDomeniiUtilizare);
                }
            }
            else
            {
                MessageBox.Show("Selectati un domeniu pentru a o sterge!");
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
                int mousex = MousePosition.X - 334;
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
