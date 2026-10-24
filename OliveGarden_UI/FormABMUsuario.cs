using BE;
using BLL_Negocio;
using System;
using System.Windows.Forms;

namespace OliveGarden_UI
{
    public partial class FormABMUsuario : Form
    {
        #region "Atributos"
        private BEUsuario _usuarioLogueado;
        BLLUsuario oBLLUsuario = new BLLUsuario();
        #endregion

        #region "Constructor"
        public FormABMUsuario(BEUsuario pUsuario)
        {
            InitializeComponent();
            _usuarioLogueado = pUsuario;
        }
        #endregion

        // Controles necesarios:
        // - DataGridView: dgvUsuarios
        // - TextBox: txtUsuario, txtPassword
        // - CheckBox: chkActivo (texto: "Activo"), chkBloqueado (texto: "Bloqueado")
        // - Button: btnGuardar ("Guardar"), btnEliminar ("Eliminar"), btnLimpiar ("Limpiar")
        // - Label: lblId (hidden, guarda el Id del registro seleccionado)

        #region "Eventos"
        private void FormABMUsuario_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            chkActivo.Checked = true;
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0) return;
            var fila = dgvUsuarios.SelectedRows[0];
            lblId.Text = fila.Cells["Id"].Value.ToString();
            txtUsuario.Text = fila.Cells["Usuario"].Value.ToString();
            txtPassword.Text = "";
            chkActivo.Checked = (bool)fila.Cells["Activo"].Value;
            chkBloqueado.Checked = (bool)fila.Cells["Bloqueado"].Value;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                    throw new Exception("Ingresá un nombre de usuario.");
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                    throw new Exception("Ingresá una contraseña.");

                long id = string.IsNullOrWhiteSpace(lblId.Text) ? 0 : long.Parse(lblId.Text);

                BEUsuario oBEUsuario = new BEUsuario
                {
                    Id = id,
                    Usuario = txtUsuario.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Activo = chkActivo.Checked,
                    Bloqueado = chkBloqueado.Checked
                };

                oBLLUsuario.Guardar(oBEUsuario);
                CargarGrilla();
                Limpiar();
                MessageBox.Show("Usuario guardado correctamente.", "OK",
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
                    throw new Exception("Seleccioná un usuario de la grilla.");

                var confirmacion = MessageBox.Show(
                    "¿Estás segura de que querés eliminar este usuario?",
                    "Eliminar usuario",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    oBLLUsuario.Eliminar(new BEUsuario { Id = long.Parse(lblId.Text) });
                    CargarGrilla();
                    Limpiar();
                    MessageBox.Show("Usuario eliminado correctamente.", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
        private void CargarGrilla()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = oBLLUsuario.ListarTodo();

            if (dgvUsuarios.Columns.Count > 0)
                dgvUsuarios.Columns["Password"].Visible = false;
        }

        private void Limpiar()
        {
            lblId.Text = "";
            txtUsuario.Text = "";
            txtPassword.Text = "";
            chkActivo.Checked = true;
            chkBloqueado.Checked = false;
            dgvUsuarios.ClearSelection();
        }
        #endregion

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}