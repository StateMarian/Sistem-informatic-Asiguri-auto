namespace Sistem_informatic_Asiguri_auto
{
    partial class FormManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormManager));
            this.buttonLogout = new System.Windows.Forms.Button();
            this.labelNumeAngajat = new System.Windows.Forms.Label();
            this.buttonGestionareAngajati = new System.Windows.Forms.Button();
            this.panelMenuManager = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelGestionare = new System.Windows.Forms.Panel();
            this.panelBar = new System.Windows.Forms.Panel();
            this.buttonMinimize = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonMaximize = new System.Windows.Forms.Button();
            this.panelMeniu = new System.Windows.Forms.Panel();
            this.pictureBoxMenu = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelMenuManager.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelBar.SuspendLayout();
            this.panelMeniu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLogout
            // 
            this.buttonLogout.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonLogout.FlatAppearance.BorderSize = 0;
            this.buttonLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogout.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonLogout.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonLogout.Location = new System.Drawing.Point(1, 655);
            this.buttonLogout.Margin = new System.Windows.Forms.Padding(0);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(227, 67);
            this.buttonLogout.TabIndex = 2;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = false;
            this.buttonLogout.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelNumeAngajat
            // 
            this.labelNumeAngajat.AutoSize = true;
            this.labelNumeAngajat.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelNumeAngajat.Location = new System.Drawing.Point(3, 8);
            this.labelNumeAngajat.Name = "labelNumeAngajat";
            this.labelNumeAngajat.Size = new System.Drawing.Size(82, 31);
            this.labelNumeAngajat.TabIndex = 2;
            this.labelNumeAngajat.Text = "label1";
            // 
            // buttonGestionareAngajati
            // 
            this.buttonGestionareAngajati.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonGestionareAngajati.FlatAppearance.BorderSize = 0;
            this.buttonGestionareAngajati.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGestionareAngajati.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonGestionareAngajati.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonGestionareAngajati.Location = new System.Drawing.Point(-1, 59);
            this.buttonGestionareAngajati.Margin = new System.Windows.Forms.Padding(0);
            this.buttonGestionareAngajati.Name = "buttonGestionareAngajati";
            this.buttonGestionareAngajati.Size = new System.Drawing.Size(227, 60);
            this.buttonGestionareAngajati.TabIndex = 1;
            this.buttonGestionareAngajati.Text = "Gestionare Angajati";
            this.buttonGestionareAngajati.UseVisualStyleBackColor = false;
            this.buttonGestionareAngajati.Click += new System.EventHandler(this.buttonGestionareAngajati_Click);
            this.buttonGestionareAngajati.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonGestionareAngajati_MouseClick);
            // 
            // panelMenuManager
            // 
            this.panelMenuManager.BackColor = System.Drawing.Color.SteelBlue;
            this.panelMenuManager.Controls.Add(this.buttonLogout);
            this.panelMenuManager.Controls.Add(this.buttonGestionareAngajati);
            this.panelMenuManager.Location = new System.Drawing.Point(0, 34);
            this.panelMenuManager.Name = "panelMenuManager";
            this.panelMenuManager.Size = new System.Drawing.Size(228, 722);
            this.panelMenuManager.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.labelNumeAngajat);
            this.panel1.Location = new System.Drawing.Point(566, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 50);
            this.panel1.TabIndex = 5;
            // 
            // panelGestionare
            // 
            this.panelGestionare.Location = new System.Drawing.Point(229, 93);
            this.panelGestionare.Name = "panelGestionare";
            this.panelGestionare.Size = new System.Drawing.Size(1067, 663);
            this.panelGestionare.TabIndex = 6;
            // 
            // panelBar
            // 
            this.panelBar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panelBar.Controls.Add(this.buttonMinimize);
            this.panelBar.Controls.Add(this.buttonExit);
            this.panelBar.Controls.Add(this.buttonMaximize);
            this.panelBar.Location = new System.Drawing.Point(0, -1);
            this.panelBar.Name = "panelBar";
            this.panelBar.Size = new System.Drawing.Size(1306, 35);
            this.panelBar.TabIndex = 7;
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
            this.buttonMinimize.Location = new System.Drawing.Point(1196, 3);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(28, 30);
            this.buttonMinimize.TabIndex = 2;
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
            this.buttonExit.Location = new System.Drawing.Point(1264, 2);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(28, 30);
            this.buttonExit.TabIndex = 0;
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
            this.buttonMaximize.Location = new System.Drawing.Point(1230, 3);
            this.buttonMaximize.Name = "buttonMaximize";
            this.buttonMaximize.Size = new System.Drawing.Size(28, 30);
            this.buttonMaximize.TabIndex = 1;
            this.buttonMaximize.UseVisualStyleBackColor = false;
            // 
            // panelMeniu
            // 
            this.panelMeniu.BackColor = System.Drawing.Color.SteelBlue;
            this.panelMeniu.Controls.Add(this.pictureBoxMenu);
            this.panelMeniu.Controls.Add(this.label1);
            this.panelMeniu.Location = new System.Drawing.Point(1, 37);
            this.panelMeniu.Name = "panelMeniu";
            this.panelMeniu.Size = new System.Drawing.Size(224, 53);
            this.panelMeniu.TabIndex = 8;
            // 
            // pictureBoxMenu
            // 
            this.pictureBoxMenu.BackColor = System.Drawing.Color.SteelBlue;
            this.pictureBoxMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxMenu.BackgroundImage")));
            this.pictureBoxMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxMenu.Location = new System.Drawing.Point(21, 10);
            this.pictureBoxMenu.Name = "pictureBoxMenu";
            this.pictureBoxMenu.Size = new System.Drawing.Size(38, 32);
            this.pictureBoxMenu.TabIndex = 0;
            this.pictureBoxMenu.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(65, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Meniu";
            // 
            // FormManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1296, 754);
            this.Controls.Add(this.panelMeniu);
            this.Controls.Add(this.panelBar);
            this.Controls.Add(this.panelGestionare);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelMenuManager);
            this.Name = "FormManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormManager";
            this.panelMenuManager.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelBar.ResumeLayout(false);
            this.panelMeniu.ResumeLayout(false);
            this.panelMeniu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Label labelNumeAngajat;
        private System.Windows.Forms.Button buttonGestionareAngajati;
        private System.Windows.Forms.Panel panelMenuManager;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelGestionare;
        private System.Windows.Forms.Panel panelBar;
        private System.Windows.Forms.Panel panelMeniu;
        private System.Windows.Forms.PictureBox pictureBoxMenu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonMinimize;
        private System.Windows.Forms.Button buttonMaximize;
        private System.Windows.Forms.Button buttonExit;
    }
}