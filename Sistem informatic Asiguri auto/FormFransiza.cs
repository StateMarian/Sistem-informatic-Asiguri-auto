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
    public partial class FormFransiza : Form
    {
        public FormFransiza()
        {
            InitializeComponent();
            AddFransizaToComboBox();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }
        List<Fransiza> listaFransize = DatabaseAcces.ExtrageFransiza().Where(d => d.status_fransiza == true).ToList();

        void AddFransizaToComboBox()
        {
            comboBoxDenFransiza.DataSource = null;
            var listaFranFiltrate = listaFransize
                .Select(d => d.Tip_fransiza)
                .Distinct()
                .ToList();
            listaFranFiltrate.Sort();
            listaFranFiltrate.Add("Adauga nou...");
            comboBoxDenFransiza.DataSource = listaFranFiltrate;
            comboBoxDenFransiza.DisplayMember = "Tip_fransiza";
        }

        void AdDProcentToCombobox()
        {
            var FransizaSelectata = comboBoxDenFransiza.SelectedItem as string;
            List<string> procente = new List<string>();
            if (comboBoxDenFransiza.SelectedItem != null)
            {
                foreach (Fransiza fra in listaFransize)
                {
                    if (fra.Tip_fransiza == FransizaSelectata)
                    {
                        var procent_fran = fra.Procent;
                        procente.Add(Convert.ToString(procent_fran));
                    }
                }
                procente.Add("Adauga nou...");
            }
            comboBoxprocentFransiza.DataSource = null;
            //procente.Sort();
            comboBoxprocentFransiza.DataSource = procente;
        }
        void AdDProcentReducereToCombobox()
        {
            var FransizaSelectata = comboBoxDenFransiza.SelectedItem as string;
            var procentSelectat = Convert.ToInt32(comboBoxprocentFransiza.SelectedItem);
            if (comboBoxDenFransiza.SelectedItem != null && comboBoxprocentFransiza.SelectedItem != null)
            {
                foreach (Fransiza fra in listaFransize)
                {
                    if (fra.Tip_fransiza == FransizaSelectata && fra.Procent == procentSelectat)
                    {
                        var procent_fran_reducere = fra.Procent_reducere;
                        numericUpDownProcentRed.Value = procent_fran_reducere;
                    }
                }
            }
        }
        bool checkExistFran()
        {
            string fransiza = comboBoxDenFransiza.Text;
            int procentFran = Convert.ToInt32(comboBoxprocentFransiza.Text);
            int procentRed = Convert.ToInt32(numericUpDownProcentRed.Value);
            foreach (Fransiza fran in listaFransize)
            {
                if (fran.Tip_fransiza == fransiza && fran.Procent == procentFran && fran.Procent_reducere == procentRed)
                {
                    return false;
                }
            }
            return true;
        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if (!Verificari.checkName(comboBoxDenFransiza.Text) || string.IsNullOrEmpty(comboBoxDenFransiza.Text))
            {
                MessageBox.Show("Va rog introduce-ti fransiza in format corespunzator, nu poate contine cifre sau sa fie gol!");
            }
            else
            {
                if (string.IsNullOrEmpty(comboBoxprocentFransiza.Text))
                {
                    MessageBox.Show("Va rog selectati un procenta sau introduceti procentul in format corespunzator!");
                }
                else
                {
                    if (!Verificari.checkCnp(comboBoxprocentFransiza.Text))
                    {
                        MessageBox.Show("Procentul introdus trebuie sa fie numar intreg!"); 
                    }
                    else
                    {
                        if (Convert.ToInt32(comboBoxprocentFransiza.Text) < 0 || Convert.ToInt32(comboBoxprocentFransiza.Text) > 50)
                        {
                            MessageBox.Show("Procentul fransizei poate sa fie doar intre 1 si 50!");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(numericUpDownProcentRed.Text))
                            {
                                MessageBox.Show("Va rog selectati un procenta sau introduceti procentul de reducere in format corespunzator!");
                            }
                            else
                            {
                                if (!Verificari.checkCnp(numericUpDownProcentRed.Text))
                                {
                                    MessageBox.Show("Procentul introdus trebuie sa fie numar intreg!");
                                }
                                else
                                {
                                    if (Convert.ToInt32(numericUpDownProcentRed.Value) < 0 || Convert.ToInt32(numericUpDownProcentRed.Value) > 30)
                                    {
                                        MessageBox.Show("Procentul de reducere poate sa fie doar intre 1 si 30!");
                                    }
                                    else
                                    {
                                        if (!checkExistFran())
                                        {
                                            MessageBox.Show("Fransiza pe care doriti sa o adaugati cu procentele selectate sau introduse exista deja, pentru a introduce una noua cu alte procente trebuie sa o stergeti pe cea existenta!");
                                        }
                                        else
                                        {
                                            int id_fran = 1;
                                            List<Fransiza> listaToateFransize = DatabaseAcces.ExtrageFransiza();
                                            if (listaToateFransize.Count > 0)
                                            {
                                                id_fran = listaToateFransize.Max(d => d.Id_fransiza) + 1;
                                            }
                                            DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati fransiza", "Confirmare", MessageBoxButtons.YesNo);
                                            if (dialog == DialogResult.Yes)
                                            {
                                                Fransiza fran = new Fransiza()
                                                {
                                                    Id_fransiza = id_fran,
                                                    Tip_fransiza = comboBoxDenFransiza.Text,
                                                    Procent = Convert.ToInt32(comboBoxprocentFransiza.Text),
                                                    Procent_reducere = Convert.ToInt32(numericUpDownProcentRed.Value),
                                                    status_fransiza = true
                                                };
                                                listaFransize.Add(fran);
                                                DatabaseAcces.AdaugaFransiza(fran);
                                                listaFransize = DatabaseAcces.ExtrageFransiza().Where(d => d.status_fransiza == true).ToList();
                                                AddFransizaToComboBox();
                                                MessageBox.Show("Fransiza a fost adaugata!");
                                            }
                                            else
                                            {
                                                listaFransize = DatabaseAcces.ExtrageFransiza().Where(d => d.status_fransiza == true).ToList();
                                                AddFransizaToComboBox();
                                                MessageBox.Show("Adaugare anulata!");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        int IdFran()
        {
            int idFran = 0;
            string fransiza = comboBoxDenFransiza.Text;
            int procentFran = Convert.ToInt32(comboBoxprocentFransiza.Text);
            int procentRed = Convert.ToInt32(numericUpDownProcentRed.Value);
            foreach (Fransiza fran in listaFransize)
            {
                if (fran.Tip_fransiza == fransiza && fran.Procent == procentFran && fran.Procent_reducere == procentRed)
                {
                    idFran = fran.Id_fransiza;
                }
            }
            return idFran;
        }
        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxDenFransiza.Text))
            {
                MessageBox.Show("Pentru a sterge o fransiza va rog sa selectati denumirea fransizei!");
            }
            else
            {
                if (string.IsNullOrEmpty(comboBoxprocentFransiza.Text))
                {
                    MessageBox.Show("Pentru a sterge o fransiza va rog sa selectati procentul acesteia!");
                }
                else
                {
                    if (string.IsNullOrEmpty(numericUpDownProcentRed.Text))
                    {
                        MessageBox.Show("Pentru a sterge o fransiza va rog sa selectati procentul de reducere al acesteia!");
                    }
                    else
                    {
                        if (IdFran() == 0)
                        {
                            MessageBox.Show("Datele alese pentru stergere nu exista in sistem, va rog selectati datele corecte!");
                        }
                        else
                        {
                            int indexDelete = IdFran();
                            bool status = false;
                            DialogResult dialogResult = MessageBox.Show($"Sigur doriti sa stergeti fransiza", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                DatabaseAcces.ModificaStatusFransiza(indexDelete, status);
                                listaFransize = DatabaseAcces.ExtrageFransiza().Where(d => d.status_fransiza == true).ToList();
                                AddFransizaToComboBox();
                                MessageBox.Show("Stergerea a fost realizata cu succes!!");
                            }
                            else
                            {
                                listaFransize = DatabaseAcces.ExtrageFransiza().Where(d => d.status_fransiza == true).ToList();
                                AddFransizaToComboBox();
                                MessageBox.Show("Stergere anulata!!");
                            }
                        }
                    }
                }
            }
        }

        private void comboBoxDenFransiza_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdDProcentToCombobox();
        }

        private void comboBoxprocentFransiza_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Convert.ToString(comboBoxprocentFransiza.SelectedItem)!="Adauga nou...")
            {
                AdDProcentReducereToCombobox();
            }
            else
            {
                numericUpDownProcentRed.Value = 0;
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
                int mousex = MousePosition.X - 262;
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
