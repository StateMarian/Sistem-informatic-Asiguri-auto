using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sistem_informatic_Asiguri_auto
{

    public partial class FormManager : Form
    {

        public FormManager(string id_angajat)
        {
            InitializeComponent();
            AdaugaNume(id_angajat);
            DisplayFirst();
            this.FormBorderStyle = FormBorderStyle.None;
            buttonGestionareAngajati.BackColor = Color.AliceBlue;
            buttonGestionareAngajati.ForeColor = Color.SteelBlue;
        }

        List<Angajat> listaAng = DatabaseAcces.ExtrageAngajati();
        public void AdaugaNume(string id_angajat)
        {
            DateAngajat.IdAngajat = id_angajat;
            foreach (Angajat ang in listaAng)
            {
                if (ang.Cod_angajat == DateAngajat.IdAngajat)
                {
                    labelNumeAngajat.Text = "Bine ai venit " + ang.FullName;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormLogare formL = new FormLogare();
            this.Dispose();
            formL.ShowDialog();
        }
        void DisplayFirst()
        {
            FormGestionareAngajati formG = new FormGestionareAngajati();
            formG.TopLevel = false;
            if (panelGestionare.Controls.Count > 0)
                panelGestionare.Controls.Clear();
            formG.FormBorderStyle = FormBorderStyle.None;
            panelGestionare.Controls.Add(formG);
            formG.BringToFront();
            formG.Show();
            //this.Dispose();
            //formG.ShowDialog();
        }
        private void buttonGestionareAngajati_Click(object sender, EventArgs e)
        {
            DisplayFirst();
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
                int mousex = MousePosition.X - 653;
                int mousey = MousePosition.Y - 17;
                this.SetDesktopLocation(mousex, mousey);
            }
        }
        //In caz ca se mai adauga alte butoane cu functionalitati in meniu
        void ChangeButtonColorBack(Button but)
        {
            but.BackColor = Color.SteelBlue;
            but.ForeColor = Color.White;
        }
        private void panelBar_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }

        private void buttonGestionareAngajati_MouseClick(object sender, MouseEventArgs e)
        {
            buttonGestionareAngajati.BackColor = Color.AliceBlue;
            buttonGestionareAngajati.ForeColor = Color.SteelBlue;
        }
    }
}