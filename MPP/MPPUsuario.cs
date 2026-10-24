using Abstraccion;
using Backup;
using BE;
using BE.BEComposite;
using Seguridad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;

namespace MPP
{
    public class MPPUsuario : IGestor<BEUsuario>
    {
        readonly string ruta = GestorCarpeta.UbicacionBD("BD.Xml");
        XDocument BDXML;

        #region "Métodos XML"

        public bool CrearXML()
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    BDXML = new XDocument(new XElement("Root",
                        new XElement("Usuarios"),
                        new XElement("Permisos"),
                        new XElement("Roles"),
                        new XElement("Rol_Permisos"),
                        new XElement("Usuario_Roles"),
                        new XElement("Usuario_Permisos"),
                        new XElement("Clientes"),
                        new XElement("Empleados"),
                        new XElement("Mesas"),
                        new XElement("Reservas"),
                        new XElement("Productos"),
                        new XElement("Pedidos"),
                        new XElement("Facturas"),
                        new XElement("Pagos"),
                        new XElement("OrdenesDeCompra")));
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        // Verificar y crear todos los elementos raíz necesarios
                        string[] elementos = {
                            "Usuarios","Permisos","Roles","Rol_Permisos",
                            "Usuario_Roles","Usuario_Permisos","Clientes","Empleados",
                            "Mesas","Reservas","Productos","Pedidos",
                            "Facturas","Pagos","OrdenesDeCompra"
                        };
                        foreach (var elem in elementos)
                        {
                            if (BDXML.Root.Element(elem) == null)
                            {
                                BDXML.Root.Add(new XElement(elem));
                            }
                        }
                        BDXML.Save(ruta);
                        return true;
                    }
                    else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region "Métodos Usuario"

        public bool Guardar(BEUsuario oBEUsuario)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEUsuario != null)
                    {
                        if (oBEUsuario.Id == 0)
                        {
                            if (!VerificarExistenciaObjeto(oBEUsuario))
                            {
                                long pId = ObtenerUltimoId() + 1;
                                oBEUsuario.Id = pId;
                                string passHasheado = HashearPassword(oBEUsuario.Password);
                                BDXML.Root.Element("Usuarios").Add(new XElement("usuario",
                                    new XAttribute("Id", oBEUsuario.Id.ToString().Trim()),
                                    new XElement("Usuario", oBEUsuario.Usuario.Trim()),
                                    new XElement("Password", passHasheado),
                                    new XElement("Activo", oBEUsuario.Activo.ToString().Trim()),
                                    new XElement("Bloqueado", oBEUsuario.Bloqueado.ToString().Trim())));
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: El Usuario ya existe!"); }
                        }
                        else
                        {
                            var buscar = from u in BDXML.Root.Element("Usuarios").Descendants("usuario")
                                         where u.Attribute("Id").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                         select u;
                            if (buscar.Any())
                            {
                                var u = buscar.First();
                                // Solo se vuelve a hashear si la contraseña cambió. Si llega el valor ya guardado
                                // (por ejemplo, al bloquear un usuario), se conserva tal cual para no dañarlo.
                                string passGuardada = u.Element("Password").Value.Trim();
                                if (!string.Equals((oBEUsuario.Password ?? "").Trim(), passGuardada, StringComparison.Ordinal))
                                {
                                    u.Element("Password").Value = HashearPassword(oBEUsuario.Password);
                                }
                                u.Element("Activo").Value = oBEUsuario.Activo.ToString().Trim();
                                u.Element("Bloqueado").Value = oBEUsuario.Bloqueado.ToString().Trim();
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: No se encontró el Usuario!"); }
                        }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (CryptographicException ex) { throw ex; }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Eliminar(BEUsuario oBEUsuario)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEUsuario != null)
                    {
                        var buscar = from u in BDXML.Root.Element("Usuarios").Descendants("usuario")
                                     where u.Attribute("Id").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                     select u;
                        if (buscar.Any())
                        {
                            var enRoles = from ur in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                          where ur.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                          select ur;
                            var enPermisos = from up in BDXML.Root.Element("Usuario_Permisos").Descendants("usuario_permiso")
                                             where up.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                             select up;
                            if (!enRoles.Any() && !enPermisos.Any())
                            {
                                buscar.First().Remove();
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: No se puede eliminar un Usuario con Roles o Permisos asignados!"); }
                        }
                        else { throw new Exception("Error: No se encontró el Usuario!"); }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public BEUsuario ListarObjeto(BEUsuario oBEUsuario)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEUsuario != null)
                    {
                        var buscar = from u in BDXML.Root.Element("Usuarios").Descendants("usuario")
                                     where u.Element("Usuario").Value.Trim() == oBEUsuario.Usuario.Trim()
                                     select u;
                        if (buscar.Any())
                        {
                            var encontrado = buscar.First();
                            BEUsuario usuario = new BEUsuario
                            {
                                Id = long.Parse(encontrado.Attribute("Id").Value.Trim()),
                                Usuario = encontrado.Element("Usuario").Value.Trim(),
                                Password = encontrado.Element("Password").Value.Trim(),
                                Activo = bool.Parse(encontrado.Element("Activo").Value.Trim()),
                                Bloqueado = bool.Parse(encontrado.Element("Bloqueado").Value.Trim())
                            };
                            // Cargar roles
                            CargarRolesDeUsuario(usuario);
                            // Cargar permisos directos
                            CargarPermisosDeUsuario(usuario);
                            return usuario;
                        }
                        else { throw new Exception("Error: No se encontró el Usuario!"); }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public BEUsuario ListarObjetoPorId(BEUsuario oBEUsuario)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEUsuario != null)
                    {
                        var buscar = from u in BDXML.Root.Element("Usuarios").Descendants("usuario")
                                     where u.Attribute("Id").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                     select u;
                        if (buscar.Any())
                        {
                            var encontrado = buscar.First();
                            return new BEUsuario
                            {
                                Id = long.Parse(encontrado.Attribute("Id").Value.Trim()),
                                Usuario = encontrado.Element("Usuario").Value.Trim(),
                                Password = encontrado.Element("Password").Value.Trim(),
                                Activo = bool.Parse(encontrado.Element("Activo").Value.Trim()),
                                Bloqueado = bool.Parse(encontrado.Element("Bloqueado").Value.Trim())
                            };
                        }
                        else { throw new Exception("Error: No se encontró el Usuario!"); }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEUsuario> ListarTodo()
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        var lista = from u in BDXML.Root.Element("Usuarios").Elements("usuario")
                                    select new BEUsuario
                                    {
                                        Id = long.Parse(u.Attribute("Id").Value),
                                        Usuario = u.Element("Usuario").Value,
                                        Password = u.Element("Password").Value,
                                        Activo = bool.Parse(u.Element("Activo").Value),
                                        Bloqueado = bool.Parse(u.Element("Bloqueado").Value)
                                    };
                        return lista.ToList();
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                BDXML = XDocument.Load(ruta);
                var ids = from u in BDXML.Root.Element("Usuarios").Descendants("usuario")
                          select long.Parse(u.Attribute("Id").Value.Trim());
                return ids.Any() ? ids.Max() : 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool VerificarExistenciaObjeto(BEUsuario oBEUsuario)
        {
            try
            {
                BDXML = XDocument.Load(ruta);
                var buscar = from u in BDXML.Root.Element("Usuarios").Descendants("usuario")
                             where u.Element("Usuario").Value.Trim() == oBEUsuario.Usuario.Trim()
                             select u;
                return buscar.Any();
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region "Login y Contraseñas"

        public bool Login(BEUsuario oBEUsuario)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        string passIngresada = (oBEUsuario.Password ?? "").Trim();
                        var buscar = from u in BDXML.Root.Element("Usuarios").Descendants("usuario")
                                     where u.Element("Usuario").Value.Trim() == oBEUsuario.Usuario.Trim()
                                     select u;
                        var encontrado = buscar.FirstOrDefault();
                        if (encontrado != null && VerificarPassword(passIngresada, encontrado.Element("Password").Value.Trim()))
                        {
                            if (bool.Parse(encontrado.Element("Activo").Value))
                            {
                                if (!bool.Parse(encontrado.Element("Bloqueado").Value))
                                {
                                    // Migración transparente: si estaba en el formato viejo, se guarda ya hasheada.
                                    if (NecesitaRehash(encontrado.Element("Password").Value.Trim()))
                                    {
                                        encontrado.Element("Password").Value = HashearPassword(passIngresada);
                                        BDXML.Save(ruta);
                                    }
                                    return true;
                                }
                                else { throw new Exception("Error: El Usuario está Bloqueado!"); }
                            }
                            else { throw new Exception("Error: El Usuario no está Activo!"); }
                        }
                        else { throw new Exception("Error: Usuario y/o Contraseña incorrectos!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public string HashearPassword(string pPassword)
        {
            if (!string.IsNullOrEmpty(pPassword)) { return Encriptacion.HashearPassword(pPassword); }
            else { throw new Exception("Error: La contraseña no puede ser vacía!"); }
        }

        public bool VerificarPassword(string pPassword, string pAlmacenado)
        {
            return Encriptacion.VerificarPassword(pPassword, pAlmacenado);
        }

        public bool NecesitaRehash(string pAlmacenado)
        {
            return Encriptacion.NecesitaRehash(pAlmacenado);
        }

        #endregion

        #region "Roles y Permisos del Usuario"

        private void CargarRolesDeUsuario(BEUsuario usuario)
        {
            var rolesUsuario = from ur in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                               where ur.Element("Id_Usuario").Value.Trim() == usuario.Id.ToString().Trim()
                               select ur;
            foreach (var ur in rolesUsuario)
            {
                string rolId = ur.Element("Id_Rol_Padre").Value.Trim();
                var buscarRol = from r in BDXML.Root.Element("Roles").Descendants("rol")
                                where r.Attribute("Id").Value.Trim() == rolId
                                select r;
                if (buscarRol.Any())
                {
                    var r = buscarRol.First();
                    usuario.listaRoles.Add(new BERol(long.Parse(r.Attribute("Id").Value), r.Element("Nombre").Value.Trim()));
                }
            }
        }

        private void CargarPermisosDeUsuario(BEUsuario usuario)
        {
            var permisosUsuario = from up in BDXML.Root.Element("Usuario_Permisos").Descendants("usuario_permiso")
                                  where up.Element("Id_Usuario").Value.Trim() == usuario.Id.ToString().Trim()
                                  select up;
            foreach (var up in permisosUsuario)
            {
                string permisoId = up.Element("Id_Permiso").Value.Trim();
                var buscarPermiso = from p in BDXML.Root.Element("Permisos").Descendants("permiso")
                                    where p.Attribute("Id").Value.Trim() == permisoId
                                    select p;
                if (buscarPermiso.Any())
                {
                    var p = buscarPermiso.First();
                    usuario.listaPermisos.Add(new BEPermiso(long.Parse(p.Attribute("Id").Value), p.Element("Nombre").Value.Trim()));
                }
            }
        }

        public List<BEPermiso> ListarTodosLosPermisosDelUsuario(BEUsuario oBEUsuario)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    List<BEPermiso> listaPermisos = new List<BEPermiso>();

                    // Permisos directos del usuario
                    var permisosDirectos = from up in BDXML.Root.Element("Usuario_Permisos").Descendants("usuario_permiso")
                                           where up.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                           select up;
                    foreach (var up in permisosDirectos)
                    {
                        var pNombre = (from p in BDXML.Root.Element("Permisos").Descendants("permiso")
                                       where p.Attribute("Id").Value.Trim() == up.Element("Id_Permiso").Value.Trim()
                                       select p.Element("Nombre").Value).FirstOrDefault();
                        if (pNombre != null)
                            listaPermisos.Add(new BEPermiso(long.Parse(up.Element("Id_Permiso").Value), pNombre));
                    }

                    // Permisos heredados de los roles
                    var rolesUsuario = from ur in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                       where ur.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                       select ur;
                    foreach (var ur in rolesUsuario)
                    {
                        var permisosRol = from rp in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                          where rp.Element("Id_Rol").Value.Trim() == ur.Element("Id_Rol_Padre").Value.Trim()
                                          select rp;
                        foreach (var rp in permisosRol)
                        {
                            var pNombre = (from p in BDXML.Root.Element("Permisos").Descendants("permiso")
                                           where p.Attribute("Id").Value.Trim() == rp.Element("Id_Permiso").Value.Trim()
                                           select p.Element("Nombre").Value).FirstOrDefault();
                            if (pNombre != null && !listaPermisos.Any(x => x.Nombre == pNombre))
                                listaPermisos.Add(new BEPermiso(long.Parse(rp.Element("Id_Permiso").Value), pNombre));
                        }
                    }
                    return listaPermisos;
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool AsociarUsuarioARol(BEUsuario oBEUsuario, BERol oBERol)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (!VerificarUsuarioRol(oBEUsuario, oBERol))
                    {
                        BDXML.Root.Element("Usuario_Roles").Add(new XElement("usuario_rol",
                            new XElement("Id_Usuario", oBEUsuario.Id.ToString().Trim()),
                            new XElement("Id_Rol_Padre", oBERol.Id.ToString().Trim())));
                        BDXML.Save(ruta);
                        return true;
                    }
                    else { throw new Exception("Error: El Usuario ya tiene asociado ese Rol!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool DesasociarUsuarioARol(BEUsuario oBEUsuario, BERol oBERol)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var buscar = from ur in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                 where ur.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                 && ur.Element("Id_Rol_Padre").Value.Trim() == oBERol.Id.ToString().Trim()
                                 select ur;
                    if (buscar.Any())
                    {
                        buscar.First().Remove();
                        BDXML.Save(ruta);
                        return true;
                    }
                    else { throw new Exception("Error: No se encontró la asociación Usuario-Rol!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool AsociarPermisoAUsuario(BEUsuario oBEUsuario, BEPermiso oBEPermiso)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (!VerificarUsuarioPermiso(oBEUsuario, oBEPermiso))
                    {
                        BDXML.Root.Element("Usuario_Permisos").Add(new XElement("usuario_permiso",
                            new XElement("Id_Usuario", oBEUsuario.Id.ToString().Trim()),
                            new XElement("Id_Permiso", oBEPermiso.Id.ToString().Trim())));
                        BDXML.Save(ruta);
                        return true;
                    }
                    else { throw new Exception("Error: El Permiso ya está asociado al Usuario!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool DesasociarPermisoAUsuario(BEUsuario oBEUsuario, BEPermiso oBEPermiso)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var buscar = from up in BDXML.Root.Element("Usuario_Permisos").Descendants("usuario_permiso")
                                 where up.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                                 && up.Element("Id_Permiso").Value.Trim() == oBEPermiso.Id.ToString().Trim()
                                 select up;
                    if (buscar.Any())
                    {
                        buscar.First().Remove();
                        BDXML.Save(ruta);
                        return true;
                    }
                    else { throw new Exception("Error: No se encontró la asociación Usuario-Permiso!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool VerificarUsuarioRol(BEUsuario oBEUsuario, BERol oBERol)
        {
            BDXML = XDocument.Load(ruta);
            var buscar = from ur in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                         where ur.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                         && ur.Element("Id_Rol_Padre").Value.Trim() == oBERol.Id.ToString().Trim()
                         select ur;
            return buscar.Any();
        }

        public bool VerificarUsuarioPermiso(BEUsuario oBEUsuario, BEPermiso oBEPermiso)
        {
            BDXML = XDocument.Load(ruta);
            var buscar = from up in BDXML.Root.Element("Usuario_Permisos").Descendants("usuario_permiso")
                         where up.Element("Id_Usuario").Value.Trim() == oBEUsuario.Id.ToString().Trim()
                         && up.Element("Id_Permiso").Value.Trim() == oBEPermiso.Id.ToString().Trim()
                         select up;
            return buscar.Any();
        }

        #endregion
    }
}
