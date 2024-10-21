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
    public partial class FormTipCasco : Form
    {
        public FormTipCasco()
        {
            InitializeComponent();
            AddTipCascoToListbox();
            AddFransizatoCombo();       
            this.FormBorderStyle = FormBorderStyle.None;
        }
        List<Fransiza> listaFransiza = DatabaseAcces.ExtrageFransiza().Where(d => d.status_fransiza == true).ToList();
        List<Tip_casco> listaCasco = DatabaseAcces.ExtrageTipCasco().Where(d => d.status_tipCasco == true).ToList();
        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }

        void AddFransizatoCombo()
        {
            comboBoxFransiza.DataSource = null;
            comboBoxFransiza.Sorted = true;
            var listaFranFiltrata = listaFransiza
                .Select(d => d.Tip_fransiza)
                .Distinct()
                .ToList();
            comboBoxFransiza.DataSource = listaFranFiltrata;
            comboBoxFransiza.DisplayMember = "Tip_fransiza";
        }
        void AddProcentFranToCombo()
        {
            var tip_fransiza = comboBoxFransiza.SelectedItem as string;
            if (comboBoxFransiza.SelectedItem != null)
            {
                comboBoxProcentFran.Items.Clear();
                foreach (Fransiza fran in listaFransiza)
                {
                    if (fran.Tip_fransiza==tip_fransiza)
                    {
                        var procentFran = fran.Procent;
                        comboBoxProcentFran.Items.Add(procentFran);
                        comboBoxProcentFran.SelectedIndex = 0;
                    }
                }    
            }
        }
        void AddProcentRedToCombo()
        {
            int procent = Convert.ToInt32(comboBoxProcentFran.Text);
            comboBoxProcentRed.DataSource = null;
            var listaFiltrata = listaFransiza
                .Where(d => d.Procent == Convert.ToInt32(procent))
                .Select(d => d.Procent_reducere)
                .Distinct()
                .ToList();
            listaFiltrata.Sort();
            comboBoxProcentRed.DataSource = listaFiltrata;
            comboBoxProcentRed.DisplayMember = "Procent_reducere";
        }
        
        void AddTipCascoToListbox()
        {
            comboBoxTipCasco.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTipCasco.DataSource = null;
            var listaFiltrata = listaCasco
                .Select(d => d.Denumire_casco)
                .Distinct()
                .ToList();
            listaFiltrata.Sort();
            comboBoxTipCasco.DataSource = listaFiltrata;
            comboBoxTipCasco.DisplayMember = "Denumire_casco";
        }
        int codFran()
        {
            string denFran = comboBoxFransiza.SelectedItem as string;
            int procent = Convert.ToInt32(comboBoxProcentFran.Text);
            int procentRed = Convert.ToInt32(comboBoxProcentRed.Text);
            int cod_fran = 0;
            foreach(Fransiza fran in listaFransiza)
            {
                if(fran.Tip_fransiza==denFran && fran.Procent==procent && fran.Procent_reducere==procentRed)
                {
                    cod_fran = fran.Id_fransiza;
                }
            }
            return cod_fran;
        }
        bool checkExist()
        {
            foreach(Tip_casco tip in listaCasco)
            {
                if(tip.Denumire_casco==comboBoxTipCasco.Text && tip.Id_fransiza==codFran())
                {
                    return false;
                }
            }
            return true;
        }
        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if (!Verificari.checkName(comboBoxTipCasco.Text))
            {
                MessageBox.Show("Va rog introduce-ti tipul casco in format corespunzator, nu poate contine cifre sau sa fie gol!");
            }
            else
            {
                if (!checkExist())
                {
                    MessageBox.Show("Asocierea dintre casco si fransiza selectata a fost realizata, alegeti alta fransiza care nu a fost asociata cu niciun tip casco!");
                }
                else
                {
                    int id_tipCasco = 1;
                    List<Tip_casco> listaToateCasco = DatabaseAcces.ExtrageTipCasco();
                    if (listaToateCasco.Count > 0)
                    {
                        id_tipCasco = listaToateCasco.Max(d => d.Id_casco) + 1;
                    }

                    DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati casco", "Confirmare", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        Tip_casco casc = new Tip_casco()
                        {
                            Id_casco = id_tipCasco,
                            Denumire_casco = comboBoxTipCasco.Text.Trim(),
                            status_tipCasco = true,
                            Id_fransiza = codFran()
                        };
                        listaCasco.Add(casc);
                        DatabaseAcces.AdaugaTipCasco(casc);
                        listaCasco = DatabaseAcces.ExtrageTipCasco().Where(d => d.status_tipCasco == true).ToList();
                        AddTipCascoToListbox();
                        MessageBox.Show("Tipul casco a fost adaugat!");
                    }
                    else
                    {
                        MessageBox.Show("Adaugare anulata!");
                    }
                }
            }
        }
        int codCasco()
        {
            string den_casco = comboBoxTipCasco.SelectedItem as string;
            int cod_casco = 0;
            foreach (Tip_casco casc in listaCasco)
            {
                if (casc.Denumire_casco==den_casco && casc.Id_fransiza==codFran())
                {
                    cod_casco = casc.Id_casco;
                }
            }
            return cod_casco;
        }
        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (comboBoxTipCasco.Text != "")
            {
                int idCasco = codCasco();
                if (idCasco == 0)
                {
                    MessageBox.Show("Datele selectate nu pentru stergere nu sunt corecte!");
                }
                else
                {
                    bool status = false;
                    DialogResult dialogResult = MessageBox.Show($"Sigur doriti sa stergeti tipul casco", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DatabaseAcces.ModificastatusTipCasco(idCasco, status);
                        listaCasco = DatabaseAcces.ExtrageTipCasco().Where(d => d.status_tipCasco == true).ToList();
                        listaFransiza = DatabaseAcces.ExtrageFransiza().Where(d => d.status_fransiza == true).ToList();
                        AddTipCascoToListbox();
                        MessageBox.Show("Stergerea a fost realizata cu succes!!");
                    }
                    else
                    {
                        MessageBox.Show("Stergere anulata!!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selectati un tip casco pentru a-l sterge!");
            }
        }

        private void comboBoxFransiza_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddProcentFranToCombo();
        }

        private void comboBoxProcentFran_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddProcentRedToCombo();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                comboBoxTipCasco.DataSource = null;
                comboBoxTipCasco.DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
            {
                listaCasco = DatabaseAcces.ExtrageTipCasco().Where(d => d.status_tipCasco == true).ToList(); 
                AddTipCascoToListbox();           
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
                int mousex = MousePosition.X - 344;
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
