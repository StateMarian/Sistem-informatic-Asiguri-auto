
namespace Sistem_informatic_Asiguri_auto
{
    partial class FormAsociereCascoClauze
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAsociereCascoClauze));
            this.listBoxClauzeSuplimentare = new System.Windows.Forms.ListBox();
            this.numericUpDownValoareClauza = new System.Windows.Forms.NumericUpDown();
            this.labelVal = new System.Windows.Forms.Label();
            this.labelClauze = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonAcasa = new System.Windows.Forms.Button();
            this.labelTip = new System.Windows.Forms.Label();
            this.comboBoxTpcCasco = new System.Windows.Forms.ComboBox();
            this.comboBoxDenFran = new System.Windows.Forms.ComboBox();
            this.comboBoxProcFran = new System.Windows.Forms.ComboBox();
            this.comboBoxProcRedFran = new System.Windows.Forms.ComboBox();
            this.labelFransiza = new System.Windows.Forms.Label();
            this.labelproc = new System.Windows.Forms.Label();
            this.labelProcred = new System.Windows.Forms.Label();
            this.panelBar = new System.Windows.Forms.Panel();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonMaximize = new System.Windows.Forms.Button();
            this.buttonMinimize = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValoareClauza)).BeginInit();
            this.panelBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxClauzeSuplimentare
            // 
            this.listBoxClauzeSuplimentare.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listBoxClauzeSuplimentare.FormattingEnabled = true;
            this.listBoxClauzeSuplimentare.HorizontalScrollbar = true;
            this.listBoxClauzeSuplimentare.ItemHeight = 23;
            this.listBoxClauzeSuplimentare.Location = new System.Drawing.Point(335, 108);
            this.listBoxClauzeSuplimentare.Name = "listBoxClauzeSuplimentare";
            this.listBoxClauzeSuplimentare.ScrollAlwaysVisible = true;
            this.listBoxClauzeSuplimentare.Size = new System.Drawing.Size(445, 349);
            this.listBoxClauzeSuplimentare.TabIndex = 1;
            // 
            // numericUpDownValoareClauza
            // 
            this.numericUpDownValoareClauza.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDownValoareClauza.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownValoareClauza.Location = new System.Drawing.Point(58, 425);
            this.numericUpDownValoareClauza.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownValoareClauza.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownValoareClauza.Name = "numericUpDownValoareClauza";
            this.numericUpDownValoareClauza.Size = new System.Drawing.Size(238, 32);
            this.numericUpDownValoareClauza.TabIndex = 2;
            this.numericUpDownValoareClauza.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelVal
            // 
            this.labelVal.AutoSize = true;
            this.labelVal.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelVal.Location = new System.Drawing.Point(58, 395);
            this.labelVal.Name = "labelVal";
            this.labelVal.Size = new System.Drawing.Size(157, 27);
            this.labelVal.TabIndex = 3;
            this.labelVal.Text = "Valoare clauza:";
            // 
            // labelClauze
            // 
            this.labelClauze.AutoSize = true;
            this.labelClauze.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelClauze.Location = new System.Drawing.Point(335, 78);
            this.labelClauze.Name = "labelClauze";
            this.labelClauze.Size = new System.Drawing.Size(207, 27);
            this.labelClauze.TabIndex = 5;
            this.labelClauze.Text = "Clauze suplimentare";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SteelBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(58, 484);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(238, 61);
            this.button1.TabIndex = 8;
            this.button1.Text = "Asociere casco clauza";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.buttonAsociere_Click);
            // 
            // buttonAcasa
            // 
            this.buttonAcasa.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAcasa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAcasa.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonAcasa.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAcasa.Location = new System.Drawing.Point(565, 484);
            this.buttonAcasa.Name = "buttonAcasa";
            this.buttonAcasa.Size = new System.Drawing.Size(215, 61);
            this.buttonAcasa.TabIndex = 9;
            this.buttonAcasa.Text = "Acasa";
            this.buttonAcasa.UseVisualStyleBackColor = false;
            this.buttonAcasa.Click += new System.EventHandler(this.buttonAcasa_Click);
            // 
            // labelTip
            // 
            this.labelTip.AutoSize = true;
            this.labelTip.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTip.Location = new System.Drawing.Point(58, 78);
            this.labelTip.Name = "labelTip";
            this.labelTip.Size = new System.Drawing.Size(138, 27);
            this.labelTip.TabIndex = 4;
            this.labelTip.Text = "Pachet casco";
            // 
            // comboBoxTpcCasco
            // 
            this.comboBoxTpcCasco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTpcCasco.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxTpcCasco.FormattingEnabled = true;
            this.comboBoxTpcCasco.Location = new System.Drawing.Point(58, 108);
            this.comboBoxTpcCasco.Name = "comboBoxTpcCasco";
            this.comboBoxTpcCasco.Size = new System.Drawing.Size(238, 31);
            this.comboBoxTpcCasco.TabIndex = 10;
            this.comboBoxTpcCasco.SelectedIndexChanged += new System.EventHandler(this.comboBoxTpcCasco_SelectedIndexChanged);
            // 
            // comboBoxDenFran
            // 
            this.comboBoxDenFran.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDenFran.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxDenFran.FormattingEnabled = true;
            this.comboBoxDenFran.Location = new System.Drawing.Point(58, 181);
            this.comboBoxDenFran.Name = "comboBoxDenFran";
            this.comboBoxDenFran.Size = new System.Drawing.Size(238, 31);
            this.comboBoxDenFran.TabIndex = 11;
            this.comboBoxDenFran.SelectedIndexChanged += new System.EventHandler(this.comboBoxDenFran_SelectedIndexChanged);
            // 
            // comboBoxProcFran
            // 
            this.comboBoxProcFran.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcFran.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxProcFran.FormattingEnabled = true;
            this.comboBoxProcFran.Location = new System.Drawing.Point(58, 259);
            this.comboBoxProcFran.Name = "comboBoxProcFran";
            this.comboBoxProcFran.Size = new System.Drawing.Size(238, 31);
            this.comboBoxProcFran.TabIndex = 12;
            this.comboBoxProcFran.SelectedIndexChanged += new System.EventHandler(this.comboBoxProcFran_SelectedIndexChanged);
            // 
            // comboBoxProcRedFran
            // 
            this.comboBoxProcRedFran.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcRedFran.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxProcRedFran.FormattingEnabled = true;
            this.comboBoxProcRedFran.Location = new System.Drawing.Point(58, 337);
            this.comboBoxProcRedFran.Name = "comboBoxProcRedFran";
            this.comboBoxProcRedFran.Size = new System.Drawing.Size(238, 31);
            this.comboBoxProcRedFran.TabIndex = 13;
            // 
            // labelFransiza
            // 
            this.labelFransiza.AutoSize = true;
            this.labelFransiza.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelFransiza.Location = new System.Drawing.Point(58, 155);
            this.labelFransiza.Name = "labelFransiza";
            this.labelFransiza.Size = new System.Drawing.Size(80, 23);
            this.labelFransiza.TabIndex = 14;
            this.labelFransiza.Text = "Fransiza";
            // 
            // labelproc
            // 
            this.labelproc.AutoSize = true;
            this.labelproc.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelproc.Location = new System.Drawing.Point(58, 233);
            this.labelproc.Name = "labelproc";
            this.labelproc.Size = new System.Drawing.Size(144, 23);
            this.labelproc.TabIndex = 15;
            this.labelproc.Text = "Procent fransiza";
            // 
            // labelProcred
            // 
            this.labelProcred.AutoSize = true;
            this.labelProcred.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelProcred.Location = new System.Drawing.Point(58, 311);
            this.labelProcred.Name = "labelProcred";
            this.labelProcred.Size = new System.Drawing.Size(220, 23);
            this.labelProcred.TabIndex = 16;
            this.labelProcred.Text = "Procent reducere fransiza";
            // 
            // panelBar
            // 
            this.panelBar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panelBar.Controls.Add(this.buttonMinimize);
            this.panelBar.Controls.Add(this.buttonExit);
            this.panelBar.Controls.Add(this.buttonMaximize);
            this.panelBar.Location = new System.Drawing.Point(0, -1);
            this.panelBar.Name = "panelBar";
            this.panelBar.Size = new System.Drawing.Size(838, 35);
            this.panelBar.TabIndex = 17;
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
            this.buttonExit.Location = new System.Drawing.Point(795, 3);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(28, 30);
            this.buttonExit.TabIndex = 18;
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
            this.buttonMaximize.Location = new System.Drawing.Point(761, 3);
            this.buttonMaximize.Name = "buttonMaximize";
            this.buttonMaximize.Size = new System.Drawing.Size(28, 30);
            this.buttonMaximize.TabIndex = 19;
            this.buttonMaximize.UseVisualStyleBackColor = false;
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonMinimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMinimize.BackgroundImage")));
            this.buttonMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMinimize.FlatAppearance.BorderSize = 0;
            this.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMinimize.Location = new System.Drawing.Point(727, 3);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(28, 30);
            this.buttonMinimize.TabIndex = 20;
            this.buttonMinimize.UseVisualStyleBackColor = false;
            this.buttonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
            // 
            // FormAsociereCascoClauze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(830, 561);
            this.Controls.Add(this.panelBar);
            this.Controls.Add(this.labelProcred);
            this.Controls.Add(this.labelproc);
            this.Controls.Add(this.labelFransiza);
            this.Controls.Add(this.comboBoxProcRedFran);
            this.Controls.Add(this.comboBoxProcFran);
            this.Controls.Add(this.comboBoxDenFran);
            this.Controls.Add(this.comboBoxTpcCasco);
            this.Controls.Add(this.buttonAcasa);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelClauze);
            this.Controls.Add(this.labelTip);
            this.Controls.Add(this.labelVal);
            this.Controls.Add(this.numericUpDownValoareClauza);
            this.Controls.Add(this.listBoxClauzeSuplimentare);
            this.Name = "FormAsociereCascoClauze";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAsociereCascoClauze";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValoareClauza)).EndInit();
            this.panelBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBoxClauzeSuplimentare;
        private System.Windows.Forms.NumericUpDown numericUpDownValoareClauza;
        private System.Windows.Forms.Label labelVal;
        private System.Windows.Forms.Label labelClauze;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonAcasa;
        private System.Windows.Forms.Label labelTip;
        private System.Windows.Forms.ComboBox comboBoxTpcCasco;
        private System.Windows.Forms.ComboBox comboBoxDenFran;
        private System.Windows.Forms.ComboBox comboBoxProcFran;
        private System.Windows.Forms.ComboBox comboBoxProcRedFran;
        private System.Windows.Forms.Label labelFransiza;
        private System.Windows.Forms.Label labelproc;
        private System.Windows.Forms.Label labelProcred;
        private System.Windows.Forms.Panel panelBar;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonMaximize;
        private System.Windows.Forms.Button buttonMinimize;
    }
}