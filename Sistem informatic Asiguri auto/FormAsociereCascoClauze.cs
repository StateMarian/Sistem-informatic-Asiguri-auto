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
    public partial class FormAsociereCascoClauze : Form
    {
        public FormAsociereCascoClauze()
        {
            InitializeComponent();
            AddTipCascoToListBox();
            AddClauzeToListBox();
            Verificari.Listbox(listBoxClauzeSuplimentare);
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }

        List<Tip_casco> listaCasco = DatabaseAcces.ExtrageTipCasco().Where(d => d.status_tipCasco == true).ToList();
        List<Clauze_suplimentare> listaClauze = DatabaseAcces.ExtrageClauzeSuplimentare().Where(d => d.status_clauza == true).ToList();
        List<Fransiza> listaFransiza = DatabaseAcces.ExtrageFransiza().Where(d => d.status_fransiza == true).ToList();
        void AddTipCascoToListBox()
        {
            comboBoxTpcCasco.DataSource = null;
            var listaCascoFiltrata = listaCasco
                .Select(d => d.Denumire_casco)
                .Distinct()
                .ToList();
            listaCascoFiltrata.Sort();
            comboBoxTpcCasco.DataSource = listaCascoFiltrata;
            comboBoxTpcCasco.DisplayMember = "Denumire_casco";
        }
        void AddDenFransiza()
        {
            List<Fransiza> listaFran = DatabaseAcces.ExtrageFransizaDupaCasco(comboBoxTpcCasco.Text);
            comboBoxDenFran.DataSource = null;
            var listaFransiza = listaFran
                .Select(d => d.Tip_fransiza)
                .Distinct()
                .ToList();
            listaFransiza.Sort();
            comboBoxDenFran.DataSource = listaFransiza;
            comboBoxDenFran.DisplayMember = "Tip_Fransiza";
        }
        void AddProcentFransiza()
        {
            var tip_fransiza = comboBoxDenFran.SelectedItem as string;
            List<Fransiza> listaFran = DatabaseAcces.ExtrageFransizaDupaCasco(comboBoxTpcCasco.Text);
            comboBoxProcFran.Items.Clear();
            if (comboBoxDenFran.SelectedItem != null)
            {
                foreach (Fransiza fran in listaFran)
                {
                    if (fran.Tip_fransiza == tip_fransiza)
                    {
                        var procentFran = fran.Procent;
                        comboBoxProcFran.Items.Add(procentFran);
                        comboBoxProcFran.SelectedIndex = 0;
                    }
                }
            }
        }
        void AddProcentReducereFransiza()
        {
            List<Fransiza> listaFran = DatabaseAcces.ExtrageFransizaDupaCasco(comboBoxTpcCasco.Text);
            comboBoxProcRedFran.DataSource = null;
            var listaFransiza = listaFran
                .Where(d => d.Procent == Convert.ToInt32(comboBoxProcFran.Text))
                .Select(d => d.Procent_reducere)
                .Distinct()
                .ToList();
            listaFransiza.Sort();
            comboBoxProcRedFran.DataSource = listaFransiza;
        }
        void AddClauzeToListBox()
        {
            listBoxClauzeSuplimentare.DataSource = null;
            listBoxClauzeSuplimentare.Sorted = true;
            listBoxClauzeSuplimentare.DataSource = listaClauze;
            listBoxClauzeSuplimentare.DisplayMember = "Denumire_clauza";
        }
        int IdCasco()
        {
            int cod_Fran = 0;
            string denFran = comboBoxDenFran.SelectedItem as string;
            int procent = Convert.ToInt32(comboBoxProcFran.Text);
            int procentRed = Convert.ToInt32(comboBoxProcRedFran.Text);
            foreach (Fransiza fran in listaFransiza)
            {
                if (fran.Tip_fransiza == denFran && fran.Procent == procent && fran.Procent_reducere == procentRed)
                {
                    cod_Fran = fran.Id_fransiza;
                }
            }
            string DenCasco = comboBoxTpcCasco.Text;
            int codCasco = 0;
            foreach (Tip_casco casc in listaCasco)
            {
                if (casc.Denumire_casco == DenCasco && casc.Id_fransiza == cod_Fran)
                {
                    codCasco = casc.Id_casco;
                }
            }
            return codCasco;
        }

        private void buttonAsociere_Click(object sender, EventArgs e)
        {
            if (listBoxClauzeSuplimentare.SelectedItem == null)
            {
                MessageBox.Show("Pentru a asocia un tip casco cu o clauza trebuie sa selecta-ti o clauza!!");
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Sigur doriti sa efectuati asocierea", "Confirmare", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    try
                    {
                        AsociereCascoClauza asoc = new AsociereCascoClauza()
                        {
                            Id_casco = IdCasco(),
                            Id_clauza = ((Clauze_suplimentare)listBoxClauzeSuplimentare.SelectedItem).Id_clauza,
                            Valoare_clauza = Convert.ToSingle(numericUpDownValoareClauza.Value)
                        };
                        DatabaseAcces.AdaugaClauzaCascoAsociate(asoc);
                        Verificari.Listbox(listBoxClauzeSuplimentare);
                        MessageBox.Show("Asocierea a fost realizata cu succes!");
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627 || ex.Number == 2601)
                        {
                            MessageBox.Show("Casco a fost asociat cu clauza selectata!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Adaugare anulata!");
                    Verificari.Listbox(listBoxClauzeSuplimentare);
                }
            }
        }

        private void comboBoxTpcCasco_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddDenFransiza();
        }

        private void comboBoxProcFran_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddProcentReducereFransiza();
        }

        private void comboBoxDenFran_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddProcentFransiza();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit()
;        }

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
                int mousex = MousePosition.X - 419;
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
