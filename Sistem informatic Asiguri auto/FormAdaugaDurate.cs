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
    public partial class FormAdaugaDurate : Form
    {
        public FormAdaugaDurate()
        {
            InitializeComponent();
            ComboOption();
            AddDurateCombobox();
            this.FormBorderStyle = FormBorderStyle.None;
        }
        List<DurataAsigurare> listaDurate = DatabaseAcces.ExtrageDurataAsigurare().Where(d => d.status_durata == true).ToList();
        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }
        void ComboOption()
        {
            List<string> valoriPredefinite = new List<string> { "RCA", "CASCO" };
            comboBoxTipAsigurare.DataSource = valoriPredefinite;
            comboBoxTipAsigurare.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        void AddDurateCombobox()
        {
            comboBoxDurata.DataSource = null;
            var listaDurateFiltrate = listaDurate
                .Select(d => d.Durata)
                .Distinct()
                .ToList();
            listaDurateFiltrate.Sort();
            listaDurateFiltrate.Add("Adauga nou...");
            comboBoxDurata.DataSource = listaDurateFiltrate;
            comboBoxDurata.DisplayMember = "Durata";
        }
        void AddProcentToComboBox()
        {
            var durata = comboBoxDurata.SelectedItem as string;
            var tipAsigurare = comboBoxTipAsigurare.SelectedItem as string;
            List<string> listaMod = new List<string>();
            if (comboBoxDurata.SelectedItem != null)
            {
                comboBoxProcent.DataSource = null;
                foreach (DurataAsigurare dur in listaDurate)
                {
                    if (dur.Durata == durata && dur.Tip_asigurare == tipAsigurare)
                    {
                        var procent = dur.Procent_durata;
                        listaMod.Add(Convert.ToString(procent));
                    }
                }
                listaMod.Sort();
                listaMod.Add("Adauga nou...");
                comboBoxProcent.DataSource = listaMod;
            }
        }

        private void comboBoxTipAsigurare_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddProcentToComboBox();
        }

        private void comboBoxDurata_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddProcentToComboBox();
        }
        bool CheckDurataExistenta()
        {
            foreach (DurataAsigurare dur in listaDurate)
            {
                if (dur.Durata == comboBoxDurata.Text && dur.Procent_durata == Convert.ToInt32(comboBoxProcent.Text) && dur.Tip_asigurare == comboBoxTipAsigurare.Text)
                {
                    return false;
                }
            }
            return true;
        }
        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if (comboBoxDurata.Text == "" || comboBoxDurata.Text == "Adauga nou...")
            {
                MessageBox.Show("Campul pentru durata nu poate fi gol, selectati o durata sau introduceti una noua!");
            }
            else
            {
                if (comboBoxProcent.Text == "" || !Verificari.checkCnp(comboBoxProcent.Text) || comboBoxDurata.Text == "Adauga nou...")
                {
                    MessageBox.Show("Campul pentru durata nu poate fi gol, introduceti un procent valid!");
                }
                else
                {
                    if (!CheckDurataExistenta())
                    {
                        MessageBox.Show("Durata pe care doriti sa o introduceti exista deja in sistem!");
                    }
                    else
                    {
                        int id_dur = 1;
                        List<DurataAsigurare> listaToateDuratele = DatabaseAcces.ExtrageDurataAsigurare();
                        if (listaToateDuratele.Count > 0)
                        {
                            id_dur = listaToateDuratele.Max(d => d.Id_durata) + 1;
                        }
                        DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati durata asigurari", "Confirmare", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            DurataAsigurare dur = new DurataAsigurare()
                            {
                                Id_durata = id_dur,
                                Durata = comboBoxDurata.Text,
                                Procent_durata = Convert.ToInt32(comboBoxProcent.Text),
                                Tip_asigurare = comboBoxTipAsigurare.Text,
                                status_durata = true
                            };
                            listaDurate.Add(dur);
                            DatabaseAcces.AdaugaDurata(dur);
                            AddDurateCombobox();
                            ComboOption();
                            MessageBox.Show("Durata a fost adaugata!");
                        }
                        else
                        {
                            MessageBox.Show("Adaugare anulata!");
                            AddDurateCombobox();
                            ComboOption();
                        }
                    }
                }
            }

        }

        int Cod_durata()
        {
            int cod_durata= 0;
            foreach (DurataAsigurare dur in listaDurate)
            {
                if (dur.Durata == comboBoxDurata.Text && dur.Procent_durata == Convert.ToInt32(comboBoxProcent.Text) && dur.Tip_asigurare == comboBoxTipAsigurare.Text)
                {
                    cod_durata = dur.Id_durata;
                }
            }
            return cod_durata;
        }
        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (comboBoxDurata.Text == null)
            {
                MessageBox.Show("Pentru a sterge o durata trebuie sa selectati durata din lista!");
            }
            else
            {
                if (comboBoxProcent.Text == null)
                {
                    MessageBox.Show("Procentul selectat nu exista, selectati o durata cu un procent existent!");
                }
                else
                {
                    if (Cod_durata() == 0)
                    {
                        MessageBox.Show("Datele selectate nu sunt concordante cu datele din sistem, selectati date concordante cu datele din sistem!");
                    }
                    else
                    {
                        int indexDelete = Cod_durata();
                        bool status = false;
                        DialogResult dialogResult = MessageBox.Show($"Sigur doriti sa stergeti durata asigurari", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            DatabaseAcces.ModificaStatusDurata(indexDelete, status);
                            listaDurate = DatabaseAcces.ExtrageDurataAsigurare().Where(d => d.status_durata == true).ToList();
                            AddDurateCombobox();
                            MessageBox.Show("Stergere a fost realizata cu succes!!");
                            if (comboBoxTipAsigurare.Text == "RCA")
                            {
                                comboBoxTipAsigurare.Text = "CASCO";
                            }
                            else
                            {
                                comboBoxTipAsigurare.Text = "RCA";
                            }
                        }
                        else
                        {
                            AddDurateCombobox();
                            MessageBox.Show("Stergere anulata!!");
                        }
                    }
                }
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
                int mousex = MousePosition.X - 278;
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
