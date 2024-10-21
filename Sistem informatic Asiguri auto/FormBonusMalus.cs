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
    public partial class FormBonusMalus : Form
    {
        public FormBonusMalus()
        {
            InitializeComponent();
            AddBonusMaluListBox();
            Verificari.Listbox(listBoxBonusMalus);
            this.FormBorderStyle = FormBorderStyle.None;
        }

        List<Bonus_Malus_Class> listaClase = DatabaseAcces.ExtrageClaseBonusMalus().Where(d=>d.status_bonus==true).ToList();

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
            
        }

        void AddBonusMaluListBox()
        {
            
            listBoxBonusMalus.DataSource = null;
            listBoxBonusMalus.Sorted = true;
            listBoxBonusMalus.DataSource = listaClase;
            listBoxBonusMalus.DisplayMember = "FullClass";
        }

        private void buttonAdaugaClasa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDenumireBonus.Text))
            {
                MessageBox.Show("Trebuie sa completa-ti denumirea clase de Bonus_Malus!!");
            }
            else
            {
                if (!Verificari.checkCnp(textBoxProcent.Text) || string.IsNullOrWhiteSpace(textBoxProcent.Text))
                {
                    MessageBox.Show("Va rog introduce-ti procentul in formatul corect!!");
                }
                else
                {
                    if (!Verificari.IntervalBonusMalus(Convert.ToInt32(textBoxProcent.Text)))
                    {
                        MessageBox.Show("Clasa bonus-malus trebuie sa fie intre 40% si 190%!!");
                    }
                    else
                    {
                        List<Bonus_Malus_Class> listaClasePrimarykey = DatabaseAcces.ExtrageClaseBonusMalus();
                        int id_clasa = 1;
                        if (listaClasePrimarykey.Count > 0)
                        {
                            id_clasa = listaClasePrimarykey.Max(d => d.Id_bonus_malus) + 1;
                        }
                        DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati clasa?", "Confirmare", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            Bonus_Malus_Class bonmalus = new Bonus_Malus_Class()
                            {
                                Id_bonus_malus = id_clasa,
                                Bonus_Malus = textBoxDenumireBonus.Text.ToUpper(),
                                Procent = Convert.ToInt32(textBoxProcent.Text),
                                status_bonus=true
                            };
                            listaClase.Add(bonmalus);
                            DatabaseAcces.AdaugaClasa(bonmalus);
                            AddBonusMaluListBox();
                            textBoxDenumireBonus.Clear();
                            textBoxProcent.Clear();
                            Verificari.Listbox(listBoxBonusMalus);
                        }
                        else
                        {
                            MessageBox.Show("Adaugare anulata!!");
                            Verificari.Listbox(listBoxBonusMalus);
                            textBoxDenumireBonus.Clear();
                            textBoxProcent.Clear();

                        }
                    }
                }
            }
        }

        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (listBoxBonusMalus.SelectedItem != null)
            {
                DialogResult dialog = MessageBox.Show("Sigur doriti sa stergeti clasa!!", "Confirmare", MessageBoxButtons.YesNo);
                int index = ((Bonus_Malus_Class)listBoxBonusMalus.SelectedItem).Id_bonus_malus;
                bool status = false;
                if (dialog == DialogResult.Yes)
                {
                    DatabaseAcces.ModificaStatusBonusMalus(index,status);
                    listaClase = DatabaseAcces.ExtrageClaseBonusMalus().Where(d => d.status_bonus == true).ToList();
                    AddBonusMaluListBox();
                    Verificari.Listbox(listBoxBonusMalus);
                }
                else
                {
                    MessageBox.Show("Stergere anulata!!");
                    Verificari.Listbox(listBoxBonusMalus);
                }
            }
            else
            {
                MessageBox.Show("Trebuie sa selecati o clasa inainte de a o sterge!");

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
                int mousex = MousePosition.X - 335;
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
