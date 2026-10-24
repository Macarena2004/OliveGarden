using Abstraccion;
using Backup;
using BE;
using BE.BEComposite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace MPP.MPPComposite
{
    public class MPPRol : IGestor<BERol>
    {
        readonly string ruta = GestorCarpeta.UbicacionBD("BD.Xml");
        XDocument BDXML;

        #region "Métodos Rol"

        public bool CrearXML()
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    BDXML = new XDocument(new XElement("Root", new XElement("Roles")));
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        if (BDXML.Root.Element("Roles") == null)
                        {
                            BDXML.Root.Add(new XElement("Roles"));
                            BDXML.Save(ruta);
                        }
                        return true;
                    }
                    else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
                }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool CrearXMLRolPermiso()
        {
            try
            {
                if (!File.Exists(ruta)) { return CrearXML(); }
                BDXML = XDocument.Load(ruta);
                if (BDXML != null)
                {
                    if (BDXML.Root.Element("Rol_Permisos") == null)
                    {
                        BDXML.Root.Add(new XElement("Rol_Permisos"));
                        BDXML.Save(ruta);
                    }
                    return true;
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Guardar(BERol oBERol)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBERol != null)
                    {
                        if (oBERol.Id == 0)
                        {
                            if (!VerificarExistenciaObjeto(oBERol))
                            {
                                long pId = ObtenerUltimoId() + 1;
                                oBERol.Id = pId;
                                BDXML.Root.Element("Roles").Add(new XElement("rol",
                                    new XAttribute("Id", oBERol.Id.ToString().Trim()),
                                    new XElement("Nombre", oBERol.Nombre.Trim())));
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: El Rol ya existe!"); }
                        }
                        else
                        {
                            if (!VerificarExistenciaObjeto(oBERol))
                            {
                                var buscar = from r in BDXML.Root.Element("Roles").Descendants("rol")
                                             where r.Attribute("Id").Value.Trim() == oBERol.Id.ToString().Trim()
                                             select r;
                                if (buscar.Any())
                                {
                                    buscar.First().Element("Nombre").Value = oBERol.Nombre.Trim();
                                    BDXML.Save(ruta);
                                    return true;
                                }
                                else { throw new Exception("Error: No se encontró el Rol!"); }
                            }
                            else { throw new Exception("Error: El nombre del Rol ya existe!"); }
                        }
                    }
                    else { throw new Exception("Error: No se pudo obtener los datos del Rol!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Eliminar(BERol oBERol)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBERol != null)
                    {
                        var buscar = from r in BDXML.Root.Element("Roles").Descendants("rol")
                                     where r.Attribute("Id").Value.Trim() == oBERol.Id.ToString().Trim()
                                     select r;
                        if (buscar.Any())
                        {
                            var enUsuario = from ur in BDXML.Root.Element("Usuario_Roles").Descendants("usuario_rol")
                                            where ur.Element("Id_Rol_Padre").Value.Trim() == oBERol.Id.ToString().Trim()
                                            select ur;
                            if (!enUsuario.Any())
                            {
                                var enRolPermisos = from rp in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                                    where rp.Element("Id_Rol").Value.Trim() == oBERol.Id.ToString().Trim()
                                                    select rp;
                                enRolPermisos.ToList().ForEach(x => x.Remove());
                                buscar.First().Remove();
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: No se puede eliminar un Rol asociado a un Usuario!"); }
                        }
                        else { throw new Exception("Error: No se encontró el Rol!"); }
                    }
                    else { throw new Exception("Error: No se pudo obtener los datos del Rol!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public BERol ListarObjeto(BERol oBERol)
        {
            try
            {
                if (CrearXML() && CrearXMLRolPermiso())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBERol != null)
                    {
                        var buscar = from r in BDXML.Root.Element("Roles").Descendants("rol")
                                     where r.Element("Nombre").Value.Trim() == oBERol.Nombre.Trim()
                                     select r;
                        if (buscar.Any())
                        {
                            var encontrado = buscar.First();
                            BERol rol = new BERol(
                                long.Parse(encontrado.Attribute("Id").Value),
                                encontrado.Element("Nombre").Value.Trim());

                            // Cargar permisos del rol
                            var permisosRol = from rp in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                              where rp.Element("Id_Rol").Value.Trim() == rol.Id.ToString().Trim()
                                              select rp;
                            foreach (var rp in permisosRol)
                            {
                                var pNombre = (from p in BDXML.Root.Element("Permisos").Descendants("permiso")
                                               where p.Attribute("Id").Value.Trim() == rp.Element("Id_Permiso").Value.Trim()
                                               select p.Element("Nombre").Value).FirstOrDefault();
                                if (pNombre != null)
                                    rol.listaPermisos.Add(new BEPermiso(long.Parse(rp.Element("Id_Permiso").Value), pNombre));
                            }
                            return rol;
                        }
                        else { throw new Exception("Error: No se encontró el Rol!"); }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BERol> ListarTodo()
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        var lista = from r in BDXML.Root.Element("Roles").Elements("rol")
                                    select new BERol(
                                        long.Parse(r.Attribute("Id").Value),
                                        r.Element("Nombre").Value);
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
                var ids = from r in BDXML.Root.Element("Roles").Descendants("rol")
                          select long.Parse(r.Attribute("Id").Value);
                return ids.Any() ? ids.Max() : 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool VerificarExistenciaObjeto(BERol oBERol)
        {
            try
            {
                BDXML = XDocument.Load(ruta);
                var buscar = from r in BDXML.Root.Element("Roles").Descendants("rol")
                             where r.Element("Nombre").Value.Trim() == oBERol.Nombre.Trim()
                             select r;
                return buscar.Any();
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region "Métodos Rol-Permiso"

        public bool AsociarRolAPermiso(BERol oBERol, BEPermiso oBEPermiso)
        {
            try
            {
                if (CrearXMLRolPermiso())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBERol != null && oBEPermiso != null)
                    {
                        if (!VerificarExistenciaPermisoEnRol(oBERol, oBEPermiso))
                        {
                            var ids = from rp in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                      select long.Parse(rp.Attribute("Id").Value);
                            long pId = ids.Any() ? ids.Max() + 1 : 1;
                            BDXML.Root.Element("Rol_Permisos").Add(new XElement("rol_permiso",
                                new XAttribute("Id", pId),
                                new XElement("Id_Rol", oBERol.Id.ToString().Trim()),
                                new XElement("Id_Permiso", oBEPermiso.Id.ToString().Trim())));
                            BDXML.Save(ruta);
                            return true;
                        }
                        else { throw new Exception("Error: El Permiso ya está asociado al Rol!"); }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool DesasociarRolAPermiso(BERol oBERol, BEPermiso oBEPermiso)
        {
            try
            {
                if (CrearXMLRolPermiso())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBERol != null && oBEPermiso != null)
                    {
                        var buscar = from rp in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                     where rp.Element("Id_Rol").Value.Trim() == oBERol.Id.ToString().Trim()
                                     && rp.Element("Id_Permiso").Value.Trim() == oBEPermiso.Id.ToString().Trim()
                                     select rp;
                        if (buscar.Any())
                        {
                            buscar.First().Remove();
                            BDXML.Save(ruta);
                            return true;
                        }
                        else { throw new Exception("Error: No se encontró la asociación Rol-Permiso!"); }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool VerificarExistenciaPermisoEnRol(BERol oBERol, BEPermiso oBEPermiso)
        {
            try
            {
                BDXML = XDocument.Load(ruta);
                var buscar = from rp in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                             where rp.Element("Id_Rol").Value.Trim() == oBERol.Id.ToString().Trim()
                             && rp.Element("Id_Permiso").Value.Trim() == oBEPermiso.Id.ToString().Trim()
                             select rp;
                return buscar.Any();
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}
