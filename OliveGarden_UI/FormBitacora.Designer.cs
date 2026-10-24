namespace OliveGarden_UI
{
    partial class FormBitacora
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
            this.dgvBitacora = new System.Windows.Forms.DataGridView();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.rbBackups = new System.Windows.Forms.RadioButton();
            this.rbRestores = new System.Windows.Forms.RadioButton();
            this.btnFiltrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBitacora
            // 
            this.dgvBitacora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBitacora.Location = new System.Drawing.Point(12, 23);
            this.dgvBitacora.Name = "dgvBitacora";
            this.dgvBitacora.Size = new System.Drawing.Size(502, 267);
            this.dgvBitacora.TabIndex = 0;
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Location = new System.Drawing.Point(12, 316);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(55, 17);
            this.rbTodos.TabIndex = 1;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            // 
            // rbBackups
            // 
            this.rbBackups.AutoSize = true;
            this.rbBackups.Location = new System.Drawing.Point(97, 316);
            this.rbBackups.Name = "rbBackups";
            this.rbBackups.Size = new System.Drawing.Size(67, 17);
            this.rbBackups.TabIndex = 2;
            this.rbBackups.TabStop = true;
            this.rbBackups.Text = "Backups";
            this.rbBackups.UseVisualStyleBackColor = true;
            // 
            // rbRestores
            // 
            this.rbRestores.AutoSize = true;
            this.rbRestores.Location = new System.Drawing.Point(202, 320);
            this.rbRestores.Name = "rbRestores";
            this.rbRestores.Size = new System.Drawing.Size(67, 17);
            this.rbRestores.TabIndex = 3;
            this.rbRestores.TabStop = true;
            this.rbRestores.Text = "Restores";
            this.rbRestores.UseVisualStyleBackColor = true;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(404, 310);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrar.TabIndex = 4;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // FormBitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.rbRestores);
            this.Controls.Add(this.rbBackups);
            this.Controls.Add(this.rbTodos);
            this.Controls.Add(this.dgvBitacora);
            this.Name = "FormBitacora";
            this.Text = "FormBitacora";
            this.Load += new System.EventHandler(this.FormBitacora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBitacora;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.RadioButton rbBackups;
        private System.Windows.Forms.RadioButton rbRestores;
        private System.Windows.Forms.Button btnFiltrar;
    }
}