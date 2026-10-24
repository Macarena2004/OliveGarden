using Abstraccion;
using Backup;
using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace MPP
{
    public class MPPBitacora : IGestor<BEBitacora>
    {
            readonly string ruta = GestorCarpeta.UbicacionBitacora("Bitacora.Xml");
            XDocument BDXML;

            #region "Métodos"

            public bool CrearXML()
            {
                try
                {
                    if (!File.Exists(ruta))
                    {
                        BDXML = new XDocument(new XElement("Root",
                            new XElement("Bitacoras")));
                        BDXML.Save(ruta);
                        return true;
                    }
                    else
                    {
                        BDXML = XDocument.Load(ruta);
                        if (BDXML != null)
                        {
                            XElement bitacoras = BDXML.Root.Element("Bitacoras");
                            if (bitacoras != null) { return true; }
                            else
                            {
                                BDXML.Root.Add(new XElement("Bitacoras"));
                                BDXML.Save(ruta);
                                return true;
                            }
                        }
                        else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
                    }
                }
                catch (XmlException ex) { throw ex; }
                catch (Exception ex) { throw ex; }
            }

            public bool Guardar(BEBitacora oBEBitacora)
            {
                try
                {
                    if (CrearXML())
                    {
                        BDXML = XDocument.Load(ruta);
                        if (BDXML != null)
                        {
                            if (oBEBitacora != null && oBEBitacora.Id == 0)
                            {
                                if (!VerificarExistenciaObjeto(oBEBitacora))
                                {
                                    long pId = ObtenerUltimoId() + 1;
                                    oBEBitacora.Id = pId;
                                    BDXML.Root.Element("Bitacoras").Add(new XElement("bitacora",
                                        new XAttribute("Id", oBEBitacora.Id.ToString().Trim()),
                                        new XElement("Detalle", oBEBitacora.Detalle.Trim()),
                                        new XElement("Fecha_Registro", oBEBitacora.FechaRegistro.ToString("dd/MM/yyyy HH:mm:ss").Trim()),
                                        new XElement("usuario",
                                            new XAttribute("Id", oBEBitacora.oBEUsuario.Id.ToString().Trim()))));
                                    BDXML.Save(ruta);
                                    return true;
                                }
                                else { throw new Exception("Error: Ya existe un registro de bitácora con esa fecha!"); }
                            }
                            else { throw new Exception("Error: No se pudo obtener la información de la Bitácora!"); }
                        }
                        else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                    }
                    else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
                }
                catch (XmlException ex) { throw ex; }
                catch (Exception ex) { throw ex; }
            }

            public List<BEBitacora> ListarTodo()
            {
                try
                {
                    if (CrearXML())
                    {
                        BDXML = XDocument.Load(ruta);
                        if (BDXML != null)
                        {
                            var lista = from b in BDXML.Root.Element("Bitacoras").Descendants("bitacora")
                                        select new BEBitacora
                                        {
                                            Id = long.Parse(b.Attribute("Id").Value.Trim()),
                                            FechaRegistro = DateTime.Parse(b.Element("Fecha_Registro").Value.Trim()),
                                            Detalle = b.Element("Detalle").Value.Trim(),
                                            oBEUsuario = new BEUsuario
                                            {
                                                Id = long.Parse(b.Element("usuario").Attribute("Id").Value.Trim())
                                            }
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

            public List<BEBitacora> ListarTodoPorTipo(bool pTipo)
            {
                try
                {
                    if (CrearXML())
                    {
                        BDXML = XDocument.Load(ruta);
                        if (BDXML != null)
                        {
                            string tipo = pTipo ? "backup" : "restore";
                            var lista = from b in BDXML.Root.Element("Bitacoras").Descendants("bitacora")
                                        where b.Element("Detalle").Value.Trim() == tipo
                                        select new BEBitacora
                                        {
                                            Id = long.Parse(b.Attribute("Id").Value.Trim()),
                                            FechaRegistro = DateTime.Parse(b.Element("Fecha_Registro").Value.Trim()),
                                            Detalle = b.Element("Detalle").Value.Trim(),
                                            oBEUsuario = new BEUsuario
                                            {
                                                Id = long.Parse(b.Element("usuario").Attribute("Id").Value.Trim())
                                            }
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

            public bool Eliminar(BEBitacora objeto) { throw new NotImplementedException(); }
            public BEBitacora ListarObjeto(BEBitacora objeto) { throw new NotImplementedException(); }

            public long ObtenerUltimoId()
            {
                try
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        var ids = from b in BDXML.Root.Element("Bitacoras").Descendants("bitacora")
                                  select long.Parse(b.Attribute("Id").Value.Trim());
                        return ids.Any() ? ids.Max() : 0;
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                catch (XmlException ex) { throw ex; }
                catch (Exception ex) { throw ex; }
            }

            public bool VerificarExistenciaObjeto(BEBitacora oBEBitacora)
            {
                try
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        var buscar = from b in BDXML.Root.Element("Bitacoras").Descendants("bitacora")
                                     where b.Element("Fecha_Registro").Value.Trim() == oBEBitacora.FechaRegistro.ToString("dd/MM/yyyy HH:mm:ss").Trim()
                                     select b;
                        return buscar.Any();
                    }
                    else { throw new XmlException("Error: No se pudo recuperar la información del XML!"); }
                }
                catch (XmlException ex) { throw ex; }
                catch (Exception ex) { throw ex; }
            }

            #endregion
        
    }
}
