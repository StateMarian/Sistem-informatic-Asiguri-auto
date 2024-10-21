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
    public partial class FormGrupeVarsta : Form
    {
        public FormGrupeVarsta()
        {
            InitializeComponent();
            AddGrupaVarstaToListBox();
            Verificari.Listbox(listBoxGrupeVarsta);
            this.FormBorderStyle = FormBorderStyle.None;
        }

        List<GrupeVarsta> listGrupe = DatabaseAcces.ExtrageGrupe().Where(d=>d.status_grupa==true).ToList();

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat from = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            from.ShowDialog();

        }

        void AddGrupaVarstaToListBox()
        {
            listBoxGrupeVarsta.DataSource = null;
            listBoxGrupeVarsta.Sorted = true;
            listBoxGrupeVarsta.DataSource = listGrupe;
            listBoxGrupeVarsta.DisplayMember = "StringLista";
        }

        private void buttonAdaugaGrupa_Click(object sender, EventArgs e)
        {
            if(!Verificari.checkCnp(textBoxMinVarsta.Text) || string.IsNullOrWhiteSpace(textBoxMinVarsta.Text))
            {
                MessageBox.Show("Introduceti varsta in formatul corect!");
            }
            else
            {
                if (!Verificari.checkCnp(textBoxMaxVarsta.Text) || string.IsNullOrWhiteSpace(textBoxMaxVarsta.Text)) 
                {
                    MessageBox.Show("Introduceti varsta in formatul corect!");
                }
                else
                {
                    if (!Verificari.IntervalVarsta(textBoxMinVarsta.Text, textBoxMaxVarsta.Text) || !Verificari.IntervalVarsta(textBoxMaxVarsta.Text,textBoxMinVarsta.Text))
                    {
                        MessageBox.Show("Varsta trebuie sa fie intre 18 si 80");
                    }
                    else
                    {
                        int id_grupa = 1;
                        List<GrupeVarsta> listGrupeTotal = DatabaseAcces.ExtrageGrupe();
                        if (listGrupeTotal.Count > 0)
                        {
                            id_grupa = listGrupeTotal.Max(d => d.Id_grupa) + 1;
                        }
                        DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati grupa", "Confirmare", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            GrupeVarsta grup = new GrupeVarsta()
                            {
                                Id_grupa = id_grupa,
                                Min_varsta = Convert.ToInt32(textBoxMinVarsta.Text),
                                Max_varsta = Convert.ToInt32(textBoxMaxVarsta.Text),
                                status_grupa = true
                            };
                            listGrupe.Add(grup);
                            AddGrupaVarstaToListBox();
                            DatabaseAcces.AdaugaGrupa(grup);
                            textBoxMinVarsta.Clear();
                            textBoxMaxVarsta.Clear();
                            Verificari.Listbox(listBoxGrupeVarsta);
                        }
                        else
                        {
                            MessageBox.Show("Adaugare anulata!");
                            Verificari.Listbox(listBoxGrupeVarsta);
                            textBoxMinVarsta.Clear();
                            textBoxMaxVarsta.Clear();
                        }
                    }
                }
            }
        }

        private void buttonStergeGrupa_Click(object sender, EventArgs e)
        {
            
            if (listBoxGrupeVarsta.SelectedItem != null)
            {
                DialogResult dialog = MessageBox.Show("Sigur doriti sa stergeti grupa!!", "Confirmare", MessageBoxButtons.YesNo);
                int index = ((GrupeVarsta)listBoxGrupeVarsta.SelectedItem).Id_grupa;
                bool status = false;
                if (dialog == DialogResult.Yes)
                {
                    DatabaseAcces.ModificaStatusGrupa(index,status);
                    listGrupe = DatabaseAcces.ExtrageGrupe().Where(d => d.status_grupa == true).ToList();
                    AddGrupaVarstaToListBox();
                    Verificari.Listbox(listBoxGrupeVarsta);
                }
                else
                {
                    MessageBox.Show("Stergere anulata!");
                    Verificari.Listbox(listBoxGrupeVarsta);
                }
            }
            else
            {
                MessageBox.Show("Trebuie sa selectati o grupa pentru a o putea sterge!!");
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
                int mousex = MousePosition.X - 260;
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
