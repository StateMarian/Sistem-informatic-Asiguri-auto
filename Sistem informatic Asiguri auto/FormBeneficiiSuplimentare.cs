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
    public partial class FormBeneficiiSuplimentare : Form
    {
        public FormBeneficiiSuplimentare()
        {
            InitializeComponent();
            AddBeneficiuToListBox();
            Verificari.Listbox(listBoxBeneficii);
            textBoxBeneficiiPachet.Text = "";
            this.FormBorderStyle = FormBorderStyle.None; 
        }

        List<BeneficiiSuplimentare> listaBeneficii = DatabaseAcces.ExtrageBeneficii().Where(d => d.status_beneficiu == true).ToList();

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }


        void AddBeneficiuToListBox()
        {
            listBoxBeneficii.DataSource = null;
            listBoxBeneficii.Sorted = true;
            listBoxBeneficii.DataSource = listaBeneficii;
            listBoxBeneficii.DisplayMember = "Beneficiu";
        }

        bool benExist()
        {
            foreach(BeneficiiSuplimentare ben in listaBeneficii)
            {
                if(ben.Denumire_pachet.ToUpper()==textBoxDenumire.Text.ToUpper())
                {
                    return false;
                }
            }
            return true;
        }
        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if (!Verificari.checkName(textBoxDenumire.Text) || string.IsNullOrEmpty(textBoxDenumire.Text))
            {
                MessageBox.Show("Va rog introduce-ti denumnirea beneficiului in format corect,fara cifre si campul nu poate sa fie gol!!");
                textBoxDenumire.Clear();
            }
            else
            {
                if (string.IsNullOrEmpty(textBoxBeneficiiPachet.Text))
                {
                    MessageBox.Show("Campul pentru beneficii pachet nu poate fi gol!");
                }
                else
                {
                    if (!benExist())
                    {
                        MessageBox.Show("Pachetul pe doriti sa il adaugati exista deja, pentru a-l adauga cu alte beneficii sau alt procent trebuie sa il stergeti intai!");
                    }
                    else
                    {
                        int id_beneficii = 1;
                        List<BeneficiiSuplimentare> listaToateBeneficiile = DatabaseAcces.ExtrageBeneficii();
                        if (listaToateBeneficiile.Count > 0)
                        {
                            id_beneficii = listaToateBeneficiile.Max(d => d.Id_beneficiu) + 1;
                        }
                        DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati beneficiul", "Confirmare", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            BeneficiiSuplimentare ben = new BeneficiiSuplimentare()
                            {
                                Id_beneficiu = id_beneficii,
                                Denumire_pachet = textBoxDenumire.Text,
                                continut_pachet = textBoxBeneficiiPachet.Text,
                                Procent_pachet = Convert.ToSingle(numericUpDownProcent.Value),
                                status_beneficiu = true
                            };
                            listaBeneficii.Add(ben);
                            DatabaseAcces.AdaugaBeneficiu(ben);
                            AddBeneficiuToListBox();
                            textBoxDenumire.Clear();
                            textBoxBeneficiiPachet.Clear();
                            numericUpDownProcent.Value = Convert.ToDecimal(1.0);
                            Verificari.Listbox(listBoxBeneficii);
                            MessageBox.Show("Beneficiul a fost adaugat!");
                        }
                        else
                        {
                            MessageBox.Show("Adaugare anulata");
                            Verificari.Listbox(listBoxBeneficii);
                            numericUpDownProcent.Value = Convert.ToDecimal(1.0);
                            textBoxDenumire.Clear();
                            textBoxBeneficiiPachet.Clear();
                        }
                    }
                }
            }
        }

        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (listBoxBeneficii.SelectedItem != null)
            {
                int indexDelete = ((BeneficiiSuplimentare)listBoxBeneficii.SelectedItem).Id_beneficiu;
                bool status = false;
                DialogResult dialogResult = MessageBox.Show($"Sigur doriti sa stergeti beneficiul", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    DatabaseAcces.ModificaStatusBeneficiu(indexDelete, status);
                    listaBeneficii = DatabaseAcces.ExtrageBeneficii().Where(d => d.status_beneficiu == true).ToList();
                    AddBeneficiuToListBox();
                    Verificari.Listbox(listBoxBeneficii);
                    MessageBox.Show("Stergere a fost realizata cu succes!!");
                }
                else
                {
                    MessageBox.Show("Stergere anulata!!");
                    Verificari.Listbox(listBoxBeneficii);
                }
            }
            else
            {
                MessageBox.Show("Selectati un beneficiu pentru a o sterge!");
            }
        }

        private void listBoxBeneficii_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxBeneficii.SelectedItem != null)
            {
                foreach (BeneficiiSuplimentare ben in listaBeneficii)
                {
                    if (((BeneficiiSuplimentare)listBoxBeneficii.SelectedItem).Id_beneficiu == ben.Id_beneficiu)
                    {
                        textBoxBeneficiiPachet.Text = ben.continut_pachet;
                    }
                }
            }
            else
            {

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
