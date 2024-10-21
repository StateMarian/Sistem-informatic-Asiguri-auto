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
    public partial class FormCapacitateCilindrica : Form
    {
        public FormCapacitateCilindrica()
        {
            InitializeComponent();
            AddCapacitateToDataGrid();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        List<CapacitateCilindrica> listaCapacitati = DatabaseAcces.ExtrageCapacitate().Where(d => d.status_capacitate==true).ToList();

        void AddCapacitateToDataGrid()
        {
            dataGridViewCapacitateCilindrica.DataSource = null;
            dataGridViewCapacitateCilindrica.DataSource = listaCapacitati;
            dataGridViewCapacitateCilindrica.Columns[0].Visible = false;
            dataGridViewCapacitateCilindrica.Columns[3].Visible = false;
            dataGridViewCapacitateCilindrica.Columns[5].Visible = false;
            dataGridViewCapacitateCilindrica.Columns[1].HeaderText = "Capacitate minima";
            dataGridViewCapacitateCilindrica.Columns[2].HeaderText = "Capacitate maxima";
            dataGridViewCapacitateCilindrica.Columns[4].HeaderText = "Putere";
            dataGridViewCapacitateCilindrica.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCapacitateCilindrica.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewCapacitateCilindrica.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCapacitateCilindrica.DefaultCellStyle.Font = new Font("Times new roman", 14);
        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if(!Verificari.checkCnp(textBoxMinCapacitate.Text) || string.IsNullOrEmpty(textBoxMinCapacitate.Text))
            {
                MessageBox.Show("Va rog introduceti capacitatea minima in formatul corect!");
            }
            else
            {
                if(!Verificari.checkCnp(textBoxMaxCapacitate.Text)  || string.IsNullOrEmpty(textBoxMaxCapacitate.Text))
                {
                    MessageBox.Show("Va rog introduceti capacitatea maxima in formatul corect!");
                }
                else
                {
                    if(!Verificari.checkCnp(textBoxPutere.Text) || string.IsNullOrEmpty(textBoxPutere.Text))
                    {
                        MessageBox.Show("Va rog introduceti puterea in formatul corect!");
                    }
                    else
                    {
                        int id_Capacitate = 1;
                        List<CapacitateCilindrica> listaCapacitatiTotal = DatabaseAcces.ExtrageCapacitate();
                        if (listaCapacitatiTotal.Count > 0)
                        {
                            id_Capacitate = listaCapacitatiTotal.Max(d => d.Id_capacitate) + 1;
                        }
                        DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati Capacitatea", "Confirmare", MessageBoxButtons.YesNo);
                        if(dialog == DialogResult.Yes)
                        {
                            CapacitateCilindrica cap = new CapacitateCilindrica()
                            {
                                Id_capacitate = id_Capacitate,
                                Min_capacitate = Convert.ToInt32(textBoxMinCapacitate.Text),
                                Max_capacitate = Convert.ToInt32(textBoxMaxCapacitate.Text),
                                Putere = Convert.ToInt32(textBoxPutere.Text),
                                status_capacitate = true
                            };
                            DatabaseAcces.AdaugaCapacitate(cap);
                            listaCapacitati.Add(cap);
                            AddCapacitateToDataGrid();
                            textBoxMaxCapacitate.Clear();
                            textBoxMinCapacitate.Clear();
                            textBoxPutere.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Adaugare anulata!");
                            textBoxMaxCapacitate.Clear();
                            textBoxMinCapacitate.Clear();
                            textBoxPutere.Clear();
                        }
                    }
                }
            }
        }

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }

        private void buttonSterge_Click(object sender, EventArgs e)
        {
            
            if(dataGridViewCapacitateCilindrica.CurrentRow !=null)
            {
                int indexDelete = Convert.ToInt32(dataGridViewCapacitateCilindrica.CurrentRow.Cells[0].Value);
                DialogResult dialog = MessageBox.Show("Sigur doriti sa stergeti capacitatea!!", "Confirmare", MessageBoxButtons.YesNo);
                bool status = false;
                if(dialog==DialogResult.Yes)
                {
                    DatabaseAcces.ModificaStatusCapacitate(indexDelete,status);
                    listaCapacitati = DatabaseAcces.ExtrageCapacitate().Where(d => d.status_capacitate == true).ToList();
                    AddCapacitateToDataGrid();
                }
                else
                {
                    MessageBox.Show("Stergere anulata");
                }
            }
            else
            {
                MessageBox.Show("Trebuie sa selectati o capacitate in tabel pentru a sterge");
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
                int mousex = MousePosition.X - 553;
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
