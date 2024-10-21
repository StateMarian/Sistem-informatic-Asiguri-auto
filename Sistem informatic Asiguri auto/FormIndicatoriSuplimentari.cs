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
    public partial class FormIndicatoriSuplimentari : Form
    {
        public FormIndicatoriSuplimentari()
        {
            InitializeComponent();
            AddIndicatoriToListBox();
            Verificari.Listbox(listBoxIndicatori);
            this.FormBorderStyle = FormBorderStyle.None;
        }

        List<IndicatoriSuplimentari> listaIndicatori = DatabaseAcces.ExtrageIndicatoriSuplimentari().Where(d => d.status_indicator == true).ToList();

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }

        void AddIndicatoriToListBox()
        {
            listBoxIndicatori.DataSource = null;
            listBoxIndicatori.Sorted = true;
            listBoxIndicatori.DataSource = listaIndicatori;
            listBoxIndicatori.DisplayMember = "Denumire_indicator";
        }


        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if(!Verificari.checkName(textBoxDenumire.Text) || string.IsNullOrEmpty(textBoxDenumire.Text))
            {
                MessageBox.Show("Va rog introduce-ti indicatorul in format corespunzator, nu poate contine cifre sau sa fie gol!");
                textBoxDenumire.Clear();
            }
            else
            {
                int id_ind = 1;
                List<IndicatoriSuplimentari> listaFullIndicatori = DatabaseAcces.ExtrageIndicatoriSuplimentari();
                if (listaFullIndicatori.Count > 0)
                {
                    id_ind = listaFullIndicatori.Max(d => d.Id_indicatori) + 1;
                }
                DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati indicatorul", "Confirmare", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    IndicatoriSuplimentari ind = new IndicatoriSuplimentari()
                    {
                        Id_indicatori=id_ind,
                        Denumire_indicator=textBoxDenumire.Text,
                        status_indicator=true
                    };
                    listaIndicatori.Add(ind);
                    DatabaseAcces.AdaugaIndicatori(ind);
                    textBoxDenumire.Clear();
                    AddIndicatoriToListBox();
                    Verificari.Listbox(listBoxIndicatori);
                    MessageBox.Show("Indicatorul a fost adaugat!");
                }
                else
                {
                    textBoxDenumire.Clear();
                    Verificari.Listbox(listBoxIndicatori);
                    MessageBox.Show("Adaugare anulata!");
                }
            }
        }

        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (listBoxIndicatori.SelectedItem != null)
            {
                int indexDelete = ((IndicatoriSuplimentari)listBoxIndicatori.SelectedItem).Id_indicatori;
                bool status = false;
                DialogResult dialogResult = MessageBox.Show($"Sigur doriti sa stergeti indicatorul?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    DatabaseAcces.ModificastatusIndicatori(indexDelete, status);
                    listaIndicatori = DatabaseAcces.ExtrageIndicatoriSuplimentari().Where(d => d.status_indicator == true).ToList();
                    AddIndicatoriToListBox();
                    Verificari.Listbox(listBoxIndicatori);
                    MessageBox.Show("Stergerea a fost realizata cu succes!!");
                }
                else
                {
                    MessageBox.Show("Stergere anulata!!");
                    Verificari.Listbox(listBoxIndicatori);
                }
            }
            else
            {   
                MessageBox.Show("Selecta-ti un indicator pentru a-l sterge!");
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
                int mousex = MousePosition.X - 375;
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
