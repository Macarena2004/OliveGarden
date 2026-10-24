using BE;
using BLL_Negocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OliveGarden_UI
{
    public partial class FormPedidos : Form
    {
        #region "Atributos"
        private BEUsuario _usuarioLogueado;
        private List<BEDetallePedido> _listaDetalles = new List<BEDetallePedido>();
        BLLPedido oBLLPedido = new BLLPedido();
        BLLProducto oBLLProducto = new BLLProducto();
        BLLMesa oBLLMesa = new BLLMesa();
        #endregion

        #region "Constructor"
        public FormPedidos(BEUsuario pUsuario)
        {
            InitializeComponent();
            _usuarioLogueado = pUsuario;
        }
        #endregion

        #region "Carga"
        private void FormPedidos_Load(object sender, EventArgs e)
        {
            CargarMesas();
            CargarProductos();
            ActualizarGrillaDetalle();
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

        private void CargarProductos()
        {
            try
            {
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = oBLLProducto.ListarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarGrillaDetalle()
        {
            dgvDetalle.DataSource = null;
            dgvDetalle.DataSource = new List<BEDetallePedido>(_listaDetalles);
            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            double total = 0;
            foreach (var d in _listaDetalles)
                total += d.CalcularSubtotal();
            lblTotal.Text = $"Total: ${total:F2}";
        }
        #endregion

        #region "Botones"
        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductos.SelectedRows.Count == 0)
                    throw new Exception("Seleccioná un producto.");

                var fila = dgvProductos.SelectedRows[0];
                long idProducto = long.Parse(fila.Cells["Id"].Value.ToString());
                string nombreProd = fila.Cells["NombreProducto"].Value.ToString();
                double precioProd = double.Parse(fila.Cells["Precio"].Value.ToString());
                int stockProd = int.Parse(fila.Cells["Stock"].Value.ToString());
                int cantidad = (int)nudCantidad.Value;

                if (stockProd < cantidad)
                    throw new Exception($"Stock insuficiente. Disponible: {stockProd}");

                // Verificar si ya está en el detalle
                BEDetallePedido existente = _listaDetalles.Find(d => d.IdProducto == idProducto);
                if (existente != null)
                {
                    existente.Cantidad += cantidad;
                    existente.Subtotal = existente.CalcularSubtotal();
                }
                else
                {
                    _listaDetalles.Add(new BEDetallePedido(
                        _listaDetalles.Count + 1,
                        0,
                        idProducto,
                        nombreProd,
                        cantidad,
                        precioProd));
                }

                ActualizarGrillaDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetalle.SelectedRows.Count == 0)
                    throw new Exception("Seleccioná un ítem del detalle.");

                long idProducto = long.Parse(dgvDetalle.SelectedRows[0].Cells["IdProducto"].Value.ToString());
                _listaDetalles.RemoveAll(d => d.IdProducto == idProducto);
                ActualizarGrillaDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfirmarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMesas.SelectedItem == null)
                    throw new Exception("Seleccioná una mesa.");
                if (_listaDetalles.Count == 0)
                    throw new Exception("Agregá al menos un producto al pedido.");

                BEMesa mesa = (BEMesa)cmbMesas.SelectedItem;
                BEPedido pedido = new BEPedido(0, mesa.Id);
                pedido.listaDetalles = _listaDetalles;

                oBLLPedido.Guardar(pedido);
                MessageBox.Show("Pedido confirmado y enviado a cocina.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        #endregion

        #region "Métodos"
        private void Limpiar()
        {
            _listaDetalles.Clear();
            ActualizarGrillaDetalle();
            nudCantidad.Value = 1;
            dgvProductos.ClearSelection();
        }
        #endregion

        private void btnEliminarDetalle_Click_1(object sender, EventArgs e)
        {

        }
    }
}