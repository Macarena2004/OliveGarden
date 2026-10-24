namespace OliveGarden_UI
{
    partial class FormReportes
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
            this.gbFiltros = new System.Windows.Forms.GroupBox();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.gbResultados = new System.Windows.Forms.GroupBox();
            this.lblTotalFacturado = new System.Windows.Forms.Label();
            this.dgvFacturas = new System.Windows.Forms.DataGridView();
            this.lblCantidadFacturas = new System.Windows.Forms.Label();
            this.lblPromedioTicket = new System.Windows.Forms.Label();
            this.gbFiltros.SuspendLayout();
            this.gbResultados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFiltros
            // 
            this.gbFiltros.Controls.Add(this.btnGenerar);
            this.gbFiltros.Controls.Add(this.dtpFin);
            this.gbFiltros.Controls.Add(this.label2);
            this.gbFiltros.Controls.Add(this.label1);
            this.gbFiltros.Controls.Add(this.dtpInicio);
            this.gbFiltros.Location = new System.Drawing.Point(12, 12);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new System.Drawing.Size(278, 202);
            this.gbFiltros.TabIndex = 0;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "Período";
            // 
            // dtpInicio
            // 
            this.dtpInicio.Location = new System.Drawing.Point(6, 50);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(200, 20);
            this.dtpInicio.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Desde";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hasta";
            // 
            // dtpFin
            // 
            this.dtpFin.Location = new System.Drawing.Point(9, 108);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(200, 20);
            this.dtpFin.TabIndex = 3;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(9, 146);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(98, 23);
            this.btnGenerar.TabIndex = 1;
            this.btnGenerar.Text = "Generar Reporte";
            this.btnGenerar.UseVisualStyleBackColor = true;
            // 
            // gbResultados
            // 
            this.gbResultados.Controls.Add(this.lblPromedioTicket);
            this.gbResultados.Controls.Add(this.lblCantidadFacturas);
            this.gbResultados.Controls.Add(this.dgvFacturas);
            this.gbResultados.Controls.Add(this.lblTotalFacturado);
            this.gbResultados.Location = new System.Drawing.Point(296, 12);
            this.gbResultados.Name = "gbResultados";
            this.gbResultados.Size = new System.Drawing.Size(352, 220);
            this.gbResultados.TabIndex = 1;
            this.gbResultados.TabStop = false;
            this.gbResultados.Text = "Resultados";
            // 
            // lblTotalFacturado
            // 
            this.lblTotalFacturado.AutoSize = true;
            this.lblTotalFacturado.Location = new System.Drawing.Point(6, 146);
            this.lblTotalFacturado.Name = "lblTotalFacturado";
            this.lblTotalFacturado.Size = new System.Drawing.Size(112, 13);
            this.lblTotalFacturado.TabIndex = 1;
            this.lblTotalFacturado.Text = "Total facturado: $0.00";
            // 
            // dgvFacturas
            // 
            this.dgvFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFacturas.Location = new System.Drawing.Point(6, 19);
            this.dgvFacturas.Name = "dgvFacturas";
            this.dgvFacturas.Size = new System.Drawing.Size(340, 124);
            this.dgvFacturas.TabIndex = 3;
            // 
            // lblCantidadFacturas
            // 
            this.lblCantidadFacturas.AutoSize = true;
            this.lblCantidadFacturas.Location = new System.Drawing.Point(9, 170);
            this.lblCantidadFacturas.Name = "lblCantidadFacturas";
            this.lblCantidadFacturas.Size = new System.Drawing.Size(117, 13);
            this.lblCantidadFacturas.TabIndex = 4;
            this.lblCantidadFacturas.Text = "Cantidad de facturas: 0";
            // 
            // lblPromedioTicket
            // 
            this.lblPromedioTicket.AutoSize = true;
            this.lblPromedioTicket.Location = new System.Drawing.Point(10, 199);
            this.lblPromedioTicket.Name = "lblPromedioTicket";
            this.lblPromedioTicket.Size = new System.Drawing.Size(116, 13);
            this.lblPromedioTicket.TabIndex = 5;
            this.lblPromedioTicket.Text = "Ticket promedio: $0.00";
            // 
            // FormReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 252);
            this.Controls.Add(this.gbResultados);
            this.Controls.Add(this.gbFiltros);
            this.Name = "FormReportes";
            this.Text = "FormReportes";
            this.gbFiltros.ResumeLayout(false);
            this.gbFiltros.PerformLayout();
            this.gbResultados.ResumeLayout(false);
            this.gbResultados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).EndInit();
            this.ResumeLayout(false);

            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            this.Load += new System.EventHandler(this.FormReportes_Load);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbFiltros;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.GroupBox gbResultados;
        private System.Windows.Forms.DataGridView dgvFacturas;
        private System.Windows.Forms.Label lblTotalFacturado;
        private System.Windows.Forms.Label lblPromedioTicket;
        private System.Windows.Forms.Label lblCantidadFacturas;
    }
}