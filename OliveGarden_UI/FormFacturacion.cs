using BE;
using BLL_Negocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OliveGarden_UI
{
    public partial class FormFacturacion : Form
    {
        #region "Atributos"
        private BEUsuario _usuarioLogueado;
        private double _totalSinDescuento = 0;
        BLLFactura oBLLFactura = new BLLFactura();
        BLLPedido oBLLPedido = new BLLPedido();
        BLLMesa oBLLMesa = new BLLMesa();
        BLLCliente oBLLCliente = new BLLCliente();
        #endregion

        #region "Constructor"
        public FormFacturacion(BEUsuario pUsuario)
        {
            InitializeComponent();
            _usuarioLogueado = pUsuario;
        }
        #endregion

        #region "Carga"
        private void FormFacturacion_Load(object sender, EventArgs e)
        {
            CargarMesas();
            cmbMetodoPago.Items.AddRange(new string[] { "Efectivo", "Tarjeta", "MercadoPago" });
            cmbMetodoPago.SelectedIndex = 0;
            btnRegistrarPago.Enabled = false;
        }

        private void CargarMesas()
        {
            try
            {
                cmbMesas.DataSource = oBLLMesa.ListarTodo();
                cmbMesas.DisplayMember = "NumeroMesa";
                cmbMesas.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Botones"
        private void btnCargarPedidos_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMesas.SelectedItem == null)
                    throw new Exception("Seleccioná una mesa.");

                BEMesa mesa = (BEMesa)cmbMesas.SelectedItem;
                List<BEPedido> pedidos = oBLLPedido.ListarPorMesa(mesa.Id);

                if (pedidos == null || pedidos.Count == 0)
                    throw new Exception("No hay pedidos entregados para esta mesa.");

                // Consolidar todos los detalles
                List<BEDetallePedido> todosLosDetalles = new List<BEDetallePedido>();
                foreach (var pedido in pedidos)
                {
                    BEPedido pedidoCompleto = oBLLPedido.ListarObjeto(new BEPedido { Id = pedido.Id });
                    todosLosDetalles.AddRange(pedidoCompleto.listaDetalles);
                }

                dgvDetalle.DataSource = null;
                dgvDetalle.DataSource = todosLosDetalles;

                // Calcular subtotal
                _totalSinDescuento = 0;
                foreach (var d in todosLosDetalles)
                    _totalSinDescuento += d.CalcularSubtotal();

                lblSubtotal.Text = $"Subtotal: ${_totalSinDescuento:F2}";
                lblDescuento.Text = "Descuento: pendiente de generar factura";
                lblTotal.Text = $"TOTAL: ${_totalSinDescuento:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetalle.Rows.Count == 0)
                    throw new Exception("Primero cargá los pedidos de la mesa.");

                BEMesa mesa = (BEMesa)cmbMesas.SelectedItem;

                // Pedir el email del cliente para identificarlo
                string email = Microsoft.VisualBasic.Interaction.InputBox(
                    "Ingresá el email del cliente para aplicar descuentos:",
                    "Identificar cliente", "");

                long idCliente = 0;
                if (!string.IsNullOrWhiteSpace(email))
                {
                    List<BECliente> clientes = oBLLCliente.ListarTodo();
                    BECliente cliente = clientes.Find(c =>
                        c.Email.Trim().ToLower() == email.Trim().ToLower());
                    if (cliente != null) idCliente = cliente.Id;
                }

                // Armar lista de detalles desde la grilla
                List<BEDetallePedido> detalles = new List<BEDetallePedido>();
                foreach (DataGridViewRow fila in dgvDetalle.Rows)
                {
                    if (fila.IsNewRow) continue;
                    detalles.Add(new BEDetallePedido
                    {
                        IdProducto = long.Parse(fila.Cells["IdProducto"].Value.ToString()),
                        NombreProducto = fila.Cells["NombreProducto"].Value.ToString(),
                        Cantidad = int.Parse(fila.Cells["Cantidad"].Value.ToString()),
                        PrecioUnitario = double.Parse(fila.Cells["PrecioUnitario"].Value.ToString()),
                        Subtotal = double.Parse(fila.Cells["Subtotal"].Value.ToString())
                    });
                }

                BEFactura factura = oBLLFactura.GenerarFactura(mesa.Id, idCliente, detalles);

                lblIdFactura.Text = factura.Id.ToString();
                lblIdCliente.Text = idCliente.ToString();

                double descuento = _totalSinDescuento - factura.TotalFactura;
                double porcentaje = _totalSinDescuento > 0
                    ? (descuento / _totalSinDescuento) * 100 : 0;

                lblDescuento.Text = $"Descuento: {porcentaje:F0}% (- ${descuento:F2})";
                lblTotal.Text = $"TOTAL: ${factura.TotalFactura:F2}";
                txtMonto.Text = factura.TotalFactura.ToString("F2");

                btnRegistrarPago.Enabled = true;
                btnGenerarFactura.Enabled = false;

                MessageBox.Show($"Factura N°{factura.Id} generada.\nTotal: ${factura.TotalFactura:F2}",
                    "Factura generada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lblIdFactura.Text))
                    throw new Exception("Primero generá la factura.");
                if (string.IsNullOrWhiteSpace(txtMonto.Text))
                    throw new Exception("Ingresá el monto recibido.");

                double monto = double.Parse(txtMonto.Text.Replace(",", "."));
                long idFactura = long.Parse(lblIdFactura.Text);

                BEFactura factura = oBLLFactura.ListarObjeto(new BEFactura { Id = idFactura });

                if (monto < factura.TotalFactura)
                    throw new Exception($"El monto es insuficiente. Total a pagar: ${factura.TotalFactura:F2}");

                // Marcar factura como pagada
                factura.MarcarPagada();
                oBLLFactura.Guardar(factura);

                // Liberar la mesa
                BEMesa mesa = oBLLMesa.ListarObjeto(new BEMesa { Id = factura.IdMesa });
                mesa.Disponibilizar();
                oBLLMesa.ActualizarEstado(mesa);

                MessageBox.Show(
                    $"Pago registrado correctamente.\n" +
                    $"Método: {cmbMetodoPago.SelectedItem}\n" +
                    $"Monto recibido: ${monto:F2}\n" +
                    $"Vuelto: ${monto - factura.TotalFactura:F2}",
                    "Pago registrado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Métodos"
        private void Limpiar()
        {
            dgvDetalle.DataSource = null;
            lblSubtotal.Text = "Subtotal: $0.00";
            lblDescuento.Text = "Descuento: 0%";
            lblTotal.Text = "TOTAL: $0.00";
            lblIdFactura.Text = "";
            lblIdCliente.Text = "";
            txtMonto.Text = "";
            _totalSinDescuento = 0;
            btnRegistrarPago.Enabled = false;
            btnGenerarFactura.Enabled = true;
            CargarMesas();
        }
        #endregion
    }
}
