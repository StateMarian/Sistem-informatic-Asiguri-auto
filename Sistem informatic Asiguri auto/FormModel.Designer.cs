
namespace Sistem_informatic_Asiguri_auto
{
    partial class FormModel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormModel));
            this.listBoxMarca = new System.Windows.Forms.ListBox();
            this.labelMarca = new System.Windows.Forms.Label();
            this.labelvar = new System.Windows.Forms.Label();
            this.labeltip = new System.Windows.Forms.Label();
            this.buttonAdauga = new System.Windows.Forms.Button();
            this.buttonSterge = new System.Windows.Forms.Button();
            this.buttonAcasa = new System.Windows.Forms.Button();
            this.labelModele = new System.Windows.Forms.Label();
            this.comboBoxTipauto = new System.Windows.Forms.ComboBox();
            this.comboBoxModel = new System.Windows.Forms.ComboBox();
            this.comboBoxVarianta = new System.Windows.Forms.ComboBox();
            this.panelBar = new System.Windows.Forms.Panel();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonMaximize = new System.Windows.Forms.Button();
            this.buttonMinimize = new System.Windows.Forms.Button();
            this.panelBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxMarca
            // 
            this.listBoxMarca.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listBoxMarca.FormattingEnabled = true;
            this.listBoxMarca.HorizontalScrollbar = true;
            this.listBoxMarca.ItemHeight = 23;
            this.listBoxMarca.Location = new System.Drawing.Point(43, 100);
            this.listBoxMarca.Name = "listBoxMarca";
            this.listBoxMarca.Size = new System.Drawing.Size(305, 395);
            this.listBoxMarca.TabIndex = 0;
            this.listBoxMarca.SelectedIndexChanged += new System.EventHandler(this.listBoxMarca_SelectedIndexChanged);
            // 
            // labelMarca
            // 
            this.labelMarca.AutoSize = true;
            this.labelMarca.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelMarca.Location = new System.Drawing.Point(43, 71);
            this.labelMarca.Name = "labelMarca";
            this.labelMarca.Size = new System.Drawing.Size(69, 27);
            this.labelMarca.TabIndex = 1;
            this.labelMarca.Text = "Marci";
            // 
            // labelvar
            // 
            this.labelvar.AutoSize = true;
            this.labelvar.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelvar.Location = new System.Drawing.Point(384, 143);
            this.labelvar.Name = "labelvar";
            this.labelvar.Size = new System.Drawing.Size(91, 27);
            this.labelvar.TabIndex = 6;
            this.labelvar.Text = "Varianta";
            // 
            // labeltip
            // 
            this.labeltip.AutoSize = true;
            this.labeltip.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labeltip.Location = new System.Drawing.Point(384, 220);
            this.labeltip.Name = "labeltip";
            this.labeltip.Size = new System.Drawing.Size(91, 27);
            this.labeltip.TabIndex = 7;
            this.labeltip.Text = "Tip auto";
            // 
            // buttonAdauga
            // 
            this.buttonAdauga.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAdauga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdauga.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonAdauga.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAdauga.Location = new System.Drawing.Point(384, 308);
            this.buttonAdauga.Name = "buttonAdauga";
            this.buttonAdauga.Size = new System.Drawing.Size(202, 48);
            this.buttonAdauga.TabIndex = 8;
            this.buttonAdauga.Text = "Adauga model";
            this.buttonAdauga.UseVisualStyleBackColor = false;
            this.buttonAdauga.Click += new System.EventHandler(this.buttonAdauga_Click);
            // 
            // buttonSterge
            // 
            this.buttonSterge.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonSterge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSterge.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSterge.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSterge.Location = new System.Drawing.Point(384, 381);
            this.buttonSterge.Name = "buttonSterge";
            this.buttonSterge.Size = new System.Drawing.Size(202, 46);
            this.buttonSterge.TabIndex = 9;
            this.buttonSterge.Text = "Sterge model";
            this.buttonSterge.UseVisualStyleBackColor = false;
            this.buttonSterge.Click += new System.EventHandler(this.buttonSterge_Click);
            // 
            // buttonAcasa
            // 
            this.buttonAcasa.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAcasa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAcasa.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonAcasa.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAcasa.Location = new System.Drawing.Point(384, 449);
            this.buttonAcasa.Name = "buttonAcasa";
            this.buttonAcasa.Size = new System.Drawing.Size(202, 46);
            this.buttonAcasa.TabIndex = 11;
            this.buttonAcasa.Text = "Acasa";
            this.buttonAcasa.UseVisualStyleBackColor = false;
            this.buttonAcasa.Click += new System.EventHandler(this.buttonAcasa_Click);
            // 
            // labelModele
            // 
            this.labelModele.AutoSize = true;
            this.labelModele.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelModele.Location = new System.Drawing.Point(384, 71);
            this.labelModele.Name = "labelModele";
            this.labelModele.Size = new System.Drawing.Size(74, 27);
            this.labelModele.TabIndex = 12;
            this.labelModele.Text = "Model";
            // 
            // comboBoxTipauto
            // 
            this.comboBoxTipauto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipauto.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxTipauto.FormattingEnabled = true;
            this.comboBoxTipauto.Location = new System.Drawing.Point(384, 250);
            this.comboBoxTipauto.Name = "comboBoxTipauto";
            this.comboBoxTipauto.Size = new System.Drawing.Size(200, 31);
            this.comboBoxTipauto.TabIndex = 13;
            // 
            // comboBoxModel
            // 
            this.comboBoxModel.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxModel.FormattingEnabled = true;
            this.comboBoxModel.Location = new System.Drawing.Point(384, 100);
            this.comboBoxModel.Name = "comboBoxModel";
            this.comboBoxModel.Size = new System.Drawing.Size(202, 31);
            this.comboBoxModel.TabIndex = 14;
            this.comboBoxModel.SelectedIndexChanged += new System.EventHandler(this.comboBoxModel_SelectedIndexChanged);
            // 
            // comboBoxVarianta
            // 
            this.comboBoxVarianta.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxVarianta.FormattingEnabled = true;
            this.comboBoxVarianta.Location = new System.Drawing.Point(384, 173);
            this.comboBoxVarianta.Name = "comboBoxVarianta";
            this.comboBoxVarianta.Size = new System.Drawing.Size(202, 31);
            this.comboBoxVarianta.TabIndex = 15;
            // 
            // panelBar
            // 
            this.panelBar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panelBar.Controls.Add(this.buttonMinimize);
            this.panelBar.Controls.Add(this.buttonExit);
            this.panelBar.Controls.Add(this.buttonMaximize);
            this.panelBar.Location = new System.Drawing.Point(0, -1);
            this.panelBar.Name = "panelBar";
            this.panelBar.Size = new System.Drawing.Size(634, 35);
            this.panelBar.TabIndex = 16;
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
            this.buttonExit.Location = new System.Drawing.Point(591, 3);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(28, 30);
            this.buttonExit.TabIndex = 17;
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
            this.buttonMaximize.Location = new System.Drawing.Point(558, 3);
            this.buttonMaximize.Name = "buttonMaximize";
            this.buttonMaximize.Size = new System.Drawing.Size(28, 30);
            this.buttonMaximize.TabIndex = 18;
            this.buttonMaximize.UseVisualStyleBackColor = false;
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonMinimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMinimize.BackgroundImage")));
            this.buttonMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMinimize.FlatAppearance.BorderSize = 0;
            this.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMinimize.Location = new System.Drawing.Point(524, 3);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(28, 30);
            this.buttonMinimize.TabIndex = 19;
            this.buttonMinimize.UseVisualStyleBackColor = false;
            this.buttonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
            // 
            // FormModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(627, 522);
            this.Controls.Add(this.panelBar);
            this.Controls.Add(this.comboBoxVarianta);
            this.Controls.Add(this.comboBoxModel);
            this.Controls.Add(this.comboBoxTipauto);
            this.Controls.Add(this.labelModele);
            this.Controls.Add(this.buttonAcasa);
            this.Controls.Add(this.buttonSterge);
            this.Controls.Add(this.buttonAdauga);
            this.Controls.Add(this.labeltip);
            this.Controls.Add(this.labelvar);
            this.Controls.Add(this.labelMarca);
            this.Controls.Add(this.listBoxMarca);
            this.Name = "FormModel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormModel";
            this.panelBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxMarca;
        private System.Windows.Forms.Label labelMarca;
        private System.Windows.Forms.Label labelvar;
        private System.Windows.Forms.Label labeltip;
        private System.Windows.Forms.Button buttonAdauga;
        private System.Windows.Forms.Button buttonSterge;
        private System.Windows.Forms.Button buttonAcasa;
        private System.Windows.Forms.Label labelModele;
        private System.Windows.Forms.ComboBox comboBoxTipauto;
        private System.Windows.Forms.ComboBox comboBoxModel;
        private System.Windows.Forms.ComboBox comboBoxVarianta;
        private System.Windows.Forms.Panel panelBar;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonMaximize;
        private System.Windows.Forms.Button buttonMinimize;
    }
}