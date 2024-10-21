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
    public partial class FormZonaGeografica : Form
    {
        public FormZonaGeografica()
        {
            InitializeComponent();
            AddZoneToCombobox();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void buttonAcasa_Click(object sender, EventArgs e)
        {
            FormAngajat form = new FormAngajat(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }

        List<ZonaGeografica> listaZone = DatabaseAcces.ExtrageZoneGeografice().Where(d => d.status_zona == true).ToList();

        void ComboOption()
        {
            List<string> valoriPredefinite = new List<string> { "RCA", "CASCO" };
            comboBoxtipasigurare.DataSource = valoriPredefinite;
            comboBoxtipasigurare.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        void AddZoneToCombobox()
        {
            comboBoxZone.DataSource = null;
            comboBoxZone.DropDownStyle = ComboBoxStyle.DropDownList;
            var listaZoneFiltrate = listaZone
                .Select(d => d.Judet)
                .Distinct()
                .ToList();
            listaZoneFiltrate.Sort();
            comboBoxZone.DataSource = listaZoneFiltrate;
            comboBoxZone.DisplayMember = "Judet";
        }
        void AddProcentToNumericUpdown()
        {
            numericUpDownProcent.Value = Convert.ToDecimal(1.0);
            foreach (ZonaGeografica zon in listaZone)
            {
                if (zon.Judet == Convert.ToString(comboBoxZone.SelectedItem) && zon.Tip_Asigurare == Convert.ToString(comboBoxtipasigurare.SelectedItem))
                {
                    numericUpDownProcent.Value = (decimal)zon.Procent;
                }
            }
        }
        bool CheckZonaExistenta()
        {
            foreach (ZonaGeografica zon in listaZone)
            {
                if (zon.Judet == comboBoxZone.Text && zon.Tip_Asigurare == comboBoxtipasigurare.Text && zon.Procent == Convert.ToSingle(numericUpDownProcent.Value))
                {
                    return false;
                }
            }
            return true;
        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            if (!Verificari.checkName(comboBoxZone.Text) || string.IsNullOrEmpty(comboBoxZone.Text))
            {
                MessageBox.Show("Introduce-ti orasul in formatul corect, campul nu trebuie sa fie gol!!");
            }
            else
            {
                if (!Verificari.checkName(comboBoxtipasigurare.Text))
                {
                    MessageBox.Show("Tipul asigurari introdus nu este corect!!");
                }
                else
                {
                    if(Convert.ToInt32(numericUpDownProcent.Value) < 1 || Convert.ToInt32(numericUpDownProcent.Value) > 70)
                    {
                        MessageBox.Show("Procentul nu poate fi mai mic de 1 sau mai mare de 70 la suta!");
                    }
                    else
                    {
                        if (!CheckZonaExistenta())
                        {
                            MessageBox.Show("Zona geografica pe care doriti sa o adaugati exista deja, intai trebuie sa o stergeti apoi sa o adaugati pe cea noua!!");
                        }
                        else
                        {
                            int id_zona = 1;
                            List<ZonaGeografica> listaToateZonele = DatabaseAcces.ExtrageZoneGeografice();
                            if (listaToateZonele.Count > 0)
                            {
                                id_zona = listaToateZonele.Max(d => d.Id_zona) + 1;
                            }
                            DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati zona geografica", "Confirmare", MessageBoxButtons.YesNo);
                            if (dialog == DialogResult.Yes)
                            {
                                ZonaGeografica zona = new ZonaGeografica()
                                {
                                    Id_zona = id_zona,
                                    Judet = comboBoxZone.Text.TrimEnd(),
                                    Procent = Convert.ToSingle(numericUpDownProcent.Value),
                                    Tip_Asigurare = comboBoxtipasigurare.Text,
                                    status_zona = true
                                };
                                listaZone.Add(zona);
                                DatabaseAcces.AdaugaZona(zona);
                                AddZoneToCombobox();
                                numericUpDownProcent.Value = Convert.ToDecimal(1.0);
                                comboBoxtipasigurare.Text = "";
                                MessageBox.Show("Zona geografica a fost adaugata!");
                            }
                            else
                            {
                                AddZoneToCombobox();
                                MessageBox.Show("Adaugare anulata");
                                numericUpDownProcent.Value = Convert.ToDecimal(1.0);
                                comboBoxtipasigurare.Text = "";
                            }
                        }
                    }
                }
            }
        }
        int cod_zona()
        {
            int cod = 0; 
            foreach(ZonaGeografica zon in listaZone)
            {
                if(zon.Judet == comboBoxZone.Text && zon.Tip_Asigurare == comboBoxtipasigurare.Text && zon.Procent == Convert.ToSingle(numericUpDownProcent.Value))
                {
                    cod = zon.Id_zona;
                }
            }
            return cod;
        }
        private void buttonSterge_Click(object sender, EventArgs e)
        {
            if (comboBoxZone.Text == null)
            {
                MessageBox.Show("Pentru a sterge o zone trebuie sa selectati zona din lista!");
            }  
            else
            {
                if(cod_zona()==0)
                {
                    MessageBox.Show("Procentul selectat nu exista, selectati o zona cu un procent existent!");
                }
                else
                {
                    int indexDelete = cod_zona();
                    bool status = false;
                    DialogResult dialogResult = MessageBox.Show($"Sigur doriti sa stergeti zona geografica", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DatabaseAcces.ModificaStatusZonaGeografica(indexDelete, status);
                        listaZone = DatabaseAcces.ExtrageZoneGeografice().Where(d => d.status_zona == true).ToList();
                        AddZoneToCombobox();
                        MessageBox.Show("Stergere a fost realizata cu succes!!");
                        if(comboBoxtipasigurare.Text=="RCA")
                        {
                            comboBoxtipasigurare.Text = "CASCO";
                        }
                        else
                        {
                            comboBoxtipasigurare.Text = "RCA";
                        }
                    }
                    else
                    {
                        AddZoneToCombobox();
                        MessageBox.Show("Stergere anulata!!");
                    }
                }
            }
        }

        private void comboBoxtipasigurare_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddProcentToNumericUpdown();
        }

        private void comboBoxZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboOption();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                comboBoxZone.DataSource = null;
                comboBoxZone.DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
            {
                listaZone = DatabaseAcces.ExtrageZoneGeografice().Where(d => d.status_zona == true).ToList();
                AddZoneToCombobox();
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
                int mousex = MousePosition.X - 306;
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
