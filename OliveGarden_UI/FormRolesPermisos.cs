using BE;
using BE.BEComposite;
using BLL_Negocio;
using BLL_Negocio.BLLComposite;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OliveGarden_UI
{
    public partial class FormRolesPermisos : Form
    {
        #region "Atributos"
        private BEUsuario _usuarioLogueado;
        BLLUsuario oBLLUsuario = new BLLUsuario();
        BLLRol oBLLRol = new BLLRol();
        BLLPermiso oBLLPermiso = new BLLPermiso();
        #endregion

        #region "Constructor"
        public FormRolesPermisos(BEUsuario pUsuario)
        {
            InitializeComponent();
            _usuarioLogueado = pUsuario;
        }
        #endregion

        #region "Carga inicial"
        private void FormRolesPermisos_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
            CargarRoles();
            CargarPermisos();
        }

        private void CargarUsuarios()
        {
            lstUsuarios.DataSource = null;
            lstUsuarios.DataSource = oBLLUsuario.ListarTodo();
            lstUsuarios.DisplayMember = "Usuario";
            lstUsuarios.ValueMember = "Id";
        }

        private void CargarRoles()
        {
            lstRoles.DataSource = null;
            lstRoles.DataSource = oBLLRol.ListarTodo();
            lstRoles.DisplayMember = "Nombre";
            lstRoles.ValueMember = "Id";
        }

        private void CargarPermisos()
        {
            lstPermisos.DataSource = null;
            lstPermisos.DataSource = oBLLPermiso.ListarTodo();
            lstPermisos.DisplayMember = "Nombre";
            lstPermisos.ValueMember = "Id";
        }

        private void CargarArbolUsuario(BEUsuario oBEUsuario)
        {
            trvUsuario.Nodes.Clear();
            if (oBEUsuario == null) return;

            TreeNode raiz = new TreeNode(oBEUsuario.Usuario);

            // Cargar permisos directos
            List<BEPermiso> permisos = oBLLUsuario.ListarTodosLosPermisosDelUsuario(oBEUsuario);
            BEUsuario usuarioCompleto = oBLLUsuario.ListarObjeto(oBEUsuario);

            // Roles del usuario
            TreeNode nodoRoles = new TreeNode("Roles");
            foreach (var rol in usuarioCompleto.listaRoles)
            {
                TreeNode nodoRol = new TreeNode(rol.Nombre);
                BERol rolCompleto = oBLLRol.ListarObjeto(rol);
                foreach (BEComposite permiso in rolCompleto.ObtenerHijos())
                    nodoRol.Nodes.Add(new TreeNode(permiso.Nombre));
                nodoRoles.Nodes.Add(nodoRol);
            }

            // Permisos directos del usuario
            TreeNode nodoPermisos = new TreeNode("Permisos directos");
            foreach (var p in usuarioCompleto.listaPermisos)
                nodoPermisos.Nodes.Add(new TreeNode(p.Nombre));

            raiz.Nodes.Add(nodoRoles);
            raiz.Nodes.Add(nodoPermisos);
            trvUsuario.Nodes.Add(raiz);
            trvUsuario.ExpandAll();
        }
        #endregion

        #region "Eventos — Selección"
        private void lstUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUsuarios.SelectedItem is BEUsuario usuario)
                CargarArbolUsuario(usuario);
        }
        #endregion

        #region "Eventos — Botones"
        private void btnAsociarRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstUsuarios.SelectedItem == null) throw new Exception("Seleccioná un usuario.");
                if (lstRoles.SelectedItem == null) throw new Exception("Seleccioná un rol.");

                BEUsuario usuario = (BEUsuario)lstUsuarios.SelectedItem;
                BERol rol = (BERol)lstRoles.SelectedItem;
                oBLLUsuario.AsociarUsuarioARol(usuario, rol);
                CargarArbolUsuario(usuario);
                MessageBox.Show("Rol asociado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnQuitarRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstUsuarios.SelectedItem == null) throw new Exception("Seleccioná un usuario.");
                if (lstRoles.SelectedItem == null) throw new Exception("Seleccioná un rol.");

                BEUsuario usuario = (BEUsuario)lstUsuarios.SelectedItem;
                BERol rol = (BERol)lstRoles.SelectedItem;
                oBLLUsuario.DesasociarUsuarioARol(usuario, rol);
                CargarArbolUsuario(usuario);
                MessageBox.Show("Rol quitado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAsociarPermiso_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstUsuarios.SelectedItem == null) throw new Exception("Seleccioná un usuario.");
                if (lstPermisos.SelectedItem == null) throw new Exception("Seleccioná un permiso.");

                BEUsuario usuario = (BEUsuario)lstUsuarios.SelectedItem;
                BEPermiso permiso = (BEPermiso)lstPermisos.SelectedItem;
                oBLLUsuario.AsociarPermisoAUsuario(usuario, permiso);
                CargarArbolUsuario(usuario);
                MessageBox.Show("Permiso asociado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnQuitarPermiso_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstUsuarios.SelectedItem == null) throw new Exception("Seleccioná un usuario.");
                if (lstPermisos.SelectedItem == null) throw new Exception("Seleccioná un permiso.");

                BEUsuario usuario = (BEUsuario)lstUsuarios.SelectedItem;
                BEPermiso permiso = (BEPermiso)lstPermisos.SelectedItem;
                oBLLUsuario.DesasociarPermisoAUsuario(usuario, permiso);
                CargarArbolUsuario(usuario);
                MessageBox.Show("Permiso quitado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAsociarPermisoARol_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstRoles.SelectedItem == null) throw new Exception("Seleccioná un rol.");
                if (lstPermisos.SelectedItem == null) throw new Exception("Seleccioná un permiso.");

                BERol rol = (BERol)lstRoles.SelectedItem;
                BEPermiso permiso = (BEPermiso)lstPermisos.SelectedItem;
                oBLLRol.AsociarRolAPermiso(rol, permiso);
                MessageBox.Show("Permiso asociado al rol correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnQuitarPermisoDeRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstRoles.SelectedItem == null) throw new Exception("Seleccioná un rol.");
                if (lstPermisos.SelectedItem == null) throw new Exception("Seleccioná un permiso.");

                BERol rol = (BERol)lstRoles.SelectedItem;
                BEPermiso permiso = (BEPermiso)lstPermisos.SelectedItem;
                oBLLRol.DesasociarRolAPermiso(rol, permiso);
                MessageBox.Show("Permiso quitado del rol correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnNuevoRol_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = Microsoft.VisualBasic.Interaction.InputBox(
                    "Ingresá el nombre del nuevo rol:", "Nuevo Rol", "");
                if (string.IsNullOrWhiteSpace(nombre)) return;

                oBLLRol.Guardar(new BERol(0, nombre.Trim()));
                CargarRoles();
                MessageBox.Show("Rol creado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnNuevoPermiso_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = Microsoft.VisualBasic.Interaction.InputBox(
                    "Ingresá el nombre del nuevo permiso\n(debe coincidir exactamente con el ítem de menú):",
                    "Nuevo Permiso", "");
                if (string.IsNullOrWhiteSpace(nombre)) return;

                oBLLPermiso.Guardar(new BEPermiso(0, nombre.Trim()));
                CargarPermisos();
                MessageBox.Show("Permiso creado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        #endregion
    }
}
