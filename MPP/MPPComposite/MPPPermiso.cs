
using Backup;
using Abstraccion;
using BE.BEComposite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace MPP.MPPComposite
{
    public class MPPPermiso : IGestor<BEPermiso>
    {
        readonly string ruta = GestorCarpeta.UbicacionBD("BD.Xml");
        XDocument BDXML;

        #region "Métodos"

        public bool CrearXML()
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    BDXML = new XDocument(new XElement("Root", new XElement("Permisos")));
                    BDXML.Save(ruta);
                    return true;
                }
                else
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        if (BDXML.Root.Element("Permisos") == null)
                        {
                            BDXML.Root.Add(new XElement("Permisos"));
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

        public bool Guardar(BEPermiso oBEPermiso)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEPermiso != null)
                    {
                        if (oBEPermiso.Id == 0)
                        {
                            if (!VerificarExistenciaObjeto(oBEPermiso))
                            {
                                long pId = ObtenerUltimoId() + 1;
                                oBEPermiso.Id = pId;
                                BDXML.Root.Element("Permisos").Add(new XElement("permiso",
                                    new XAttribute("Id", oBEPermiso.Id.ToString().Trim()),
                                    new XElement("Nombre", oBEPermiso.Nombre.Trim())));
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: El Permiso ya existe!"); }
                        }
                        else
                        {
                            if (!VerificarExistenciaObjeto(oBEPermiso))
                            {
                                var buscar = from p in BDXML.Root.Element("Permisos").Descendants("permiso")
                                             where p.Attribute("Id").Value.Trim() == oBEPermiso.Id.ToString().Trim()
                                             select p;
                                if (buscar.Any())
                                {
                                    buscar.First().Element("Nombre").Value = oBEPermiso.Nombre.Trim();
                                    BDXML.Save(ruta);
                                    return true;
                                }
                                else { throw new Exception("Error: No se encontró el Permiso a modificar!"); }
                            }
                            else { throw new Exception("Error: El nombre del Permiso ya existe!"); }
                        }
                    }
                    else { throw new Exception("Error: No se pudo obtener los datos del Permiso!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Eliminar(BEPermiso oBEPermiso)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEPermiso != null)
                    {
                        var buscar = from p in BDXML.Root.Element("Permisos").Descendants("permiso")
                                     where p.Attribute("Id").Value.Trim() == oBEPermiso.Id.ToString().Trim()
                                     select p;
                        if (buscar.Any())
                        {
                            // Verificar que no esté asociado a ningún usuario
                            var enUsuario = from up in BDXML.Root.Element("Usuario_Permisos").Descendants("usuario_permiso")
                                            where up.Element("Id_Permiso").Value.Trim() == oBEPermiso.Id.ToString().Trim()
                                            select up;
                            if (!enUsuario.Any())
                            {
                                // Eliminar de Rol_Permisos también
                                var enRol = from rp in BDXML.Root.Element("Rol_Permisos").Descendants("rol_permiso")
                                            where rp.Element("Id_Permiso").Value.Trim() == oBEPermiso.Id.ToString().Trim()
                                            select rp;
                                enRol.ToList().ForEach(x => x.Remove());
                                buscar.First().Remove();
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: No se puede eliminar un Permiso asociado a un Usuario!"); }
                        }
                        else { throw new Exception("Error: No se encontró el Permiso!"); }
                    }
                    else { throw new Exception("Error: No se pudo obtener los datos del Permiso!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public BEPermiso ListarObjeto(BEPermiso oBEPermiso)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEPermiso != null)
                    {
                        var buscar = from p in BDXML.Root.Element("Permisos").Descendants("permiso")
                                     where p.Element("Nombre").Value.Trim() == oBEPermiso.Nombre.Trim()
                                     select p;
                        if (buscar.Any())
                        {
                            var encontrado = buscar.First();
                            return new BEPermiso(
                                long.Parse(encontrado.Attribute("Id").Value),
                                encontrado.Element("Nombre").Value.Trim());
                        }
                        else { throw new Exception("Error: No se encontró el Permiso!"); }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEPermiso> ListarTodo()
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        var lista = from p in BDXML.Root.Element("Permisos").Descendants("permiso")
                                    select new BEPermiso(
                                        long.Parse(p.Attribute("Id").Value),
                                        p.Element("Nombre").Value);
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
                var ids = from p in BDXML.Root.Element("Permisos").Descendants("permiso")
                          select long.Parse(p.Attribute("Id").Value);
                return ids.Any() ? ids.Max() : 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool VerificarExistenciaObjeto(BEPermiso oBEPermiso)
        {
            try
            {
                BDXML = XDocument.Load(ruta);
                var buscar = from p in BDXML.Root.Element("Permisos").Descendants("permiso")
                             where p.Element("Nombre").Value.Trim() == oBEPermiso.Nombre.Trim()
                             select p;
                return buscar.Any();
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}
