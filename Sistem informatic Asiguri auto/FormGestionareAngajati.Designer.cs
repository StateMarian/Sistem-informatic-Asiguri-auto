
namespace Sistem_informatic_Asiguri_auto
{
    partial class FormGestionareAngajati
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridAngajat = new System.Windows.Forms.DataGridView();
            this.buttonAdaugaAngajat = new System.Windows.Forms.Button();
            this.buttonActualizeazaAngajat = new System.Windows.Forms.Button();
            this.buttonConcediere = new System.Windows.Forms.Button();
            this.textBoxSearchName = new System.Windows.Forms.TextBox();
            this.labelCauta = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAngajat)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridAngajat
            // 
            this.dataGridAngajat.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridAngajat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridAngajat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAngajat.Location = new System.Drawing.Point(94, 81);
            this.dataGridAngajat.Name = "dataGridAngajat";
            this.dataGridAngajat.RowTemplate.Height = 25;
            this.dataGridAngajat.Size = new System.Drawing.Size(888, 350);
            this.dataGridAngajat.TabIndex = 0;
            // 
            // buttonAdaugaAngajat
            // 
            this.buttonAdaugaAngajat.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAdaugaAngajat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdaugaAngajat.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonAdaugaAngajat.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAdaugaAngajat.Location = new System.Drawing.Point(94, 465);
            this.buttonAdaugaAngajat.Name = "buttonAdaugaAngajat";
            this.buttonAdaugaAngajat.Size = new System.Drawing.Size(269, 46);
            this.buttonAdaugaAngajat.TabIndex = 1;
            this.buttonAdaugaAngajat.Text = "Adauga angajat";
            this.buttonAdaugaAngajat.UseVisualStyleBackColor = false;
            this.buttonAdaugaAngajat.Click += new System.EventHandler(this.buttonAdaugaAngajat_Click);
            // 
            // buttonActualizeazaAngajat
            // 
            this.buttonActualizeazaAngajat.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonActualizeazaAngajat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonActualizeazaAngajat.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonActualizeazaAngajat.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonActualizeazaAngajat.Location = new System.Drawing.Point(402, 465);
            this.buttonActualizeazaAngajat.Name = "buttonActualizeazaAngajat";
            this.buttonActualizeazaAngajat.Size = new System.Drawing.Size(269, 46);
            this.buttonActualizeazaAngajat.TabIndex = 2;
            this.buttonActualizeazaAngajat.Text = "Modifica date angajat";
            this.buttonActualizeazaAngajat.UseVisualStyleBackColor = false;
            this.buttonActualizeazaAngajat.Click += new System.EventHandler(this.buttonActualizeazaAngajat_Click);
            // 
            // buttonConcediere
            // 
            this.buttonConcediere.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonConcediere.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConcediere.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonConcediere.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonConcediere.Location = new System.Drawing.Point(713, 465);
            this.buttonConcediere.Name = "buttonConcediere";
            this.buttonConcediere.Size = new System.Drawing.Size(269, 46);
            this.buttonConcediere.TabIndex = 3;
            this.buttonConcediere.Text = "Concediere angajat";
            this.buttonConcediere.UseVisualStyleBackColor = false;
            this.buttonConcediere.Click += new System.EventHandler(this.buttonConcediere_Click);
            // 
            // textBoxSearchName
            // 
            this.textBoxSearchName.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxSearchName.Location = new System.Drawing.Point(837, 48);
            this.textBoxSearchName.Name = "textBoxSearchName";
            this.textBoxSearchName.Size = new System.Drawing.Size(145, 30);
            this.textBoxSearchName.TabIndex = 4;
            this.textBoxSearchName.TextChanged += new System.EventHandler(this.textBoxSearchName_TextChanged);
            // 
            // labelCauta
            // 
            this.labelCauta.AutoSize = true;
            this.labelCauta.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCauta.Location = new System.Drawing.Point(762, 51);
            this.labelCauta.Name = "labelCauta";
            this.labelCauta.Size = new System.Drawing.Size(69, 27);
            this.labelCauta.TabIndex = 5;
            this.labelCauta.Text = "Cauta";
            // 
            // FormGestionareAngajati
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1051, 624);
            this.Controls.Add(this.labelCauta);
            this.Controls.Add(this.textBoxSearchName);
            this.Controls.Add(this.buttonConcediere);
            this.Controls.Add(this.buttonActualizeazaAngajat);
            this.Controls.Add(this.buttonAdaugaAngajat);
            this.Controls.Add(this.dataGridAngajat);
            this.Name = "FormGestionareAngajati";
            this.Text = "FormGestionareAngajati";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAngajat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridAngajat;
        private System.Windows.Forms.Button buttonAdaugaAngajat;
        private System.Windows.Forms.Button buttonActualizeazaAngajat;
        private System.Windows.Forms.Button buttonConcediere;
        private System.Windows.Forms.TextBox textBoxSearchName;
        private System.Windows.Forms.Label labelCauta;
    }
}