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
    public partial class FormAngajat : Form
    {
        bool mousedown;
        public FormAngajat(string id_angajat)
        {
            InitializeComponent();
            AdaugaNume(id_angajat);
            VerificaValabilitateAsigurareCasco();
            VerificaValabilitateAsigurareRca();
            ModificaStatusRCAExpirate();
            ModificaStatusCascoExpirate();
            DisplayFirst();
            buttonIndicatoriRCA.BackColor = Color.AliceBlue;
            buttonIndicatoriRCA.ForeColor = Color.SteelBlue;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        List<Angajat> listaAng = DatabaseAcces.ExtrageAngajati();
        public void AdaugaNume(string id_angajat)
        {
            DateAngajat.IdAngajat = id_angajat;
            foreach (Angajat ang in listaAng)
            {
                if (ang.Cod_angajat == DateAngajat.IdAngajat)
                {
                    labelNume.Text = "Bine ai venit " + ang.FullName;
                }
            }
        }
        void DisplayFirst()
        {
            FormIndicatoriRCA rca = new FormIndicatoriRCA();
            rca.TopLevel = false;
            if (panelAfisare.Controls.Count > 0)
                panelAfisare.Controls.Clear();
            panelAfisare.Controls.Add(rca);
            rca.BringToFront();
            rca.Show();
        }
        private void buttonIndicatoriRCA_Click(object sender, EventArgs e)
        {
            DisplayFirst();
        }

        private void buttonIndicatoriCASCO_Click(object sender, EventArgs e)
        {
            FormFormIndicatoriCASCO casco = new FormFormIndicatoriCASCO();
            casco.TopLevel = false;
            if (panelAfisare.Controls.Count > 0)
                panelAfisare.Controls.Clear();
            casco.FormBorderStyle = FormBorderStyle.None;
            panelAfisare.Controls.Add(casco);
            casco.BringToFront();
            casco.Show();
        }

        private void buttonAsigurareRCA_Click(object sender, EventArgs e)
        {
            FormAdaugaRCA rca = new FormAdaugaRCA();
            rca.TopLevel = false;
            if (panelAfisare.Controls.Count > 0)
                panelAfisare.Controls.Clear();
            rca.FormBorderStyle = FormBorderStyle.None;
            panelAfisare.Controls.Add(rca);
            rca.BringToFront();
            rca.Show();
        }

        private void buttonAdaugaCASCO_Click(object sender, EventArgs e)
        {
            FormAdaugaCASCO rca = new FormAdaugaCASCO();
            rca.TopLevel = false;
            if (panelAfisare.Controls.Count > 0)
                panelAfisare.Controls.Clear();
            rca.FormBorderStyle = FormBorderStyle.None;
            panelAfisare.Controls.Add(rca);
            rca.BringToFront();
            rca.Show();
        }
        void VerificaValabilitateAsigurareCasco()
        {
            bool status_asigurare = false;
            List<Calcul_prima_Casco> listaAsigurariCasc = DatabaseAcces.ExtrageAsigurareCasco().Where(d => d.status_casco == true).ToList();
            foreach (Calcul_prima_Casco casc in listaAsigurariCasc)
            {
                DateTime expirare = Convert.ToDateTime(casc.Data_adaugare).AddYears(1);
                if (expirare <= DateTime.Today)
                {
                    DatabaseAcces.ModificaStatusAsigurareCasco(casc.Calcul_Casco_PrimaryKey, status_asigurare);
                }
            }
        }
        void VerificaValabilitateAsigurareRca()
        {
            bool status_asigurare = false;
            List<Calcul_prima_RCA> listaAsigurari = DatabaseAcces.ExtrageAsigurareRCA().Where(d => d.status_asigurare == true).ToList();
            foreach (Calcul_prima_RCA rca in listaAsigurari)
            {
                DateTime expirare = Convert.ToDateTime(rca.Data_adaugare).AddYears(1);
                if (expirare <= DateTime.Today)
                {
                    DatabaseAcces.ModificaStatusAsigurareRCAExpirare(rca.Calcul_RCA_PrimaryKey, status_asigurare);
                }
            }
        }
        string denumireDurata()
        {
            List<IncheiereRCA> listaRCA = DatabaseAcces.ExtrageRCA();
            List<DurataAsigurare> durata = DatabaseAcces.ExtrageDurataAsigurare().Where(d => d.status_durata == true && d.Tip_asigurare == "RCA").ToList();
            string den = string.Empty;
            foreach (IncheiereRCA rca in listaRCA)
            {
                foreach (DurataAsigurare dur in durata)
                {
                    if (rca.Id_durata == dur.Id_durata)
                    {
                        den = dur.Durata;
                    }
                }
            }
            return den;
        }
      

        int zileDurate()
        {
            int zile = 0;
            if (denumireDurata() == "6 luni")
            {
                zile = 183;
            }
            else
            {
                zile = 365;
            }
            return zile;
        }
        void ModificaStatusRCAExpirate()
        {
            List<IncheiereRCA> listaRCA = DatabaseAcces.ExtrageRCA();
            int zileScurse = 0;
            if (listaRCA.Count != 0)
            {
                foreach (IncheiereRCA rca in listaRCA)
                {
                    DateTime data_inceput = Convert.ToDateTime(rca.Data_inceput);
                    DateTime data_curenta = DateTime.Now;
                    TimeSpan diferenta = data_curenta.Subtract(data_inceput);
                    zileScurse = diferenta.Days;
                    if (zileScurse >= zileDurate())
                    {
                        DatabaseAcces.ModificaStatusRCA(rca.Cod_polita_primary_key, false);
                    }
                }
            }
            else
            {

            }
        }
        string denumireDurataCasco()
        {
            List<IncheiereCasco> listaCasco = DatabaseAcces.ExtragePoliteCasco();
            List<DurataAsigurare> durata = DatabaseAcces.ExtrageDurataAsigurare().Where(d => d.status_durata == true && d.Tip_asigurare == "CASCO").ToList();
            string den = string.Empty;
            foreach (IncheiereCasco casc in listaCasco)
            {
                foreach (DurataAsigurare dur in durata)
                {
                    if (casc.Id_durata == dur.Id_durata)
                    {
                        den = dur.Durata;
                    }
                }
            }
            return den;
        }
        int zileDurateCasco()
        {
            int zile = 0;
            if (denumireDurataCasco() == "6 luni")
            {
                zile = 183;
            }
            else
            {
                zile = 365;
            }
            return zile;
        }
        void ModificaStatusCascoExpirate()
        {
            List<IncheiereCasco> listaCasco = DatabaseAcces.ExtragePoliteCasco();
            int zileScurse = 0;
            if (listaCasco.Count != 0)
            {
                foreach (IncheiereCasco casc in listaCasco)
                {
                    DateTime data_inceput = Convert.ToDateTime(casc.Data_inceput);
                    DateTime data_curenta = DateTime.Now;
                    TimeSpan diferenta = data_curenta.Subtract(data_inceput);
                    zileScurse = diferenta.Days;
                    if (zileScurse >= zileDurateCasco())
                    {
                        DatabaseAcces.ModificaStatusCasco(casc.Cod_casco_primary_key, false);
                    }
                }
            }
            else
            {

            }
        }

        private void buttonCalculatorRCA_Click(object sender, EventArgs e)
        {
            FormCalculatorIncheiereRca rca = new FormCalculatorIncheiereRca();
            rca.TopLevel = false;
            if (panelAfisare.Controls.Count > 0)
                panelAfisare.Controls.Clear();
            rca.FormBorderStyle = FormBorderStyle.None;
            panelAfisare.Controls.Add(rca);
            rca.BringToFront();
            rca.Show();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            FormLogare frm = new FormLogare();
            this.Dispose();
            frm.ShowDialog();
        }

        void ChangeButtonColorBack(Button but)
        {
            but.BackColor = Color.SteelBlue;
            but.ForeColor = Color.White;
        }
        private void buttonIndicatoriRCA_MouseClick(object sender, MouseEventArgs e)
        {
            buttonIndicatoriRCA.BackColor = Color.AliceBlue;
            buttonIndicatoriRCA.ForeColor = Color.SteelBlue;
            ChangeButtonColorBack(buttonIndicatoriCASCO);
            ChangeButtonColorBack(buttonAsigurareRCA);
            ChangeButtonColorBack(buttonAdaugaCASCO);
            ChangeButtonColorBack(buttonCalculatorRCA);
            ChangeButtonColorBack(buttonAsigurareCasco);
            ChangeButtonColorBack(buttonVizualizare);
        }

        private void buttonIndicatoriCASCO_MouseClick(object sender, MouseEventArgs e)
        {
            buttonIndicatoriCASCO.BackColor = Color.AliceBlue;
            buttonIndicatoriCASCO.ForeColor = Color.SteelBlue;
            ChangeButtonColorBack(buttonIndicatoriRCA);
            ChangeButtonColorBack(buttonAsigurareRCA);
            ChangeButtonColorBack(buttonAdaugaCASCO);
            ChangeButtonColorBack(buttonCalculatorRCA);
            ChangeButtonColorBack(buttonAsigurareCasco);
            ChangeButtonColorBack(buttonVizualizare);
        }

        private void buttonAsigurareRCA_MouseClick(object sender, MouseEventArgs e)
        {
            buttonAsigurareRCA.BackColor = Color.AliceBlue;
            buttonAsigurareRCA.ForeColor = Color.SteelBlue;
            ChangeButtonColorBack(buttonIndicatoriRCA);
            ChangeButtonColorBack(buttonIndicatoriCASCO);
            ChangeButtonColorBack(buttonAdaugaCASCO);
            ChangeButtonColorBack(buttonCalculatorRCA);
            ChangeButtonColorBack(buttonAsigurareCasco);
            ChangeButtonColorBack(buttonVizualizare);
        }

        private void buttonAdaugaCASCO_MouseClick(object sender, MouseEventArgs e)
        {
            buttonAdaugaCASCO.BackColor = Color.AliceBlue;
            buttonAdaugaCASCO.ForeColor = Color.SteelBlue;
            ChangeButtonColorBack(buttonIndicatoriRCA);
            ChangeButtonColorBack(buttonIndicatoriCASCO);
            ChangeButtonColorBack(buttonAsigurareRCA);
            ChangeButtonColorBack(buttonCalculatorRCA);
            ChangeButtonColorBack(buttonAsigurareCasco);
            ChangeButtonColorBack(buttonVizualizare);
        }

        private void buttonCalculatorRCA_MouseClick(object sender, MouseEventArgs e)
        {
            buttonCalculatorRCA.BackColor = Color.AliceBlue;
            buttonCalculatorRCA.ForeColor = Color.SteelBlue;
            ChangeButtonColorBack(buttonIndicatoriRCA);
            ChangeButtonColorBack(buttonIndicatoriCASCO);
            ChangeButtonColorBack(buttonAsigurareRCA);
            ChangeButtonColorBack(buttonAdaugaCASCO);
            ChangeButtonColorBack(buttonAsigurareCasco);
            ChangeButtonColorBack(buttonVizualizare);
        }

        private void buttonAsigurareCasco_MouseClick(object sender, MouseEventArgs e)
        {
            buttonAsigurareCasco.BackColor = Color.AliceBlue;
            buttonAsigurareCasco.ForeColor = Color.SteelBlue;
            ChangeButtonColorBack(buttonIndicatoriRCA);
            ChangeButtonColorBack(buttonIndicatoriCASCO);
            ChangeButtonColorBack(buttonAsigurareRCA);
            ChangeButtonColorBack(buttonAdaugaCASCO);
            ChangeButtonColorBack(buttonCalculatorRCA);
            ChangeButtonColorBack(buttonVizualizare);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panelClose_MouseDown(object sender, MouseEventArgs e)
        {
            mousedown = true;
        }

        private void panelClose_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousedown)
            {
                int mousex = MousePosition.X - 900;
                int mousey = MousePosition.Y - 20;
                this.SetDesktopLocation(mousex, mousey);
            }
        }

        private void panelClose_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }

        private void buttonAsigurareCasco_Click(object sender, EventArgs e)
        {
            FormCalculatorIncheiereCasco casco = new FormCalculatorIncheiereCasco();
            casco.TopLevel = false;
            if (panelAfisare.Controls.Count > 0)
                panelAfisare.Controls.Clear();
            casco.FormBorderStyle = FormBorderStyle.None;
            panelAfisare.Controls.Add(casco);
            casco.BringToFront();
            casco.Show();
        }

        private void buttonVizualizare_Click(object sender, EventArgs e)
        {
            FormCautareAsigurariClient form = new FormCautareAsigurariClient();
            form.TopLevel = false;
            if (panelAfisare.Controls.Count > 0)
                panelAfisare.Controls.Clear();
            form.FormBorderStyle = FormBorderStyle.None;
            panelAfisare.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }

        private void buttonVizualizare_MouseClick(object sender, MouseEventArgs e)
        {
            buttonVizualizare.BackColor = Color.AliceBlue;
            buttonVizualizare.ForeColor = Color.SteelBlue;
            ChangeButtonColorBack(buttonIndicatoriRCA);
            ChangeButtonColorBack(buttonIndicatoriCASCO);
            ChangeButtonColorBack(buttonAsigurareRCA);
            ChangeButtonColorBack(buttonAdaugaCASCO);
            ChangeButtonColorBack(buttonAsigurareCasco);
            ChangeButtonColorBack(buttonAdaugaCASCO);
        }
    }
}
