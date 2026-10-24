namespace OliveGarden_UI
{
    partial class FormGestorRestore
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
            this.trvBackups = new System.Windows.Forms.TreeView();
            this.lblSeleccionado = new System.Windows.Forms.Label();
            this.btnRestore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trvBackups
            // 
            this.trvBackups.Location = new System.Drawing.Point(12, 24);
            this.trvBackups.Name = "trvBackups";
            this.trvBackups.Size = new System.Drawing.Size(691, 414);
            this.trvBackups.TabIndex = 0;
            this.trvBackups.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvBackups_AfterSelect);
            // 
            // lblSeleccionado
            // 
            this.lblSeleccionado.AutoSize = true;
            this.lblSeleccionado.Location = new System.Drawing.Point(25, 44);
            this.lblSeleccionado.Name = "lblSeleccionado";
            this.lblSeleccionado.Size = new System.Drawing.Size(72, 13);
            this.lblSeleccionado.TabIndex = 1;
            this.lblSeleccionado.Text = "Seleccionado";
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(713, 208);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.TabIndex = 2;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // FormGestorRestore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.lblSeleccionado);
            this.Controls.Add(this.trvBackups);
            this.Name = "FormGestorRestore";
            this.Text = "FormGestorRestore";
            this.Load += new System.EventHandler(this.FormGestorRestore_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvBackups;
        private System.Windows.Forms.Label lblSeleccionado;
        private System.Windows.Forms.Button btnRestore;
    }
}