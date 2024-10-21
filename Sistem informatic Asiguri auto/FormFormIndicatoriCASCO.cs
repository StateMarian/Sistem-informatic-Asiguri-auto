using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sistem_informatic_Asiguri_auto
{
    public partial class FormFormIndicatoriCASCO : Form
    {
        public FormFormIndicatoriCASCO()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void buttonFrasiza_Click(object sender, EventArgs e)
        {
            FormFransiza frm = new FormFransiza();
            ParentForm.Hide();
            frm.ShowDialog();
        }
        private void buttonTipCasco_Click(object sender, EventArgs e)
        {
            FormTipCasco frm = new FormTipCasco();
            ParentForm.Hide();
            frm.ShowDialog();
        }

        private void buttonRiscuri_Click(object sender, EventArgs e)
        {
            FormRiscuriCasco form = new FormRiscuriCasco();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void buttonClauze_Click(object sender, EventArgs e)
        {
            FormClauzeSuplimentare form = new FormClauzeSuplimentare();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAsociereRiscCasco form = new FormAsociereRiscCasco();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void buttonAsociereRiscCasco_Click(object sender, EventArgs e)
        {
            FormAsociereRiscCasco form = new FormAsociereRiscCasco();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void buttonAsociereClauzaCasco_Click(object sender, EventArgs e)
        {
            FormAsociereCascoClauze form = new FormAsociereCascoClauze();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void buttonMarca_Click(object sender, EventArgs e)
        {
            FormMarca form = new FormMarca();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void buttonModele_Click(object sender, EventArgs e)
        {
            FormModel form = new FormModel();
            ParentForm.Hide();
            form.ShowDialog();
        }
    }
}
