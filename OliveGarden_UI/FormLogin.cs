using BE;
using BLL_Negocio;
using System;
using System.Windows.Forms;

namespace OliveGarden_UI
{
    public partial class FormLogin : Form
    {
        #region "Atributos"
        private int intentosFallidos = 0;
        private const int MAX_INTENTOS = 3;
        public BEUsuario UsuarioLogueado { get; private set; }
        #endregion

        #region "Constructor"
        public FormLogin()
        {
            InitializeComponent();
        }
        #endregion

        #region "Eventos"
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                    throw new Exception("Ingresá un nombre de usuario.");
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                    throw new Exception("Ingresá una contraseña.");

                BEUsuario oBEUsuario = new BEUsuario
                {
                    Usuario  = txtUsuario.Text.Trim(),
                    Password = txtPassword.Text.Trim()
                };

                BLLUsuario oBLLUsuario = new BLLUsuario();
                if (oBLLUsuario.Login(oBEUsuario))
                {
                    UsuarioLogueado = oBLLUsuario.ListarObjeto(oBEUsuario);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                intentosFallidos++;
                lblError.Text    = ex.Message;
                lblError.Visible = true;
                txtPassword.Clear();
                txtPassword.Focus();
                if (intentosFallidos >= MAX_INTENTOS)
                    BloquearUsuario();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnIngresar_Click(sender, e);
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }
        #endregion

        #region "Métodos"
        private void BloquearUsuario()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    BLLUsuario oBLLUsuario = new BLLUsuario();
                    BEUsuario oBEUsuario = oBLLUsuario.ListarObjeto(
                        new BEUsuario { Usuario = txtUsuario.Text.Trim() });
                    if (oBEUsuario != null)
                    {
                        oBEUsuario.Bloqueado = true;
                        oBLLUsuario.Guardar(oBEUsuario);
                    }
                }
            }
            catch { }
            finally
            {
                MessageBox.Show(
                    "Superaste el número máximo de intentos. El usuario fue bloqueado.\nContactá al administrador.",
                    "Usuario bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        #endregion
    }
}
