
namespace Sistem_informatic_Asiguri_auto
{
    partial class FormAdaugaSubcategorie
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdaugaSubcategorie));
            this.buttonInapoi = new System.Windows.Forms.Button();
            this.textBoxDenumire = new System.Windows.Forms.TextBox();
            this.labelDenumire = new System.Windows.Forms.Label();
            this.labelMasaMin = new System.Windows.Forms.Label();
            this.labelMasaMax = new System.Windows.Forms.Label();
            this.buttonAdauga = new System.Windows.Forms.Button();
            this.listBoxSubcategorii = new System.Windows.Forms.ListBox();
            this.labelCategorii = new System.Windows.Forms.Label();
            this.buttonSterge = new System.Windows.Forms.Button();
            this.comboBoxCategorii = new System.Windows.Forms.ComboBox();
            this.labelSubcategorii = new System.Windows.Forms.Label();
            this.comboBoxLocuri = new System.Windows.Forms.ComboBox();
            this.comboBoxMasaMax = new System.Windows.Forms.ComboBox();
            this.labelLocuri = new System.Windows.Forms.Label();
            this.comboBoxMasaMin = new System.Windows.Forms.ComboBox();
            this.checkBoxDeblocare = new System.Windows.Forms.CheckBox();
            this.panelBar = new System.Windows.Forms.Panel();
            this.buttonMinimize = new System.Windows.Forms.Button();
            this.buttonMaximize = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.panelBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonInapoi
            // 
            this.buttonInapoi.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonInapoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonInapoi.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonInapoi.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonInapoi.Location = new System.Drawing.Point(849, 464);
            this.buttonInapoi.Name = "buttonInapoi";
            this.buttonInapoi.Size = new System.Drawing.Size(170, 46);
            this.buttonInapoi.TabIndex = 0;
            this.buttonInapoi.Text = "Acasa";
            this.buttonInapoi.UseVisualStyleBackColor = false;
            this.buttonInapoi.Click += new System.EventHandler(this.buttonInapoi_Click);
            // 
            // textBoxDenumire
            // 
            this.textBoxDenumire.Enabled = false;
            this.textBoxDenumire.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxDenumire.Location = new System.Drawing.Point(458, 228);
            this.textBoxDenumire.Name = "textBoxDenumire";
            this.textBoxDenumire.Size = new System.Drawing.Size(561, 32);
            this.textBoxDenumire.TabIndex = 1;
            // 
            // labelDenumire
            // 
            this.labelDenumire.AutoSize = true;
            this.labelDenumire.Enabled = false;
            this.labelDenumire.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelDenumire.Location = new System.Drawing.Point(458, 198);
            this.labelDenumire.Name = "labelDenumire";
            this.labelDenumire.Size = new System.Drawing.Size(234, 27);
            this.labelDenumire.TabIndex = 5;
            this.labelDenumire.Text = "Denumire subcategorie";
            // 
            // labelMasaMin
            // 
            this.labelMasaMin.AutoSize = true;
            this.labelMasaMin.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelMasaMin.Location = new System.Drawing.Point(458, 368);
            this.labelMasaMin.Name = "labelMasaMin";
            this.labelMasaMin.Size = new System.Drawing.Size(141, 27);
            this.labelMasaMin.TabIndex = 7;
            this.labelMasaMin.Text = "Masa minima";
            // 
            // labelMasaMax
            // 
            this.labelMasaMax.AutoSize = true;
            this.labelMasaMax.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelMasaMax.Location = new System.Drawing.Point(746, 368);
            this.labelMasaMax.Name = "labelMasaMax";
            this.labelMasaMax.Size = new System.Drawing.Size(146, 27);
            this.labelMasaMax.TabIndex = 8;
            this.labelMasaMax.Text = "Masa maxima";
            // 
            // buttonAdauga
            // 
            this.buttonAdauga.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAdauga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdauga.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonAdauga.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAdauga.Location = new System.Drawing.Point(465, 464);
            this.buttonAdauga.Name = "buttonAdauga";
            this.buttonAdauga.Size = new System.Drawing.Size(170, 46);
            this.buttonAdauga.TabIndex = 9;
            this.buttonAdauga.Text = "Adauga";
            this.buttonAdauga.UseVisualStyleBackColor = false;
            this.buttonAdauga.Click += new System.EventHandler(this.buttonAdauga_Click);
            // 
            // listBoxSubcategorii
            // 
            this.listBoxSubcategorii.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listBoxSubcategorii.FormattingEnabled = true;
            this.listBoxSubcategorii.HorizontalScrollbar = true;
            this.listBoxSubcategorii.ItemHeight = 27;
            this.listBoxSubcategorii.Location = new System.Drawing.Point(12, 155);
            this.listBoxSubcategorii.Name = "listBoxSubcategorii";
            this.listBoxSubcategorii.ScrollAlwaysVisible = true;
            this.listBoxSubcategorii.Size = new System.Drawing.Size(417, 355);
            this.listBoxSubcategorii.TabIndex = 10;
            this.listBoxSubcategorii.SelectedIndexChanged += new System.EventHandler(this.listBoxSubcategorii_SelectedIndexChanged);
            // 
            // labelCategorii
            // 
            this.labelCategorii.AutoSize = true;
            this.labelCategorii.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCategorii.Location = new System.Drawing.Point(12, 50);
            this.labelCategorii.Name = "labelCategorii";
            this.labelCategorii.Size = new System.Drawing.Size(100, 27);
            this.labelCategorii.TabIndex = 11;
            this.labelCategorii.Text = "Categorii";
            // 
            // buttonSterge
            // 
            this.buttonSterge.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonSterge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSterge.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSterge.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSterge.Location = new System.Drawing.Point(656, 464);
            this.buttonSterge.Name = "buttonSterge";
            this.buttonSterge.Size = new System.Drawing.Size(170, 46);
            this.buttonSterge.TabIndex = 12;
            this.buttonSterge.Text = "Sterge ";
            this.buttonSterge.UseVisualStyleBackColor = false;
            this.buttonSterge.Click += new System.EventHandler(this.buttonSterge_Click);
            // 
            // comboBoxCategorii
            // 
            this.comboBoxCategorii.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxCategorii.FormattingEnabled = true;
            this.comboBoxCategorii.Location = new System.Drawing.Point(12, 80);
            this.comboBoxCategorii.Name = "comboBoxCategorii";
            this.comboBoxCategorii.Size = new System.Drawing.Size(1007, 31);
            this.comboBoxCategorii.TabIndex = 13;
            this.comboBoxCategorii.SelectedIndexChanged += new System.EventHandler(this.comboBoxCategorii_SelectedIndexChanged);
            // 
            // labelSubcategorii
            // 
            this.labelSubcategorii.AutoSize = true;
            this.labelSubcategorii.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelSubcategorii.Location = new System.Drawing.Point(12, 125);
            this.labelSubcategorii.Name = "labelSubcategorii";
            this.labelSubcategorii.Size = new System.Drawing.Size(223, 27);
            this.labelSubcategorii.TabIndex = 14;
            this.labelSubcategorii.Text = "Subcategorii existente";
            // 
            // comboBoxLocuri
            // 
            this.comboBoxLocuri.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxLocuri.FormattingEnabled = true;
            this.comboBoxLocuri.Location = new System.Drawing.Point(458, 311);
            this.comboBoxLocuri.Name = "comboBoxLocuri";
            this.comboBoxLocuri.Size = new System.Drawing.Size(191, 31);
            this.comboBoxLocuri.TabIndex = 15;
            this.comboBoxLocuri.SelectedIndexChanged += new System.EventHandler(this.comboBoxLocuri_SelectedIndexChanged);
            // 
            // comboBoxMasaMax
            // 
            this.comboBoxMasaMax.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxMasaMax.FormattingEnabled = true;
            this.comboBoxMasaMax.Location = new System.Drawing.Point(746, 398);
            this.comboBoxMasaMax.Name = "comboBoxMasaMax";
            this.comboBoxMasaMax.Size = new System.Drawing.Size(273, 31);
            this.comboBoxMasaMax.TabIndex = 16;
            // 
            // labelLocuri
            // 
            this.labelLocuri.AutoSize = true;
            this.labelLocuri.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelLocuri.Location = new System.Drawing.Point(458, 281);
            this.labelLocuri.Name = "labelLocuri";
            this.labelLocuri.Size = new System.Drawing.Size(139, 27);
            this.labelLocuri.TabIndex = 17;
            this.labelLocuri.Text = "Numar locuri";
            // 
            // comboBoxMasaMin
            // 
            this.comboBoxMasaMin.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxMasaMin.FormattingEnabled = true;
            this.comboBoxMasaMin.Location = new System.Drawing.Point(458, 398);
            this.comboBoxMasaMin.Name = "comboBoxMasaMin";
            this.comboBoxMasaMin.Size = new System.Drawing.Size(274, 31);
            this.comboBoxMasaMin.TabIndex = 19;
            // 
            // checkBoxDeblocare
            // 
            this.checkBoxDeblocare.AutoSize = true;
            this.checkBoxDeblocare.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxDeblocare.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.checkBoxDeblocare.Location = new System.Drawing.Point(458, 155);
            this.checkBoxDeblocare.Name = "checkBoxDeblocare";
            this.checkBoxDeblocare.Size = new System.Drawing.Size(248, 27);
            this.checkBoxDeblocare.TabIndex = 20;
            this.checkBoxDeblocare.Text = "Adauga subcategorie noua";
            this.checkBoxDeblocare.UseVisualStyleBackColor = true;
            this.checkBoxDeblocare.CheckedChanged += new System.EventHandler(this.checkBoxDeblocare_CheckedChanged);
            // 
            // panelBar
            // 
            this.panelBar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panelBar.Controls.Add(this.buttonMinimize);
            this.panelBar.Controls.Add(this.buttonMaximize);
            this.panelBar.Controls.Add(this.buttonExit);
            this.panelBar.Location = new System.Drawing.Point(-4, -1);
            this.panelBar.Name = "panelBar";
            this.panelBar.Size = new System.Drawing.Size(1046, 35);
            this.panelBar.TabIndex = 21;
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
            this.buttonMinimize.Location = new System.Drawing.Point(939, 3);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(28, 30);
            this.buttonMinimize.TabIndex = 22;
            this.buttonMinimize.UseVisualStyleBackColor = false;
            this.buttonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
            // 
            // buttonMaximize
            // 
            this.buttonMaximize.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonMaximize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMaximize.BackgroundImage")));
            this.buttonMaximize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMaximize.FlatAppearance.BorderSize = 0;
            this.buttonMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMaximize.Location = new System.Drawing.Point(973, 3);
            this.buttonMaximize.Name = "buttonMaximize";
            this.buttonMaximize.Size = new System.Drawing.Size(28, 30);
            this.buttonMaximize.TabIndex = 23;
            this.buttonMaximize.UseVisualStyleBackColor = false;
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonExit.BackgroundImage")));
            this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Location = new System.Drawing.Point(1007, 3);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(28, 30);
            this.buttonExit.TabIndex = 24;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // FormAdaugaSubcategorie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1036, 541);
            this.Controls.Add(this.panelBar);
            this.Controls.Add(this.checkBoxDeblocare);
            this.Controls.Add(this.comboBoxMasaMin);
            this.Controls.Add(this.labelLocuri);
            this.Controls.Add(this.comboBoxMasaMax);
            this.Controls.Add(this.comboBoxLocuri);
            this.Controls.Add(this.labelSubcategorii);
            this.Controls.Add(this.comboBoxCategorii);
            this.Controls.Add(this.buttonSterge);
            this.Controls.Add(this.labelCategorii);
            this.Controls.Add(this.listBoxSubcategorii);
            this.Controls.Add(this.buttonAdauga);
            this.Controls.Add(this.labelMasaMax);
            this.Controls.Add(this.labelMasaMin);
            this.Controls.Add(this.labelDenumire);
            this.Controls.Add(this.textBoxDenumire);
            this.Controls.Add(this.buttonInapoi);
            this.Name = "FormAdaugaSubcategorie";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAdaugaSubcategorie";
            this.panelBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonInapoi;
        private System.Windows.Forms.TextBox textBoxDenumire;
        private System.Windows.Forms.Label labelDenumire;
        private System.Windows.Forms.Label labelMasaMin;
        private System.Windows.Forms.Label labelMasaMax;
        private System.Windows.Forms.Button buttonAdauga;
        private System.Windows.Forms.ListBox listBoxSubcategorii;
        private System.Windows.Forms.Label labelCategorii;
        private System.Windows.Forms.Button buttonSterge;
        private System.Windows.Forms.ComboBox comboBoxCategorii;
        private System.Windows.Forms.Label labelSubcategorii;
        private System.Windows.Forms.ComboBox comboBoxLocuri;
        private System.Windows.Forms.ComboBox comboBoxMasaMax;
        private System.Windows.Forms.Label labelLocuri;
        private System.Windows.Forms.ComboBox comboBoxMasaMin;
        private System.Windows.Forms.CheckBox checkBoxDeblocare;
        private System.Windows.Forms.Panel panelBar;
        private System.Windows.Forms.Button buttonMinimize;
        private System.Windows.Forms.Button buttonMaximize;
        private System.Windows.Forms.Button buttonExit;
    }
}