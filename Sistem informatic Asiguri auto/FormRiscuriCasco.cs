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
    public partial class FormRiscuriCasco : Form
    {
        public FormRiscuriCasco()
        {
            InitializeComponent();
            AddRiscuriToListBox();
            Verificari.Listbox(listBoxRiscuri);
            this.FormBorderStyle = FormBorderStyle.None;
        }
        List<RiscCasco> listaRiscuri = DatabaseAcces.ExtrageRiscuriCasco().Where(d => d.status_risc == true).ToList();

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }

        void AddRiscuriToListBox()
        {
            listBoxRiscuri.DataSource = null;
            listBoxRiscuri.Sorted = true;
            listBoxRiscuri.DataSource = listaRiscuri;
            listBoxRiscuri.DisplayMember = "Denumire_risc";
        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if(!Verificari.checkName(textBoxDenumire.Text) || string.IsNullOrEmpty(textBoxDenumire.Text))
            {
                MessageBox.Show("Va rog introduce-ti riscul in format corespunzator, nu poate contine cifre sau sa fie gol!");
                textBoxDenumire.Clear();
            }
            else
            {
                int id_risc = 1;
                List<RiscCasco> listaFullRiscuri = DatabaseAcces.ExtrageRiscuriCasco();
                if (listaFullRiscuri.Count > 0)
                {
                    id_risc = listaFullRiscuri.Max(d => d.id_risc) + 1;
                }
                DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati riscul", "Confirmare", MessageBoxButtons.YesNo);
                if(dialog ==  DialogResult.Yes)
                {
                    RiscCasco risc = new RiscCasco()
                    {
                        id_risc = id_risc,
                        Denumire_risc = textBoxDenumire.Text,
                        status_risc = true
                    };
                    listaRiscuri.Add(risc);
                    DatabaseAcces.AdaugaRisc(risc);
                    AddRiscuriToListBox();
                    textBoxDenumire.Clear();
                    Verificari.Listbox(listBoxRiscuri);
                    MessageBox.Show("Riscul a fost adaugat!");
                }
                else
                {
                    textBoxDenumire.Clear();
                    Verificari.Listbox(listBoxRiscuri);
                    MessageBox.Show("Adaugare anulata!");
                }
            }
        }

        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (listBoxRiscuri.SelectedItem != null)
            {
                int indexDelete = ((RiscCasco)listBoxRiscuri.SelectedItem).id_risc;
                bool status = false;
                DialogResult dialogResult = MessageBox.Show($"Sigur doriti sa stergeti riscul", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    DatabaseAcces.ModificastatusRisc(indexDelete, status);
                    listaRiscuri = DatabaseAcces.ExtrageRiscuriCasco().Where(d => d.status_risc == true).ToList();
                    AddRiscuriToListBox();
                    Verificari.Listbox(listBoxRiscuri);
                    MessageBox.Show("Stergerea a fost realizata cu succes!!");
                }
                else
                {
                    MessageBox.Show("Stergere anulata!!");
                    Verificari.Listbox(listBoxRiscuri);
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
                int mousex = MousePosition.X - 306;
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
