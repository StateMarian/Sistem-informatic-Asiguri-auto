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
    public partial class FormAsociereRiscCasco : Form
    {
        public FormAsociereRiscCasco()
        {
            InitializeComponent();
            AddTipCascoToListBox();
            AddRiscuriToListBox();
            Verificari.Listbox(listBoxRiscuri);
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }

        List<Tip_casco> listaTipcasco = DatabaseAcces.ExtrageTipCasco().Where(d => d.status_tipCasco == true).ToList();
        List<RiscCasco> listaRiscuri = DatabaseAcces.ExtrageRiscuriCasco().Where(d => d.status_risc == true).ToList();
        List<Fransiza> listaFransiza = DatabaseAcces.ExtrageFransiza().Where(d => d.status_fransiza == true).ToList();

        void AddTipCascoToListBox()
        {
            comboBoxPachetCasco.DataSource = null;
            var listaCasco = listaTipcasco
                .Select(d => d.Denumire_casco)
                .Distinct()
                .ToList();
            listaCasco.Sort();
            comboBoxPachetCasco.DataSource = listaCasco;
            comboBoxPachetCasco.DisplayMember = "Denumire_casco";
        }

        void AddDenFransiza()
        {
            List<Fransiza> listaFran = DatabaseAcces.ExtrageFransizaDupaCasco(comboBoxPachetCasco.Text);
            comboBoxFransiza.DataSource = null;
            var listaFransiza = listaFran
                .Select(d => d.Tip_fransiza)
                .Distinct()
                .ToList();
            listaFransiza.Sort();
            comboBoxFransiza.DataSource = listaFransiza;
            comboBoxFransiza.DisplayMember = "Tip_Fransiza";
        }
        void AddProcentFransiza()
        {
            var tip_fransiza = comboBoxFransiza.SelectedItem as string;
            List<Fransiza> listaFran = DatabaseAcces.ExtrageFransizaDupaCasco(comboBoxPachetCasco.Text);
            comboBoxProcent.Items.Clear();
            if (comboBoxFransiza.SelectedItem != null)
            {
                foreach (Fransiza fran in listaFran)
                {
                    if (fran.Tip_fransiza == tip_fransiza)
                    {
                        var procentFran = fran.Procent;
                        comboBoxProcent.Items.Add(procentFran);
                        comboBoxProcent.SelectedIndex = 0;
                    }
                }
            }
        }
        void AddProcentReducereFransiza()
        {
            List<Fransiza> listaFran = DatabaseAcces.ExtrageFransizaDupaCasco(comboBoxPachetCasco.Text);
            comboBoxProcentRed.DataSource = null;
            var listaFransiza = listaFran
                .Where(d => d.Procent == Convert.ToInt32(comboBoxProcent.Text))
                .Select(d => d.Procent_reducere)
                .Distinct()
                .ToList();
            listaFransiza.Sort();
            comboBoxProcentRed.DataSource = listaFransiza;
        }

        int IdCasco()
        {
            int cod_Fran = 0;
            string denFran = comboBoxFransiza.SelectedItem as string;
            int procent = Convert.ToInt32(comboBoxProcent.Text);
            int procentRed = Convert.ToInt32(comboBoxProcentRed.Text);
            foreach (Fransiza fran in listaFransiza)
            {
                if (fran.Tip_fransiza == denFran && fran.Procent == procent && fran.Procent_reducere == procentRed)
                {
                    cod_Fran = fran.Id_fransiza;
                }
            }
            string DenCasco = comboBoxPachetCasco.Text;
            int codCasco = 0;
            foreach (Tip_casco casc in listaTipcasco)
            {
                if (casc.Denumire_casco == DenCasco && casc.Id_fransiza == cod_Fran)
                {
                    codCasco = casc.Id_casco;
                }
            }
            return codCasco;
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
            if (listBoxRiscuri.SelectedItem == null)
            {
                MessageBox.Show("Pentru a asocia un tip casco cu un risc trebuie sa selecta-ti un risc!");
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Sigur doriti sa efectuati asocierea", "Confirmare", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    try
                    {
                        AsociereRiscCasco asoc = new AsociereRiscCasco()
                        {
                            Id_casco = IdCasco(),
                            Id_risc = ((RiscCasco)listBoxRiscuri.SelectedItem).id_risc,
                            status_asociere = true
                        };
                        DatabaseAcces.AdaugaRiscCascoAsociate(asoc);
                        Verificari.Listbox(listBoxRiscuri);
                        MessageBox.Show("Asocierea a fost realizata cu succes!");
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627 || ex.Number == 2601)
                        {
                            MessageBox.Show("Casco a fost asociat cu riscul selectat!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Adaugare anulata!");
                    Verificari.Listbox(listBoxRiscuri);
                }
            }
        }

        private void comboBoxPachetCasco_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddDenFransiza();
        }

        private void comboBoxFransiza_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddProcentFransiza();
        }

        private void comboBoxProcent_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddProcentReducereFransiza();
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
                int mousex = MousePosition.X - 440;
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
