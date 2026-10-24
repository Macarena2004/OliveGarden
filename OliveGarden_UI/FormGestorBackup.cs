using BE;
using BLL_Negocio;
using System;
using System.Windows.Forms;

namespace OliveGarden_UI
{
    public partial class FormGestorBackup : Form
    {
        #region "Atributos"
        private BEUsuario _usuarioLogueado;
        BLLGestorBD oBLLGestorBD = new BLLGestorBD();
        BLLBitacora oBLLBitacora = new BLLBitacora();
        #endregion

        #region "Constructor"
        public FormGestorBackup(BEUsuario pUsuario)
        {
            InitializeComponent();
            _usuarioLogueado = pUsuario;
        }
        #endregion

        #region "Eventos"
        private void FormGestorBackup_Load(object sender, EventArgs e)
        {
            CargarListaBackups();
        }

        private void btnCrearBackup_Click(object sender, EventArgs e)
        {
            try
            {
                oBLLGestorBD.CrearBackup();

                // Registrar en bitácora
                BEBitacora registro = new BEBitacora(0, DateTime.Now, "backup")
                {
                    oBEUsuario = _usuarioLogueado
                };
                oBLLBitacora.Guardar(registro);

                CargarListaBackups();
                MessageBox.Show("Backup creado correctamente.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Métodos"
        private void CargarListaBackups()
        {
            try
            {
                var lista = oBLLGestorBD.ListarBackups();
                dgvBackups.DataSource = null;
                dgvBackups.DataSource = lista;

                if (lista != null && lista.Count > 0)
                    lblUltimoBackup.Text = $"Último backup: {lista[lista.Count - 1]}";
                else
                    lblUltimoBackup.Text = "No hay backups disponibles.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void FormGestorBackup_Load_1(object sender, EventArgs e)
        {

        }
    }
}
