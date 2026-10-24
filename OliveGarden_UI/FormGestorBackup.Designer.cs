namespace OliveGarden_UI
{
    partial class FormGestorBackup
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
            this.dgvBackups = new System.Windows.Forms.DataGridView();
            this.lblUltimoBackup = new System.Windows.Forms.Label();
            this.btnCrearBackup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackups)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBackups
            // 
            this.dgvBackups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBackups.Location = new System.Drawing.Point(12, 12);
            this.dgvBackups.Name = "dgvBackups";
            this.dgvBackups.Size = new System.Drawing.Size(518, 247);
            this.dgvBackups.TabIndex = 0;
            // 
            // lblUltimoBackup
            // 
            this.lblUltimoBackup.AutoSize = true;
            this.lblUltimoBackup.Location = new System.Drawing.Point(25, 22);
            this.lblUltimoBackup.Name = "lblUltimoBackup";
            this.lblUltimoBackup.Size = new System.Drawing.Size(76, 13);
            this.lblUltimoBackup.TabIndex = 1;
            this.lblUltimoBackup.Text = "Ultimo Backup";
            // 
            // btnCrearBackup
            // 
            this.btnCrearBackup.Location = new System.Drawing.Point(26, 265);
            this.btnCrearBackup.Name = "btnCrearBackup";
            this.btnCrearBackup.Size = new System.Drawing.Size(89, 23);
            this.btnCrearBackup.TabIndex = 2;
            this.btnCrearBackup.Text = "Crear Backup";
            this.btnCrearBackup.UseVisualStyleBackColor = true;
            this.btnCrearBackup.Click += new System.EventHandler(this.btnCrearBackup_Click);
            // 
            // FormGestorBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 313);
            this.Controls.Add(this.btnCrearBackup);
            this.Controls.Add(this.lblUltimoBackup);
            this.Controls.Add(this.dgvBackups);
            this.Name = "FormGestorBackup";
            this.Text = "FormGestorBackup";
            this.Load += new System.EventHandler(this.FormGestorBackup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBackups;
        private System.Windows.Forms.Label lblUltimoBackup;
        private System.Windows.Forms.Button btnCrearBackup;
    }
}