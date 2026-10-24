using BE;
using BE.BEComposite;
using BLL_Negocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OliveGarden_UI
{
    public partial class FormMenu : Form
    {
        #region "Atributos"
        private BEUsuario _usuarioLogueado;
        private List<BEPermiso> _listaPermisos;
        #endregion

        #region "Constructor"
        public FormMenu(BEUsuario pUsuario)
        {
            InitializeComponent();
            _usuarioLogueado = pUsuario;
        }
        #endregion

        #region "Eventos — Load y Timer"
        private void FormMenu_Load(object sender, EventArgs e)
        {
            try
            {
                lblUsuarioLogueado1.Text = $"Usuario: {_usuarioLogueado.Usuario}";
                timerReloj1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timerReloj1_Tick(object sender, EventArgs e)
        {
            lblFechaHora1.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
        #endregion

        #region "Eventos — Ítems del menú"

        // ── Ventas ────────────────────────────────────────────────
        private void reservarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormReservas(_usuarioLogueado));
        }

        private void modificarReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormReservas(_usuarioLogueado));
        }

        private void cancelarReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormReservas(_usuarioLogueado));
        }

        // ── Pedidos ───────────────────────────────────────────────
        private void realizarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormPedidos(_usuarioLogueado));
        }

        private void prepararPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormCocina(_usuarioLogueado));
        }

        // ── Facturación ───────────────────────────────────────────
        private void generarFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormFacturacion(_usuarioLogueado));
        }

        private void registrarPagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormFacturacion(_usuarioLogueado));
        }

        // ── Inventario ────────────────────────────────────────────
        private void registrarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormInventario(_usuarioLogueado));
        }

        private void generarOrdenDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormInventario(_usuarioLogueado));
        }

        // ── Reportes ──────────────────────────────────────────────
        private void generarReporteFinancieroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormReportes(_usuarioLogueado));
        }

        // ── Administración ────────────────────────────────────────
        private void gestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormABMUsuario(_usuarioLogueado));
        }

        private void rolesYPermisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormRolesPermisos(_usuarioLogueado));
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormGestorBackup(_usuarioLogueado));
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormGestorRestore(_usuarioLogueado));
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new FormBitacora(_usuarioLogueado));
        }

        // ── Salir ─────────────────────────────────────────────────
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarSesion();
        }
        #endregion

        #region "Métodos"
        private void AbrirForm(Form formulario)
        {
            formulario.MdiParent = this;
            formulario.WindowState = FormWindowState.Maximized;
            formulario.Show();
        }

        private void CerrarSesion()
        {
            var respuesta = MessageBox.Show(
                "¿Estás segura de que querés cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                this.Hide();
                Program.IniciarLogin();
            }
        }
        #endregion
    }
}

