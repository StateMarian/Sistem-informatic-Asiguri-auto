
namespace Sistem_informatic_Asiguri_auto
{
    partial class FormZonaGeografica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormZonaGeografica));
            this.buttonAcasa = new System.Windows.Forms.Button();
            this.labelZonaGeografica = new System.Windows.Forms.Label();
            this.labelProcent = new System.Windows.Forms.Label();
            this.labelTipAsigurare = new System.Windows.Forms.Label();
            this.numericUpDownProcent = new System.Windows.Forms.NumericUpDown();
            this.comboBoxtipasigurare = new System.Windows.Forms.ComboBox();
            this.buttonAdauga = new System.Windows.Forms.Button();
            this.buttonSterge = new System.Windows.Forms.Button();
            this.comboBoxZone = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panelBar = new System.Windows.Forms.Panel();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonMaximize = new System.Windows.Forms.Button();
            this.buttonMinimize = new System.Windows.Forms.Button();
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
            this.buttonAcasa.Location = new System.Drawing.Point(319, 262);
            this.buttonAcasa.Name = "buttonAcasa";
            this.buttonAcasa.Size = new System.Drawing.Size(253, 55);
            this.buttonAcasa.TabIndex = 0;
            this.buttonAcasa.Text = "Acasa";
            this.buttonAcasa.UseVisualStyleBackColor = false;
            this.buttonAcasa.Click += new System.EventHandler(this.buttonAcasa_Click);
            // 
            // labelZonaGeografica
            // 
            this.labelZonaGeografica.AutoSize = true;
            this.labelZonaGeografica.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelZonaGeografica.Location = new System.Drawing.Point(29, 62);
            this.labelZonaGeografica.Name = "labelZonaGeografica";
            this.labelZonaGeografica.Size = new System.Drawing.Size(167, 27);
            this.labelZonaGeografica.TabIndex = 2;
            this.labelZonaGeografica.Text = "Zone geografice";
            // 
            // labelProcent
            // 
            this.labelProcent.AutoSize = true;
            this.labelProcent.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelProcent.Location = new System.Drawing.Point(31, 255);
            this.labelProcent.Name = "labelProcent";
            this.labelProcent.Size = new System.Drawing.Size(87, 27);
            this.labelProcent.TabIndex = 4;
            this.labelProcent.Text = "Procent";
            // 
            // labelTipAsigurare
            // 
            this.labelTipAsigurare.AutoSize = true;
            this.labelTipAsigurare.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTipAsigurare.Location = new System.Drawing.Point(29, 175);
            this.labelTipAsigurare.Name = "labelTipAsigurare";
            this.labelTipAsigurare.Size = new System.Drawing.Size(136, 27);
            this.labelTipAsigurare.TabIndex = 5;
            this.labelTipAsigurare.Text = "Tip asigurare";
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
            this.numericUpDownProcent.Location = new System.Drawing.Point(31, 285);
            this.numericUpDownProcent.Maximum = new decimal(new int[] {
            75,
            0,
            0,
            0});
            this.numericUpDownProcent.Name = "numericUpDownProcent";
            this.numericUpDownProcent.Size = new System.Drawing.Size(79, 32);
            this.numericUpDownProcent.TabIndex = 8;
            // 
            // comboBoxtipasigurare
            // 
            this.comboBoxtipasigurare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxtipasigurare.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxtipasigurare.FormattingEnabled = true;
            this.comboBoxtipasigurare.Location = new System.Drawing.Point(29, 205);
            this.comboBoxtipasigurare.Name = "comboBoxtipasigurare";
            this.comboBoxtipasigurare.Size = new System.Drawing.Size(259, 31);
            this.comboBoxtipasigurare.TabIndex = 9;
            this.comboBoxtipasigurare.SelectedIndexChanged += new System.EventHandler(this.comboBoxtipasigurare_SelectedIndexChanged);
            // 
            // buttonAdauga
            // 
            this.buttonAdauga.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAdauga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdauga.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonAdauga.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAdauga.Location = new System.Drawing.Point(319, 92);
            this.buttonAdauga.Name = "buttonAdauga";
            this.buttonAdauga.Size = new System.Drawing.Size(253, 55);
            this.buttonAdauga.TabIndex = 10;
            this.buttonAdauga.Text = "Adauga zona";
            this.buttonAdauga.UseVisualStyleBackColor = false;
            this.buttonAdauga.Click += new System.EventHandler(this.buttonAdauga_Click);
            // 
            // buttonSterge
            // 
            this.buttonSterge.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonSterge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSterge.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSterge.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSterge.Location = new System.Drawing.Point(317, 175);
            this.buttonSterge.Name = "buttonSterge";
            this.buttonSterge.Size = new System.Drawing.Size(255, 55);
            this.buttonSterge.TabIndex = 11;
            this.buttonSterge.Text = "Sterge zona";
            this.buttonSterge.UseVisualStyleBackColor = false;
            this.buttonSterge.Click += new System.EventHandler(this.buttonSterge_Click);
            // 
            // comboBoxZone
            // 
            this.comboBoxZone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxZone.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxZone.FormattingEnabled = true;
            this.comboBoxZone.Location = new System.Drawing.Point(31, 94);
            this.comboBoxZone.Name = "comboBoxZone";
            this.comboBoxZone.Size = new System.Drawing.Size(257, 31);
            this.comboBoxZone.TabIndex = 12;
            this.comboBoxZone.SelectedIndexChanged += new System.EventHandler(this.comboBoxZone_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox1.Location = new System.Drawing.Point(31, 131);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(182, 27);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Adauga zona noua";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panelBar
            // 
            this.panelBar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panelBar.Controls.Add(this.buttonMinimize);
            this.panelBar.Controls.Add(this.buttonExit);
            this.panelBar.Controls.Add(this.buttonMaximize);
            this.panelBar.Location = new System.Drawing.Point(-1, -1);
            this.panelBar.Name = "panelBar";
            this.panelBar.Size = new System.Drawing.Size(606, 35);
            this.panelBar.TabIndex = 14;
            this.panelBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelBar_MouseDown);
            this.panelBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelBar_MouseMove);
            this.panelBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelBar_MouseUp);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonExit.BackgroundImage")));
            this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Location = new System.Drawing.Point(562, 3);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(28, 30);
            this.buttonExit.TabIndex = 15;
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
            this.buttonMaximize.Location = new System.Drawing.Point(528, 3);
            this.buttonMaximize.Name = "buttonMaximize";
            this.buttonMaximize.Size = new System.Drawing.Size(28, 30);
            this.buttonMaximize.TabIndex = 16;
            this.buttonMaximize.UseVisualStyleBackColor = false;
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonMinimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMinimize.BackgroundImage")));
            this.buttonMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMinimize.FlatAppearance.BorderSize = 0;
            this.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMinimize.Location = new System.Drawing.Point(494, 3);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(28, 30);
            this.buttonMinimize.TabIndex = 17;
            this.buttonMinimize.UseVisualStyleBackColor = false;
            this.buttonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
            // 
            // FormZonaGeografica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(596, 338);
            this.Controls.Add(this.panelBar);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.comboBoxZone);
            this.Controls.Add(this.buttonSterge);
            this.Controls.Add(this.buttonAdauga);
            this.Controls.Add(this.comboBoxtipasigurare);
            this.Controls.Add(this.numericUpDownProcent);
            this.Controls.Add(this.labelTipAsigurare);
            this.Controls.Add(this.labelProcent);
            this.Controls.Add(this.labelZonaGeografica);
            this.Controls.Add(this.buttonAcasa);
            this.Name = "FormZonaGeografica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormZonaGeografica";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcent)).EndInit();
            this.panelBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAcasa;
        private System.Windows.Forms.Label labelZonaGeografica;
        private System.Windows.Forms.Label labelProcent;
        private System.Windows.Forms.Label labelTipAsigurare;
        private System.Windows.Forms.NumericUpDown numericUpDownProcent;
        private System.Windows.Forms.ComboBox comboBoxtipasigurare;
        private System.Windows.Forms.Button buttonAdauga;
        private System.Windows.Forms.Button buttonSterge;
        private System.Windows.Forms.ComboBox comboBoxZone;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panelBar;
        private System.Windows.Forms.Button buttonMinimize;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonMaximize;
    }
}