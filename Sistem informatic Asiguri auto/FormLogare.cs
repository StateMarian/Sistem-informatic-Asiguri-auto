using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistem_informatic_Asiguri_auto
{
    public partial class FormLogare : Form
    {
        bool mousedown;
        public FormLogare()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        List<Angajat> listaAng = DatabaseAcces.ExtrageAngajati();

        private void buttonLogare_Click(object sender, EventArgs e)
        {
            bool isLogged = false;

            foreach(Angajat ang in listaAng)
            {
                if (textboxEmail.Text.ToLower() == ang.Email.ToLower() && textBoxPassword.Text.ToLower()==ang.Parola.ToLower())
                {
                    isLogged = true;
                    if (ang.Tip_angajat.ToUpper() == "MANAGER")
                    {
                        FormManager formM = new FormManager(ang.Cod_angajat);
                        this.Hide();
                        formM.ShowDialog();
                        break;
                    }
                    
                    
                    if(ang.Tip_angajat.ToUpper()=="ANGAJAT")
                    {
                        FormAngajat formA = new FormAngajat(ang.Cod_angajat);
                        this.Hide();
                        formA.ShowDialog();
                        break;
                    }  
                }
            }
            if (!isLogged)
            {
                if(textboxEmail.Text.Trim()=="")
                {
                    MessageBox.Show("Adresa de email este obligatorie pentru conectare!");
                }
                else
                {
                    if(textBoxPassword.Text.Trim()=="")
                    {
                        MessageBox.Show("Parola este obligatorie pentru conectare!");
                    }
                    else
                    {
                        MessageBox.Show("Credentialele introduse sunt invalide!!!!");
                    }
                }    
            }

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mousedown = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousedown)
            {
                int mousex = MousePosition.X - 400;
                int mousey = MousePosition.Y - 20;
                this.SetDesktopLocation(mousex, mousey);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }

        private void checkBoxVeziParola_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxVeziParola.Checked)
            {
                textBoxPassword.PasswordChar = '\0';
            }
            else
            {
                textBoxPassword.PasswordChar = '●';
            }
        }   

    }
}
