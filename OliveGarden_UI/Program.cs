using BE;
using BLL_Negocio;
using System;
using System.Windows.Forms;

namespace OliveGarden_UI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicializar el XML si no existe
            //try { new BLLUsuario().CrearXML(); } catch { }

            try
            {
                // Borrar el XML viejo para empezar limpio
                string rutaBD = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "BaseDatos", "BD.Xml");
                if (System.IO.File.Exists(rutaBD))
                    System.IO.File.Delete(rutaBD);

                BLLUsuario bll = new BLLUsuario();
                bll.CrearXML();
                MessageBox.Show("XML creado OK");

                // Crear usuario admin
                BEUsuario admin = new BEUsuario(0, "admin", "admin123", true, false);
                bll.Guardar(admin);
                MessageBox.Show("Usuario admin guardado OK");


                // Crear permisos
                var bllPermiso = new BLL_Negocio.BLLComposite.BLLPermiso();
                string[] permisos = {
                   "Reservar Mesa", "Modificar Reserva", "Cancelar Reserva",
                   "Realizar Pedido", "Preparar Pedido",
                   "Generar Factura", "Registrar Pago",
                   "Registrar Producto", "Generar Orden de Compra",
                   "Generar Reporte Financiero",
                   "Gestión de Usuarios", "Roles y Permisos",
                   "Backup", "Restore", "Bitácora"
    };
                foreach (var p in permisos)
                    bllPermiso.Guardar(new BE.BEComposite.BEPermiso(0, p));
                MessageBox.Show("Permisos creados OK");

                // Crear mesas
                BLLMesa oBLLMesa = new BLLMesa();
                for (int i = 1; i <= 6; i++)
                {
                    int capacidad = i <= 2 ? 2 : i <= 4 ? 4 : 6;
                    oBLLMesa.Guardar(new BEMesa(0, i, capacidad, "Disponible"));
                }
                MessageBox.Show("Mesas creadas OK");

                // Crear rol Administrador
                var bllRol = new BLL_Negocio.BLLComposite.BLLRol();
                bllRol.Guardar(new BE.BEComposite.BERol(0, "Administrador"));
                MessageBox.Show("Rol creado OK");

                // Asociar todos los permisos al rol
                BEUsuario adminCompleto = bll.ListarObjeto(new BEUsuario { Usuario = "admin" });
                var rolAdmin = bllRol.ListarObjeto(new BE.BEComposite.BERol(0, "Administrador"));
                foreach (var p in bllPermiso.ListarTodo())
                    bllRol.AsociarRolAPermiso(rolAdmin, p);
                MessageBox.Show("Permisos asociados al rol OK");

                // Asociar rol al usuario
                bll.AsociarUsuarioARol(adminCompleto, rolAdmin);
                MessageBox.Show("Rol asociado al usuario OK — INICIALIZACIÓN COMPLETA");

                // Crear Productos
                try
                {
                    BLLProducto oBLLProducto = new BLLProducto();
                    var productosExistentes = oBLLProducto.ListarTodo();
                    if (productosExistentes.Count == 0)
                    {
                        oBLLProducto.Guardar(new BEProducto(0, "Milanesa Napolitana", "Con salsa, jamón y queso", 2500.00, 20));
                        oBLLProducto.Guardar(new BEProducto(0, "Pasta Carbonara", "Con crema, panceta y huevo", 2200.00, 15));
                        oBLLProducto.Guardar(new BEProducto(0, "Ensalada César", "Con pollo y aderezo", 1800.00, 10));
                        oBLLProducto.Guardar(new BEProducto(0, "Agua mineral", "500ml", 600.00, 50));
                        oBLLProducto.Guardar(new BEProducto(0, "Gaseosa", "Lata 354ml", 800.00, 30));
                        oBLLProducto.Guardar(new BEProducto(0, "Tiramisú", "Postre italiano", 1500.00, 12));
                        MessageBox.Show("Productos creados OK");
                    }
                    else
                    {
                        MessageBox.Show($"Ya existen {productosExistentes.Count} productos");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error productos: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR EN PASO: " + ex.Message + "\n\n" + ex.StackTrace,
                 "Error inicialización", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            IniciarLogin();
        }

        public static void IniciarLogin()
        {
            FormLogin formLogin = new FormLogin();
            if (formLogin.ShowDialog() == DialogResult.OK)
            {
                BEUsuario usuarioLogueado = formLogin.UsuarioLogueado;
                Application.Run(new FormMenu(usuarioLogueado));
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
