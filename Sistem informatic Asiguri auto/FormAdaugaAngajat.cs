using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Sistem_informatic_Asiguri_auto
{
    public partial class FormAdaugaAngajat : Form
    {
        public FormAdaugaAngajat()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        List<Angajat> listaAng = DatabaseAcces.ExtrageAngajati();

        private void buttonBack_Click(object sender, EventArgs e)
        {
            FormManager form = new FormManager(DateAngajat.IdAngajat);
            this.Dispose();
            form.ShowDialog();
        }

        string codificare_cheie()
        {
            string cheie = string.Empty;
            string[] prenume = textboxPrenume.Text.Split('-');
            string nume = textBoxNume.Text;
            int contor = 1;
            string copieCheie = string.Empty;
            cheie = cheie + nume.Substring(0, 1);

            if (prenume.Length == 1)
            {
                foreach (string pren in prenume)
                {
                    cheie = cheie + pren.Substring(0, 1);
                    copieCheie = cheie;
                    cheie = cheie + contor;
                }

                foreach (Angajat ang in listaAng)
                {
                    if (ang.Cod_angajat.ToUpper() == cheie)
                    {
                        cheie = copieCheie;
                        contor++;
                        cheie = cheie + contor;
                    }
                }
                return cheie;
            }
            else
            {
                foreach (string pren in prenume)
                {
                    cheie = cheie + pren.Substring(0, 1);
                    copieCheie = cheie;
                }

                cheie = cheie + contor;
                foreach (Angajat ang in listaAng)
                {
                    if (ang.Cod_angajat.ToUpper() == cheie)
                    {
                        cheie = copieCheie;
                        contor++;
                        cheie = cheie + contor;
                    }
                }
                return cheie;
            }
        }
        string CodificareParola()
        {
            string parola = string.Empty;
            parola = parola + textBoxCnp.Text.Substring(0, 3) + textBoxCnp.Text.Substring(10, 3)
                + textBoxNume.Text.Substring(0, 1) + textboxPrenume.Text.Substring(0, 1);
            return parola;
        }

        void VisibleControls()
        {
            textBoxEmail.Visible = true;
            textBoxNume.Visible = true;
            textboxPrenume.Visible = true;
            textboxTelefon.Visible = true;
            textBoxTipAngajat.Visible = true;
            labelEmail.Visible = true;
            labelNrTelefon.Visible = true;
            labelPrenume.Visible = true;
            labelNume.Visible = true;
            labelTo_angajat.Visible = true;
            buttonAdaugaAngajat.Visible = true;
            buttonCautaAngajat.Visible = false;
        }
        void HideControls()
        {
            textBoxEmail.Visible = false;
            textBoxNume.Visible = false;
            textboxPrenume.Visible = false;
            textboxTelefon.Visible = false;
            textBoxTipAngajat.Visible = false;
            labelEmail.Visible = false;
            labelNrTelefon.Visible = false;
            labelPrenume.Visible = false;
            labelNume.Visible = false;
            labelTo_angajat.Visible = false;
            buttonAdaugaAngajat.Visible = false;
            buttonCautaAngajat.Visible = false;
        }
        void SendEmail(string parola,string email)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient sc = new SmtpClient("smtp.gmail.com");
                message.From = new MailAddress("state719@gmail.com");
                message.To.Add(email);
                message.Subject = ("Parola cont");
                message.Body = "Parola contului dumneavoastra este " + parola;
                sc.Port = 587;
                sc.Credentials = new NetworkCredential("state719@gmail.com", "suyu rfsm qoqg ncfq");
                sc.EnableSsl = true;
                sc.Send(message);
                MessageBox.Show("Emailul cu parola creata a fost trimisa angajatului!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void buttonAdaugaAngajat_Click(object sender, EventArgs e)
        {
            if (!Verificari.checkName(textBoxNume.Text) || string.IsNullOrEmpty(textBoxNume.Text))
            {
                MessageBox.Show("Numele introdus nu este corect!");
            }
            else
            {
                if (!Verificari.checkName(textboxPrenume.Text) || string.IsNullOrEmpty(textboxPrenume.Text))
                {
                    MessageBox.Show("Prenumele introdus nu este corect!");
                }
                else
                {
                    if (!(Verificari.checkCnp(textBoxCnp.Text)) || textBoxCnp.Text.Length != 13)
                    {
                        MessageBox.Show("Cnp-ul introdus nu este corect");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(textBoxEmail.Text) || !Verificari.checkEmail(textBoxEmail.Text))
                        {
                            MessageBox.Show("Emailul introdus este gresit!");
                        }
                        else
                        {
                            if (!(Verificari.checkCnp(textboxTelefon.Text)) || textboxTelefon.Text.Length != 10)
                            {
                                MessageBox.Show("Numarul de telefon este invalid!");
                            }
                            else
                            {
                                if (!Verificari.checkRoleEmployee(textBoxTipAngajat.Text))
                                {
                                    MessageBox.Show("Tipul angajatului introdus este gresit!");
                                }
                                else
                                {
                                    {
                                        DialogResult dialog = MessageBox.Show("Sigur doriti sa adaugati angajatul?", "Confirmare", MessageBoxButtons.YesNo);
                                        if (dialog == DialogResult.Yes)
                                        {
                                            Angajat ang = new Angajat()
                                            {
                                                Cod_angajat = codificare_cheie(),
                                                Nume = textBoxNume.Text,
                                                Prenume = textboxPrenume.Text,
                                                Cnp = textBoxCnp.Text,
                                                Email = textBoxEmail.Text,
                                                Nr_telefon = textboxTelefon.Text,
                                                Data_angajare = Convert.ToString(DateTime.Now),
                                                Tip_angajat = textBoxTipAngajat.Text.ToUpper(),
                                                Parola = CodificareParola(),
                                                data_concediere = null,
                                                status = true
                                            };
                                            listaAng.Add(ang);
                                            DatabaseAcces.AddAngajat(ang);
                                            SendEmail(ang.Parola, textBoxEmail.Text);
                                            FormManager form = new FormManager(DateAngajat.IdAngajat);
                                            this.Dispose();
                                            form.ShowDialog();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Adaugare anulata!");
                                            FormManager form = new FormManager(DateAngajat.IdAngajat);
                                            this.Dispose();
                                            form.ShowDialog();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        void VisibleLabel()
        {
            labelDnume.Visible = true;
            labelDprenume.Visible = true;
            labeDEmail.Visible = true;
            labelDTelefon.Visible = true;
            labelData_angajate.Visible = true;
            labelDtip_angajat.Visible = true;
            labelNumeEXIST.Visible = true;
            labelPrenumeExist.Visible = true;
            labelEmailexist.Visible = true;
            labelTelefonExist.Visible = true;
            labelDataexist.Visible = true;
            labelTipExist.Visible = true;
            buttonReangajre.Visible = true;
        }
        void HideLabel()
        {
            labelDnume.Visible = false;
            labelDprenume.Visible = false;
            labeDEmail.Visible = false;
            labelDTelefon.Visible = false;
            labelData_angajate.Visible = false;
            labelDtip_angajat.Visible = false;
            labelNumeEXIST.Visible = false;
            labelPrenumeExist.Visible = false;
            labelEmailexist.Visible = false;
            labelTelefonExist.Visible = false;
            labelDataexist.Visible = false;
            labelTipExist.Visible = false;
            buttonReangajre.Visible = false;
        }
        string id_ang = string.Empty;
        private void buttonCautaAngajat_Click(object sender, EventArgs e)
        {
            if (Verificari.checkCnp(textBoxCnp.Text) && textBoxCnp.Text.Length == 13 && !(string.IsNullOrEmpty(textBoxCnp.Text)))
            {
                if (listaAng.Any(a => a.Cnp == textBoxCnp.Text))
                {
                    foreach (Angajat ang in listaAng)
                    {
                        if (ang.Cnp == textBoxCnp.Text)
                        {
                            if(ang.status==false)
                            {
                                id_ang = ang.Cod_angajat;
                            VisibleLabel();
                            HideControls();
                            buttonCautaAngajat.Visible = true;
                            labelNumeEXIST.Text = ang.Nume;
                            labelPrenumeExist.Text = ang.Prenume;
                            labelEmailexist.Text = ang.Email;
                            labelTelefonExist.Text = ang.Nr_telefon;
                            labelDataexist.Text = Convert.ToDateTime(ang.Data_angajare).ToShortDateString();
                            labelTipExist.Text = ang.Tip_angajat;
                            }
                            else
                            {
                                MessageBox.Show("Persoana cautată este deja angajat al companiei!");                             
                            }
                            break;
                        }         
                    }
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Nu exista angajat cu acest cnp, doriti sa adaugati un nou angajat", "Confirmare", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        VisibleControls();
                        HideLabel();
                        buttonCautaAngajat.Visible = true;
                    }
                    else
                    {
                        FormManager form = new FormManager(DateAngajat.IdAngajat);
                        this.Dispose();
                        form.ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show("Introduceti cnp in format corect, campul nu trebuie sa fie gol.");
            }
        }

        private void buttonReangajre_Click(object sender, EventArgs e)
        {
            bool status = true;
            string data_angajare = Convert.ToString(DateTime.Now);
            DialogResult dialog = MessageBox.Show("Sigur doriti sa reangajati angajatul?", "Confirmare", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                MessageBox.Show("Reangajarea a fost realizata cu succes!");
                DatabaseAcces.StatusAngajat(id_ang, data_angajare, status);
                FormManager form = new FormManager(DateAngajat.IdAngajat);
                this.Dispose();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Reangajarea a fost anulata!");
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
                int mousex = MousePosition.X - 495;
                int mousey = MousePosition.Y - 17;
                this.SetDesktopLocation(mousex, mousey);
            }
        }

        private void panelBar_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }
    }
}
