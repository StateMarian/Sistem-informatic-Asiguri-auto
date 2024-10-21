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
    public partial class FormDiscountRca : Form
    {
        public FormDiscountRca()
        {
            InitializeComponent();
            AddDiscountToListBox();
            Verificari.Listbox(listBoxDiscount);
            this.FormBorderStyle = FormBorderStyle.None;
        }
        List<Discount_RCA> listaDiscount = DatabaseAcces.ExtrageDiscount().Where(d => d.status_discount == true).ToList();

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }

        void AddDiscountToListBox()
        {
            listBoxDiscount.DataSource = null;
            listBoxDiscount.Sorted = true;
            listBoxDiscount.DataSource = listaDiscount;
            listBoxDiscount.DisplayMember = "Discount";
        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if(!Verificari.checkName(textBoxdENDISCOUNT.Text) || string.IsNullOrEmpty(textBoxdENDISCOUNT.Text))
            {
                MessageBox.Show("Va rog introduce-ti denumirea discountului in format corespunzator, nu poate contine cifre sau sa fie gol!");
                textBoxdENDISCOUNT.Clear();
            }
            else
            {
                int id_disc = 1;
                List<Discount_RCA> listaToateDiscount = DatabaseAcces.ExtrageDiscount();
                if (listaToateDiscount.Count > 0)
                {
                    id_disc = listaToateDiscount.Max(d => d.Id_discount) + 1;
                }
                DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati Discountul", "Confirmare", MessageBoxButtons.YesNo);
                if(dialog==DialogResult.Yes)
                {
                    Discount_RCA disc = new Discount_RCA()
                    {
                        Id_discount=id_disc,
                        Denumire_discount=textBoxdENDISCOUNT.Text,
                        Procent_discount=Convert.ToSingle(numericUpDown1.Value),
                        status_discount=true
                    };
                    listaDiscount.Add(disc);
                    DatabaseAcces.AdaugaDiscount(disc);
                    textBoxdENDISCOUNT.Clear();
                    AddDiscountToListBox();
                    numericUpDown1.Value = Convert.ToDecimal(1.0);
                    Verificari.Listbox(listBoxDiscount);
                    MessageBox.Show("Discountul a fost adaugat!");
                }
                else
                {
                    MessageBox.Show("Adaugare anulata");
                    Verificari.Listbox(listBoxDiscount);
                    numericUpDown1.Value = Convert.ToDecimal(1.0);
                    textBoxdENDISCOUNT.Clear();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBoxDiscount.SelectedItem != null)
            {
                int indexDelete = ((Discount_RCA)listBoxDiscount.SelectedItem).Id_discount;
                bool status = false;
                DialogResult dialogResult = MessageBox.Show($"Sigur doriti sa stergeti discount-ul", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    DatabaseAcces.ModificaStatusDiscount(indexDelete, status);
                    listaDiscount = DatabaseAcces.ExtrageDiscount().Where(d => d.status_discount == true).ToList();
                    AddDiscountToListBox();
                    Verificari.Listbox(listBoxDiscount);
                    MessageBox.Show("Stergerea a fost realizata cu succes!!");
                }
                else
                {
                    MessageBox.Show("Stergere anulata!!");
                    Verificari.Listbox(listBoxDiscount);
                }
            }
            else
            {
                MessageBox.Show("Selectati un discount pentru a o sterge!");
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
