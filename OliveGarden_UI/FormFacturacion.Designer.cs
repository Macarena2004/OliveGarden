namespace OliveGarden_UI
{
    partial class FormFacturacion
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
            this.cmbMesas = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCargarPedidos = new System.Windows.Forms.Button();
            this.gbDetalle = new System.Windows.Forms.GroupBox();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.gbPago = new System.Windows.Forms.GroupBox();
            this.cmbMetodoPago = new System.Windows.Forms.ComboBox();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenerarFactura = new System.Windows.Forms.Button();
            this.btnRegistrarPago = new System.Windows.Forms.Button();
            this.lblIdFactura = new System.Windows.Forms.Label();
            this.lblIdCliente = new System.Windows.Forms.Label();
            this.gbDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.gbPago.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbMesas
            // 
            this.cmbMesas.FormattingEnabled = true;
            this.cmbMesas.Location = new System.Drawing.Point(12, 46);
            this.cmbMesas.Name = "cmbMesas";
            this.cmbMesas.Size = new System.Drawing.Size(121, 21);
            this.cmbMesas.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mesa";
            // 
            // btnCargarPedidos
            // 
            this.btnCargarPedidos.Location = new System.Drawing.Point(15, 86);
            this.btnCargarPedidos.Name = "btnCargarPedidos";
            this.btnCargarPedidos.Size = new System.Drawing.Size(75, 47);
            this.btnCargarPedidos.TabIndex = 2;
            this.btnCargarPedidos.Text = "Cargar pedidos de la mesa";
            this.btnCargarPedidos.UseVisualStyleBackColor = true;
            // 
            // gbDetalle
            // 
            this.gbDetalle.Controls.Add(this.dgvDetalle);
            this.gbDetalle.Location = new System.Drawing.Point(151, 12);
            this.gbDetalle.Name = "gbDetalle";
            this.gbDetalle.Size = new System.Drawing.Size(303, 228);
            this.gbDetalle.TabIndex = 3;
            this.gbDetalle.TabStop = false;
            this.gbDetalle.Text = "Detalle de consumo";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Location = new System.Drawing.Point(18, 34);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.Size = new System.Drawing.Size(268, 172);
            this.dgvDetalle.TabIndex = 0;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(166, 260);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(79, 13);
            this.lblSubtotal.TabIndex = 4;
            this.lblSubtotal.Text = "Subtotal: $0.00";
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.Location = new System.Drawing.Point(166, 292);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(79, 13);
            this.lblDescuento.TabIndex = 5;
            this.lblDescuento.Text = "Descuento: 0%";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(166, 326);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(75, 13);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "TOTAL: $0.00";
            // 
            // gbPago
            // 
            this.gbPago.Controls.Add(this.lblIdCliente);
            this.gbPago.Controls.Add(this.lblIdFactura);
            this.gbPago.Controls.Add(this.btnRegistrarPago);
            this.gbPago.Controls.Add(this.btnGenerarFactura);
            this.gbPago.Controls.Add(this.label2);
            this.gbPago.Controls.Add(this.txtMonto);
            this.gbPago.Controls.Add(this.cmbMetodoPago);
            this.gbPago.Location = new System.Drawing.Point(470, 12);
            this.gbPago.Name = "gbPago";
            this.gbPago.Size = new System.Drawing.Size(303, 228);
            this.gbPago.TabIndex = 7;
            this.gbPago.TabStop = false;
            this.gbPago.Text = "Registrar Pago";
            // 
            // cmbMetodoPago
            // 
            this.cmbMetodoPago.FormattingEnabled = true;
            this.cmbMetodoPago.Items.AddRange(new object[] {
            "Efectivo",
            "Tarjeta",
            "MercadoPago"});
            this.cmbMetodoPago.Location = new System.Drawing.Point(16, 31);
            this.cmbMetodoPago.Name = "cmbMetodoPago";
            this.cmbMetodoPago.Size = new System.Drawing.Size(121, 21);
            this.cmbMetodoPago.TabIndex = 0;
            // 
            // txtMonto
            // 
            this.txtMonto.Location = new System.Drawing.Point(16, 88);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(100, 20);
            this.txtMonto.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Monto";
            // 
            // btnGenerarFactura
            // 
            this.btnGenerarFactura.Location = new System.Drawing.Point(24, 139);
            this.btnGenerarFactura.Name = "btnGenerarFactura";
            this.btnGenerarFactura.Size = new System.Drawing.Size(97, 23);
            this.btnGenerarFactura.TabIndex = 3;
            this.btnGenerarFactura.Text = "Generar Factura";
            this.btnGenerarFactura.UseVisualStyleBackColor = true;
            // 
            // btnRegistrarPago
            // 
            this.btnRegistrarPago.Location = new System.Drawing.Point(22, 193);
            this.btnRegistrarPago.Name = "btnRegistrarPago";
            this.btnRegistrarPago.Size = new System.Drawing.Size(99, 23);
            this.btnRegistrarPago.TabIndex = 4;
            this.btnRegistrarPago.Text = "Registrar Pago";
            this.btnRegistrarPago.UseVisualStyleBackColor = true;
            // 
            // lblIdFactura
            // 
            this.lblIdFactura.AutoSize = true;
            this.lblIdFactura.Location = new System.Drawing.Point(140, 146);
            this.lblIdFactura.Name = "lblIdFactura";
            this.lblIdFactura.Size = new System.Drawing.Size(35, 13);
            this.lblIdFactura.TabIndex = 5;
            this.lblIdFactura.Text = "label3";
            this.lblIdFactura.Visible = false;
            // 
            // lblIdCliente
            // 
            this.lblIdCliente.AutoSize = true;
            this.lblIdCliente.Location = new System.Drawing.Point(143, 201);
            this.lblIdCliente.Name = "lblIdCliente";
            this.lblIdCliente.Size = new System.Drawing.Size(35, 13);
            this.lblIdCliente.TabIndex = 6;
            this.lblIdCliente.Text = "label3";
            this.lblIdCliente.Visible = false;
            // 
            // FormFacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbPago);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblDescuento);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.gbDetalle);
            this.Controls.Add(this.btnCargarPedidos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbMesas);
            this.Name = "FormFacturacion";
            this.Text = "FormFacturacion";
            this.gbDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.gbPago.ResumeLayout(false);
            this.gbPago.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            this.btnCargarPedidos.Click += new System.EventHandler(this.btnCargarPedidos_Click);
            this.btnGenerarFactura.Click += new System.EventHandler(this.btnGenerarFactura_Click);
            this.btnRegistrarPago.Click += new System.EventHandler(this.btnRegistrarPago_Click);
            this.Load += new System.EventHandler(this.FormFacturacion_Load);
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMesas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCargarPedidos;
        private System.Windows.Forms.GroupBox gbDetalle;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.GroupBox gbPago;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.ComboBox cmbMetodoPago;
        private System.Windows.Forms.Button btnRegistrarPago;
        private System.Windows.Forms.Button btnGenerarFactura;
        private System.Windows.Forms.Label lblIdCliente;
        private System.Windows.Forms.Label lblIdFactura;
    }
}