using BE;
using BLL_Negocio;
using System;
using System.Windows.Forms;

namespace OliveGarden_UI
{
    public partial class FormBitacora : Form
    {
        #region "Atributos"
        private BEUsuario _usuarioLogueado;
        BLLBitacora oBLLBitacora = new BLLBitacora();
        BLLUsuario oBLLUsuario = new BLLUsuario();
        #endregion

        #region "Constructor"
        public FormBitacora(BEUsuario pUsuario)
        {
            InitializeComponent();
            _usuarioLogueado = pUsuario;
        }
        #endregion

        #region "Eventos"
        private void FormBitacora_Load(object sender, EventArgs e)
        {
            rbTodos.Checked = true;
            CargarTodos();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (rbTodos.Checked)          CargarTodos();
            else if (rbBackups.Checked)   CargarPorTipo(true);
            else if (rbRestores.Checked)  CargarPorTipo(false);
        }
        #endregion

        #region "Métodos"
        private void CargarTodos()
        {
            try
            {
                var lista = oBLLBitacora.ListarTodo();
                EnriquecerBitacora(lista);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPorTipo(bool esBackup)
        {
            try
            {
                var lista = oBLLBitacora.ListarTodoPorTipo(esBackup);
                EnriquecerBitacora(lista);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnriquecerBitacora(System.Collections.Generic.List<BEBitacora> lista)
        {
            // Completar el nombre de usuario para mostrar
            var listaUsuarios = oBLLUsuario.ListarTodo();
            foreach (var reg in lista)
            {
                var u = listaUsuarios.Find(x => x.Id == reg.oBEUsuario.Id);
                if (u != null) reg.oBEUsuario.Usuario = u.Usuario;
            }

            dgvBitacora.DataSource = null;
            dgvBitacora.DataSource = lista;

            // Ajustar columnas visibles
            if (dgvBitacora.Columns.Count > 0)
            {
                dgvBitacora.Columns["Id"].HeaderText = "N°";
                dgvBitacora.Columns["FechaRegistro"].HeaderText = "Fecha y Hora";
                dgvBitacora.Columns["Detalle"].HeaderText = "Acción";
                dgvBitacora.Columns["oBEUsuario"].Visible = false;
            }
        }
        #endregion
    }
}
