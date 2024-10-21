using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sistem_informatic_Asiguri_auto
{
    public partial class FormIndicatoriRCA : Form
    {
        public FormIndicatoriRCA()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void buttonCategorii_Click(object sender, EventArgs e)
        {
            FormCategoriiAuto frm = new FormCategoriiAuto();
            ParentForm.Hide();
            frm.ShowDialog();
        }

        private void buttonSubcategorie_Click(object sender, EventArgs e)
        {
            FormAdaugaSubcategorie frm = new FormAdaugaSubcategorie();
            ParentForm.Hide();
            frm.ShowDialog();
        }

        private void buttonCapacitate_Click(object sender, EventArgs e)
        {
            FormCapacitateCilindrica frm = new FormCapacitateCilindrica();
            ParentForm.Hide();
            frm.ShowDialog();
        }

        private void buttonGrupeVarsta_Click(object sender, EventArgs e)
        {
            FormGrupeVarsta form = new FormGrupeVarsta();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void buttonBonusMalus_Click(object sender, EventArgs e)
        {
            FormBonusMalus form = new FormBonusMalus();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void buttonZonageografica_Click(object sender, EventArgs e)
        {
            FormZonaGeografica form = new FormZonaGeografica();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void buttonBeneficiiSuplimentare_Click(object sender, EventArgs e)
        {
            FormBeneficiiSuplimentare form = new FormBeneficiiSuplimentare();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void buttonDiscount_Click(object sender, EventArgs e)
        {
            FormDiscountRca form = new FormDiscountRca();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void buttonDomeniuUtilizare_Click(object sender, EventArgs e)
        {
            FormDomeniuUtilizare form = new FormDomeniuUtilizare();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void buttonIndicatoriSuplimentari_Click(object sender, EventArgs e)
        {
            FormIndicatoriSuplimentari form = new FormIndicatoriSuplimentari();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormIndicatoriGrupaCapacitate form = new FormIndicatoriGrupaCapacitate();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void buttonIndicatoriGrupa_Click(object sender, EventArgs e)
        {
            FormIndicatoriGrupaCapacitate form = new FormIndicatoriGrupaCapacitate();
            ParentForm.Hide();
            form.ShowDialog();
        }

        private void buttonDurata_Click(object sender, EventArgs e)
        {
            FormAdaugaDurate form = new FormAdaugaDurate();
            ParentForm.Hide();
            form.ShowDialog();
        }
    }
}
