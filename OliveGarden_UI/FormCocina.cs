using BE;
using BLL_Negocio;
using System;
using System.Windows.Forms;

namespace OliveGarden_UI
{
    public partial class FormCocina : Form
    {
        #region "Atributos"
        private BEUsuario _usuarioLogueado;
        BLLPedido oBLLPedido = new BLLPedido();
        #endregion

        #region "Constructor"
        public FormCocina(BEUsuario pUsuario)
        {
            InitializeComponent();
            _usuarioLogueado = pUsuario;
        }
        #endregion

        #region "Carga"
        private void FormCocina_Load(object sender, EventArgs e)
        {
            CargarGrillas();
        }

        private void CargarGrillas()
        {
            try
            {
                dgvPendientes.DataSource = null;
                dgvPendientes.DataSource = oBLLPedido.ListarPorEstado("Pendiente");
                dgvEnPreparacion.DataSource = null;
                dgvEnPreparacion.DataSource = oBLLPedido.ListarPorEstado("EnPreparacion");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Botones"
        private void btnTomarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPendientes.SelectedRows.Count == 0)
                    throw new Exception("Seleccioná un pedido pendiente.");

                long idPedido = long.Parse(dgvPendientes.SelectedRows[0].Cells["Id"].Value.ToString());
                BEPedido pedido = oBLLPedido.ListarObjeto(new BEPedido { Id = idPedido });
                pedido.MarcarEnPreparacion();
                oBLLPedido.ActualizarEstado(pedido);

                CargarGrillas();
                MessageBox.Show("Pedido marcado como En Preparación.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMarcarTerminado_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEnPreparacion.SelectedRows.Count == 0)
                    throw new Exception("Seleccioná un pedido en preparación.");

                long idPedido = long.Parse(dgvEnPreparacion.SelectedRows[0].Cells["Id"].Value.ToString());
                BEPedido pedido = oBLLPedido.ListarObjeto(new BEPedido { Id = idPedido });
                pedido.MarcarTerminado();
                oBLLPedido.ActualizarEstado(pedido);

                CargarGrillas();
                MessageBox.Show("Pedido listo. El mozo fue notificado.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarGrillas();
        }
        #endregion
    }
}