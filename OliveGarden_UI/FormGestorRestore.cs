using BE;
using BLL_Negocio;
using System;
using System.Windows.Forms;

namespace OliveGarden_UI
{
    public partial class FormGestorRestore : Form
    {
        #region "Atributos"
        private BEUsuario _usuarioLogueado;
        BLLGestorBD oBLLGestorBD = new BLLGestorBD();
        BLLBitacora oBLLBitacora = new BLLBitacora();
        #endregion

        #region "Constructor"
        public FormGestorRestore(BEUsuario pUsuario)
        {
            InitializeComponent();
            _usuarioLogueado = pUsuario;
        }
        #endregion

        #region "Eventos"
        private void FormGestorRestore_Load(object sender, EventArgs e)
        {
            CargarArbolBackups();
        }

        private void trvBackups_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lblSeleccionado.Text = $"Seleccionado: {e.Node.Text}";
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (trvBackups.SelectedNode == null)
                    throw new Exception("Seleccioná un backup de la lista.");

                string nombreBackup = trvBackups.SelectedNode.Text;

                var confirmacion = MessageBox.Show(
                    $"¿Estás segura de que querés restaurar el backup '{nombreBackup}'?\n" +
                    "Esta acción reemplazará la base de datos actual.",
                    "Confirmar Restore",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    oBLLGestorBD.CrearRestore(nombreBackup);

                    // Registrar en bitácora
                    BEBitacora registro = new BEBitacora(0, DateTime.Now, "restore")
                    {
                        oBEUsuario = _usuarioLogueado
                    };
                    oBLLBitacora.Guardar(registro);

                    MessageBox.Show("Restore realizado correctamente.", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Métodos"
        private void CargarArbolBackups()
        {
            try
            {
                trvBackups.Nodes.Clear();
                var lista = oBLLGestorBD.ListarBackups();

                TreeNode raiz = new TreeNode("Backups disponibles");
                if (lista != null)
                    foreach (var backup in lista)
                        raiz.Nodes.Add(new TreeNode(backup));

                trvBackups.Nodes.Add(raiz);
                trvBackups.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}