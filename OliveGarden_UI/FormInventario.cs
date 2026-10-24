using BE;
using BLL_Negocio;
using System;
using System.Windows.Forms;

namespace OliveGarden_UI
{
    public partial class FormInventario : Form
    {
        #region "Atributos"
        private BEUsuario _usuarioLogueado;
        BLLProducto oBLLProducto = new BLLProducto();
        BLLOrdenDeCompra oBLLOrden = new BLLOrdenDeCompra();
        #endregion

        #region "Constructor"
        public FormInventario(BEUsuario pUsuario)
        {
            InitializeComponent();
            _usuarioLogueado = pUsuario;
        }
        #endregion

        #region "Carga"
        private void FormInventario_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            CargarCriticos();
        }

        private void CargarGrilla()
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

        private void CargarCriticos()
        {
            try
            {
                var criticos = oBLLOrden.ObtenerProductosCriticos();
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = criticos;

                if (criticos != null && criticos.Count > 0)
                    MessageBox.Show(
                        $"⚠️ Hay {criticos.Count} producto(s) con stock crítico.",
                        "Alerta de stock",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Eventos grilla"
        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0) return;
            var fila = dgvProductos.SelectedRows[0];
            lblId.Text = fila.Cells["Id"].Value.ToString();
            txtNombre.Text = fila.Cells["NombreProducto"].Value.ToString();
            txtDescripcion.Text = fila.Cells["Descripcion"].Value.ToString();
            txtPrecio.Text = fila.Cells["Precio"].Value.ToString();
            nudStock.Value = int.Parse(fila.Cells["Stock"].Value.ToString());
        }
        #endregion

        #region "Botones"
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                    throw new Exception("Ingresá el nombre del producto.");
                if (string.IsNullOrWhiteSpace(txtPrecio.Text))
                    throw new Exception("Ingresá el precio del producto.");

                long id = string.IsNullOrWhiteSpace(lblId.Text) ? 0 : long.Parse(lblId.Text);

                BEProducto producto = new BEProducto(
                    id,
                    txtNombre.Text.Trim(),
                    txtDescripcion.Text.Trim(),
                    double.Parse(txtPrecio.Text.Replace(",", ".")),
                    (int)nudStock.Value);

                oBLLProducto.Guardar(producto);
                CargarGrilla();
                CargarCriticos();
                Limpiar();
                MessageBox.Show("Producto guardado correctamente.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lblId.Text))
                    throw new Exception("Seleccioná un producto de la grilla.");

                var confirmacion = MessageBox.Show(
                    "¿Estás segura de que querés eliminar este producto?",
                    "Eliminar producto",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    oBLLProducto.Eliminar(new BEProducto { Id = long.Parse(lblId.Text) });
                    CargarGrilla();
                    CargarCriticos();
                    Limpiar();
                    MessageBox.Show("Producto eliminado correctamente.", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerarOrden_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductos.SelectedRows.Count == 0)
                    throw new Exception("Seleccioná un producto crítico de la lista.");

                var fila = dgvProductos.SelectedRows[0];
                BEProducto producto = new BEProducto
                {
                    Id = long.Parse(fila.Cells["Id"].Value.ToString()),
                    NombreProducto = fila.Cells["NombreProducto"].Value.ToString()
                };

                int cantidad = (int)nudCantidadOrden.Value;
                if (cantidad <= 0)
                    throw new Exception("La cantidad a pedir debe ser mayor a cero.");

                oBLLOrden.GenerarOrden(producto, cantidad);
                MessageBox.Show(
                    $"Orden de compra generada para {producto.NombreProducto}.\nCantidad: {cantidad}",
                    "Orden generada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarCriticos();
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
            lblId.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            nudStock.Value = 0;
            dgvProductos.ClearSelection();
        }
        #endregion
    }
}