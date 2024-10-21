
namespace Sistem_informatic_Asiguri_auto
{
    partial class FormBeneficiiSuplimentare
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBeneficiiSuplimentare));
            this.buttonAcasa = new System.Windows.Forms.Button();
            this.listBoxBeneficii = new System.Windows.Forms.ListBox();
            this.labelBeneficii = new System.Windows.Forms.Label();
            this.labelDenBeneficiu = new System.Windows.Forms.Label();
            this.labelProcent = new System.Windows.Forms.Label();
            this.textBoxDenumire = new System.Windows.Forms.TextBox();
            this.numericUpDownProcent = new System.Windows.Forms.NumericUpDown();
            this.buttonAdauga = new System.Windows.Forms.Button();
            this.buttonSterge = new System.Windows.Forms.Button();
            this.textBoxBeneficiiPachet = new System.Windows.Forms.TextBox();
            this.labelContPachet = new System.Windows.Forms.Label();
            this.panelBar = new System.Windows.Forms.Panel();
            this.buttonMinimize = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonMaximize = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcent)).BeginInit();
            this.panelBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAcasa
            // 
            this.buttonAcasa.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAcasa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAcasa.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonAcasa.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAcasa.Location = new System.Drawing.Point(406, 514);
            this.buttonAcasa.Name = "buttonAcasa";
            this.buttonAcasa.Size = new System.Drawing.Size(280, 47);
            this.buttonAcasa.TabIndex = 0;
            this.buttonAcasa.Text = "Acasa";
            this.buttonAcasa.UseVisualStyleBackColor = false;
            this.buttonAcasa.Click += new System.EventHandler(this.buttonAcasa_Click);
            // 
            // listBoxBeneficii
            // 
            this.listBoxBeneficii.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listBoxBeneficii.FormattingEnabled = true;
            this.listBoxBeneficii.HorizontalScrollbar = true;
            this.listBoxBeneficii.ItemHeight = 23;
            this.listBoxBeneficii.Location = new System.Drawing.Point(49, 116);
            this.listBoxBeneficii.Name = "listBoxBeneficii";
            this.listBoxBeneficii.ScrollAlwaysVisible = true;
            this.listBoxBeneficii.Size = new System.Drawing.Size(324, 441);
            this.listBoxBeneficii.TabIndex = 1;
            this.listBoxBeneficii.SelectedIndexChanged += new System.EventHandler(this.listBoxBeneficii_SelectedIndexChanged);
            // 
            // labelBeneficii
            // 
            this.labelBeneficii.AutoSize = true;
            this.labelBeneficii.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelBeneficii.Location = new System.Drawing.Point(49, 86);
            this.labelBeneficii.Name = "labelBeneficii";
            this.labelBeneficii.Size = new System.Drawing.Size(177, 27);
            this.labelBeneficii.TabIndex = 2;
            this.labelBeneficii.Text = "Denumire pachet";
            // 
            // labelDenBeneficiu
            // 
            this.labelDenBeneficiu.AutoSize = true;
            this.labelDenBeneficiu.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelDenBeneficiu.Location = new System.Drawing.Point(406, 90);
            this.labelDenBeneficiu.Name = "labelDenBeneficiu";
            this.labelDenBeneficiu.Size = new System.Drawing.Size(177, 27);
            this.labelDenBeneficiu.TabIndex = 3;
            this.labelDenBeneficiu.Text = "Denumire pachet";
            // 
            // labelProcent
            // 
            this.labelProcent.AutoSize = true;
            this.labelProcent.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelProcent.Location = new System.Drawing.Point(406, 284);
            this.labelProcent.Name = "labelProcent";
            this.labelProcent.Size = new System.Drawing.Size(159, 27);
            this.labelProcent.TabIndex = 4;
            this.labelProcent.Text = "Procent Pachet";
            // 
            // textBoxDenumire
            // 
            this.textBoxDenumire.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxDenumire.Location = new System.Drawing.Point(406, 116);
            this.textBoxDenumire.Name = "textBoxDenumire";
            this.textBoxDenumire.Size = new System.Drawing.Size(280, 32);
            this.textBoxDenumire.TabIndex = 5;
            // 
            // numericUpDownProcent
            // 
            this.numericUpDownProcent.DecimalPlaces = 1;
            this.numericUpDownProcent.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDownProcent.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownProcent.Location = new System.Drawing.Point(406, 314);
            this.numericUpDownProcent.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownProcent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownProcent.Name = "numericUpDownProcent";
            this.numericUpDownProcent.Size = new System.Drawing.Size(79, 32);
            this.numericUpDownProcent.TabIndex = 6;
            this.numericUpDownProcent.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonAdauga
            // 
            this.buttonAdauga.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAdauga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdauga.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonAdauga.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAdauga.Location = new System.Drawing.Point(406, 367);
            this.buttonAdauga.Name = "buttonAdauga";
            this.buttonAdauga.Size = new System.Drawing.Size(280, 47);
            this.buttonAdauga.TabIndex = 7;
            this.buttonAdauga.Text = "Adauga beneficiu";
            this.buttonAdauga.UseVisualStyleBackColor = false;
            this.buttonAdauga.Click += new System.EventHandler(this.buttonAdauga_Click);
            // 
            // buttonSterge
            // 
            this.buttonSterge.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonSterge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSterge.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSterge.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSterge.Location = new System.Drawing.Point(406, 441);
            this.buttonSterge.Name = "buttonSterge";
            this.buttonSterge.Size = new System.Drawing.Size(280, 47);
            this.buttonSterge.TabIndex = 8;
            this.buttonSterge.Text = "Sterge beneficiu";
            this.buttonSterge.UseVisualStyleBackColor = false;
            this.buttonSterge.Click += new System.EventHandler(this.buttonSterge_Click);
            // 
            // textBoxBeneficiiPachet
            // 
            this.textBoxBeneficiiPachet.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxBeneficiiPachet.Location = new System.Drawing.Point(406, 194);
            this.textBoxBeneficiiPachet.Multiline = true;
            this.textBoxBeneficiiPachet.Name = "textBoxBeneficiiPachet";
            this.textBoxBeneficiiPachet.Size = new System.Drawing.Size(280, 65);
            this.textBoxBeneficiiPachet.TabIndex = 9;
            // 
            // labelContPachet
            // 
            this.labelContPachet.AutoSize = true;
            this.labelContPachet.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelContPachet.Location = new System.Drawing.Point(406, 164);
            this.labelContPachet.Name = "labelContPachet";
            this.labelContPachet.Size = new System.Drawing.Size(169, 27);
            this.labelContPachet.TabIndex = 10;
            this.labelContPachet.Text = "Beneficii pachet";
            // 
            // panelBar
            // 
            this.panelBar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panelBar.Controls.Add(this.buttonMinimize);
            this.panelBar.Controls.Add(this.buttonExit);
            this.panelBar.Controls.Add(this.buttonMaximize);
            this.panelBar.Location = new System.Drawing.Point(0, -1);
            this.panelBar.Name = "panelBar";
            this.panelBar.Size = new System.Drawing.Size(749, 35);
            this.panelBar.TabIndex = 11;
            this.panelBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelBar_MouseDown);
            this.panelBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelBar_MouseMove);
            this.panelBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelBar_MouseUp);
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonMinimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMinimize.BackgroundImage")));
            this.buttonMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMinimize.FlatAppearance.BorderSize = 0;
            this.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMinimize.Location = new System.Drawing.Point(640, 3);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(28, 30);
            this.buttonMinimize.TabIndex = 14;
            this.buttonMinimize.UseVisualStyleBackColor = false;
            this.buttonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonExit.BackgroundImage")));
            this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Location = new System.Drawing.Point(708, 3);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(28, 30);
            this.buttonExit.TabIndex = 12;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonMaximize
            // 
            this.buttonMaximize.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonMaximize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMaximize.BackgroundImage")));
            this.buttonMaximize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMaximize.FlatAppearance.BorderSize = 0;
            this.buttonMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMaximize.Location = new System.Drawing.Point(674, 3);
            this.buttonMaximize.Name = "buttonMaximize";
            this.buttonMaximize.Size = new System.Drawing.Size(28, 30);
            this.buttonMaximize.TabIndex = 13;
            this.buttonMaximize.UseVisualStyleBackColor = false;
            // 
            // FormBeneficiiSuplimentare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(742, 583);
            this.Controls.Add(this.panelBar);
            this.Controls.Add(this.labelContPachet);
            this.Controls.Add(this.textBoxBeneficiiPachet);
            this.Controls.Add(this.buttonSterge);
            this.Controls.Add(this.buttonAdauga);
            this.Controls.Add(this.numericUpDownProcent);
            this.Controls.Add(this.textBoxDenumire);
            this.Controls.Add(this.labelProcent);
            this.Controls.Add(this.labelDenBeneficiu);
            this.Controls.Add(this.labelBeneficii);
            this.Controls.Add(this.listBoxBeneficii);
            this.Controls.Add(this.buttonAcasa);
            this.Name = "FormBeneficiiSuplimentare";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormBeneficiiSuplimentare";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcent)).EndInit();
            this.panelBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAcasa;
        private System.Windows.Forms.ListBox listBoxBeneficii;
        private System.Windows.Forms.Label labelBeneficii;
        private System.Windows.Forms.Label labelDenBeneficiu;
        private System.Windows.Forms.Label labelProcent;
        private System.Windows.Forms.TextBox textBoxDenumire;
        private System.Windows.Forms.NumericUpDown numericUpDownProcent;
        private System.Windows.Forms.Button buttonAdauga;
        private System.Windows.Forms.Button buttonSterge;
        private System.Windows.Forms.TextBox textBoxBeneficiiPachet;
        private System.Windows.Forms.Label labelContPachet;
        private System.Windows.Forms.Panel panelBar;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonMaximize;
        private System.Windows.Forms.Button buttonMinimize;
    }
}