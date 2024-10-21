using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sistem_informatic_Asiguri_auto
{
    public partial class FormPrevizualizareRca : Form
    {
        Button button;
        Button buttonPrev;
        public FormPrevizualizareRca(Image img,Button btn,Button btnprev)
        {
            InitializeComponent();
            imga(img);
            button = btn;
            buttonPrev = btnprev;
        }

        void imga(Image img)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = img;
        }
        void ActivareButton(Button btn)
        {
            btn.Enabled = true;
            buttonPrev.Enabled = false;
        }

        private void buttonValidare_Click(object sender, EventArgs e)
        {
            ActivareButton(button);
            this.Dispose();
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
                int mousex = MousePosition.X - 436;
                int mousey = MousePosition.Y - 17;
                this.SetDesktopLocation(mousex, mousey);
            }
        }

        private void panelBar_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
