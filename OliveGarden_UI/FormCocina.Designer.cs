namespace OliveGarden_UI
{
    partial class FormCocina
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
            this.gbPendientes = new System.Windows.Forms.GroupBox();
            this.dgvPendientes = new System.Windows.Forms.DataGridView();
            this.gbEnPreparacion = new System.Windows.Forms.GroupBox();
            this.dgvEnPreparacion = new System.Windows.Forms.DataGridView();
            this.btnTomarPedido = new System.Windows.Forms.Button();
            this.btnMarcarTerminado = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.gbPendientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendientes)).BeginInit();
            this.gbEnPreparacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnPreparacion)).BeginInit();
            this.SuspendLayout();
            // 
            // gbPendientes
            // 
            this.gbPendientes.Controls.Add(this.dgvPendientes);
            this.gbPendientes.Location = new System.Drawing.Point(12, 12);
            this.gbPendientes.Name = "gbPendientes";
            this.gbPendientes.Size = new System.Drawing.Size(394, 311);
            this.gbPendientes.TabIndex = 0;
            this.gbPendientes.TabStop = false;
            this.gbPendientes.Text = "Pedidos pendientes";
            // 
            // dgvPendientes
            // 
            this.dgvPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPendientes.Location = new System.Drawing.Point(18, 33);
            this.dgvPendientes.Name = "dgvPendientes";
            this.dgvPendientes.Size = new System.Drawing.Size(356, 260);
            this.dgvPendientes.TabIndex = 1;
            // 
            // gbEnPreparacion
            // 
            this.gbEnPreparacion.Controls.Add(this.dgvEnPreparacion);
            this.gbEnPreparacion.Location = new System.Drawing.Point(417, 24);
            this.gbEnPreparacion.Name = "gbEnPreparacion";
            this.gbEnPreparacion.Size = new System.Drawing.Size(371, 299);
            this.gbEnPreparacion.TabIndex = 1;
            this.gbEnPreparacion.TabStop = false;
            this.gbEnPreparacion.Text = "En preparación";
            // 
            // dgvEnPreparacion
            // 
            this.dgvEnPreparacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEnPreparacion.Location = new System.Drawing.Point(6, 21);
            this.dgvEnPreparacion.Name = "dgvEnPreparacion";
            this.dgvEnPreparacion.Size = new System.Drawing.Size(359, 260);
            this.dgvEnPreparacion.TabIndex = 0;
            // 
            // btnTomarPedido
            // 
            this.btnTomarPedido.Location = new System.Drawing.Point(30, 329);
            this.btnTomarPedido.Name = "btnTomarPedido";
            this.btnTomarPedido.Size = new System.Drawing.Size(75, 37);
            this.btnTomarPedido.TabIndex = 2;
            this.btnTomarPedido.Text = "Tomar Pedido";
            this.btnTomarPedido.UseVisualStyleBackColor = true;
            // 
            // btnMarcarTerminado
            // 
            this.btnMarcarTerminado.Location = new System.Drawing.Point(369, 336);
            this.btnMarcarTerminado.Name = "btnMarcarTerminado";
            this.btnMarcarTerminado.Size = new System.Drawing.Size(75, 49);
            this.btnMarcarTerminado.TabIndex = 3;
            this.btnMarcarTerminado.Text = "Marcar como terminado";
            this.btnMarcarTerminado.UseVisualStyleBackColor = true;
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Location = new System.Drawing.Point(696, 336);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(75, 23);
            this.btnRefrescar.TabIndex = 4;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = true;
            // 
            // lblDetalle
            // 
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Location = new System.Drawing.Point(38, 381);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(35, 13);
            this.lblDetalle.TabIndex = 5;
            this.lblDetalle.Text = "label1";
            // 
            // FormCocina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblDetalle);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.btnMarcarTerminado);
            this.Controls.Add(this.btnTomarPedido);
            this.Controls.Add(this.gbEnPreparacion);
            this.Controls.Add(this.gbPendientes);
            this.Name = "FormCocina";
            this.Text = "FormCocina";
            this.gbPendientes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendientes)).EndInit();
            this.gbEnPreparacion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnPreparacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            this.btnTomarPedido.Click += new System.EventHandler(this.btnTomarPedido_Click);
            this.btnMarcarTerminado.Click += new System.EventHandler(this.btnMarcarTerminado_Click);
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            this.Load += new System.EventHandler(this.FormCocina_Load);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbPendientes;
        private System.Windows.Forms.DataGridView dgvPendientes;
        private System.Windows.Forms.GroupBox gbEnPreparacion;
        private System.Windows.Forms.DataGridView dgvEnPreparacion;
        private System.Windows.Forms.Button btnTomarPedido;
        private System.Windows.Forms.Button btnMarcarTerminado;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Label lblDetalle;
    }
}