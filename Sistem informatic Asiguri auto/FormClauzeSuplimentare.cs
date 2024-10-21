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
    public partial class FormClauzeSuplimentare : Form
    {
        public FormClauzeSuplimentare()
        {
            InitializeComponent();
            AddClauzToListBox();
            Verificari.Listbox(listBoxClauzaSuplim);
            this.FormBorderStyle = FormBorderStyle.None;
        }

        List<Clauze_suplimentare> listaClauze = DatabaseAcces.ExtrageClauzeSuplimentare().Where(d => d.status_clauza == true).ToList();

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }

        void AddClauzToListBox()
        {
            listBoxClauzaSuplim.DataSource = null;
            listBoxClauzaSuplim.Sorted = true;
            listBoxClauzaSuplim.DataSource = listaClauze;
            listBoxClauzaSuplim.DisplayMember = "Denumire_clauza";
        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if(!Verificari.checkName(textBoxDenumireClauza.Text) || string.IsNullOrEmpty(textBoxDenumireClauza.Text))
            {
                MessageBox.Show("Va rog introduce-ti clauza in format corespunzator, nu poate contine cifre sau sa fie gol!");
                textBoxDenumireClauza.Clear();
            }
            else
            {
                int id_clauza = 1;
                List<Clauze_suplimentare> listaFullClauze = DatabaseAcces.ExtrageClauzeSuplimentare();
                if (listaFullClauze.Count > 0)
                {
                    id_clauza = listaFullClauze.Max(d => d.Id_clauza) + 1;
                }
                DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati clauza", "Confirmare", MessageBoxButtons.YesNo);
                if(dialog == DialogResult.Yes)
                {
                    Clauze_suplimentare cl = new Clauze_suplimentare()
                    {
                        Id_clauza=id_clauza,
                        Denumire_clauza=textBoxDenumireClauza.Text,
                        status_clauza=true
                    };
                    listaClauze.Add(cl);
                    DatabaseAcces.AdaugaClauza(cl);
                    AddClauzToListBox();
                    textBoxDenumireClauza.Clear();
                    Verificari.Listbox(listBoxClauzaSuplim);
                    MessageBox.Show("Clauza a fost adaugata!");
                }
                else
                {
                    textBoxDenumireClauza.Clear();
                    Verificari.Listbox(listBoxClauzaSuplim);
                    MessageBox.Show("Adaugare anulata!");
                }
            }
        }

        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (listBoxClauzaSuplim.SelectedItem != null)
            {
                int indexDelete = ((Clauze_suplimentare)listBoxClauzaSuplim.SelectedItem).Id_clauza;
                bool status = false;
                DialogResult dialogResult = MessageBox.Show($"Sigur doriti sa sterge-ti clauza", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    DatabaseAcces.ModificastatusClauza(indexDelete, status);
                    listaClauze = DatabaseAcces.ExtrageClauzeSuplimentare().Where(d => d.status_clauza == true).ToList();
                    AddClauzToListBox();
                    Verificari.Listbox(listBoxClauzaSuplim);
                    MessageBox.Show("Stergerea a fost realizata cu succes!!");
                }
                else
                {
                    MessageBox.Show("Stergere anulata!!");
                    Verificari.Listbox(listBoxClauzaSuplim);
                }
            }
            else
            {
                MessageBox.Show("Selecta-ti un risc pentru al o sterge!");
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
                int mousex = MousePosition.X - 366;
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
