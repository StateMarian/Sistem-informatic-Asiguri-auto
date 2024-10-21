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
    public partial class FormGestionareAngajati : Form
    {
        public FormGestionareAngajati()
        {
            InitializeComponent();
            AdaugaAngToGrid();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            FormManager form = new FormManager(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }

        List<Angajat> listAng = DatabaseAcces.ExtrageAngajati().Where(d=>d.status==true).ToList();

        void AdaugaAngToGrid()
        {
            dataGridAngajat.DataSource = null;
            dataGridAngajat.DataSource = listAng;
            dataGridAngajat.Columns[0].Visible = false;
            dataGridAngajat.Columns[6].Visible = false;
            dataGridAngajat.Columns[5].HeaderText = "Telefon";
            dataGridAngajat.Columns[7].HeaderText = "Tip angajat";
            dataGridAngajat.Columns[8].Visible = false;
            dataGridAngajat.Columns[9].Visible = false;
            dataGridAngajat.Columns[10].Visible = false;
            dataGridAngajat.Columns[11].Visible = false;
            dataGridAngajat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridAngajat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridAngajat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridAngajat.DefaultCellStyle.Font = new Font("Times new roman", 16);
            
        }
        


        private void buttonAdaugaAngajat_Click(object sender, EventArgs e)
        {
            FormAdaugaAngajat form = new FormAdaugaAngajat();
            ParentForm.Dispose();
            form.ShowDialog();
        }

        private void buttonActualizeazaAngajat_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Sigur doriti sa modificati datele angajatului?","Confirmare", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                DatabaseAcces.UpdateAngajat(listAng);
                listAng = DatabaseAcces.ExtrageAngajati().Where(d => d.status == true).ToList();
                AdaugaAngToGrid();
            }
            else
            {
                AdaugaAngToGrid();  
            }
        }
        
        private void textBoxSearchName_TextChanged(object sender, EventArgs e)
        {
            var filteredList = listAng.Where(angajat => (angajat.FullName).ToUpper().StartsWith(textBoxSearchName.Text.ToUpper())).ToList();
            dataGridAngajat.DataSource = new BindingList<Angajat>(filteredList);
        }

        private void buttonConcediere_Click(object sender, EventArgs e)
        {
           
            if (dataGridAngajat.CurrentRow != null)
            {
                DialogResult dialog = MessageBox.Show("Sigur doriti sa concediati angajatul?", "Confirmare", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    string cod_angajat = Convert.ToString(dataGridAngajat.CurrentRow.Cells[0].Value);
                    bool status = false;
                    string data_concediere = Convert.ToString(DateTime.Now);
                    DatabaseAcces.ConcediereAngajat(listAng, cod_angajat, data_concediere, status);
                    listAng = DatabaseAcces.ExtrageAngajati().Where(d => d.status == true).ToList();
                    AdaugaAngToGrid();
                }
                else
                {
                    MessageBox.Show("A-ti anulat operatiunea de concediere!!");
                }
            }
            else
            {
                MessageBox.Show("Selectati un angajat inainte de a concedia!");
            }
        }
    }
}
