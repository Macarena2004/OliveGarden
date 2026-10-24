using Abstraccion;
using Backup;
using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace MPP
{
    public class MPPMesa : IGestor<BEMesa>
    {
        readonly string ruta = GestorCarpeta.UbicacionBD("BD.Xml");
        XDocument BDXML;

        #region "Métodos"

        public bool CrearXML()
        {
            try
            {
                BDXML = XDocument.Load(ruta);
                if (BDXML != null)
                {
                    if (BDXML.Root.Element("Mesas") == null)
                    {
                        BDXML.Root.Add(new XElement("Mesas"));
                        BDXML.Save(ruta);
                    }
                    return true;
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Guardar(BEMesa oBEMesa)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEMesa != null)
                    {
                        if (oBEMesa.Id == 0)
                        {
                            if (!VerificarExistenciaObjeto(oBEMesa))
                            {
                                long pId = ObtenerUltimoId() + 1;
                                oBEMesa.Id = pId;
                                BDXML.Root.Element("Mesas").Add(new XElement("mesa",
                                    new XAttribute("Id", oBEMesa.Id.ToString().Trim()),
                                    new XElement("NumeroMesa", oBEMesa.NumeroMesa.ToString().Trim()),
                                    new XElement("Capacidad", oBEMesa.Capacidad.ToString().Trim()),
                                    new XElement("EstadoMesa", oBEMesa.EstadoMesa.Trim())));
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: La Mesa ya existe!"); }
                        }
                        else
                        {
                            var buscar = from m in BDXML.Root.Element("Mesas").Descendants("mesa")
                                         where m.Attribute("Id").Value.Trim() == oBEMesa.Id.ToString().Trim()
                                         select m;
                            if (buscar.Any())
                            {
                                var m = buscar.First();
                                m.Element("NumeroMesa").Value = oBEMesa.NumeroMesa.ToString().Trim();
                                m.Element("Capacidad").Value = oBEMesa.Capacidad.ToString().Trim();
                                m.Element("EstadoMesa").Value = oBEMesa.EstadoMesa.Trim();
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: No se encontró la Mesa!"); }
                        }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Eliminar(BEMesa oBEMesa)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var buscar = from m in BDXML.Root.Element("Mesas").Descendants("mesa")
                                 where m.Attribute("Id").Value.Trim() == oBEMesa.Id.ToString().Trim()
                                 select m;
                    if (buscar.Any())
                    {
                        buscar.First().Remove();
                        BDXML.Save(ruta);
                        return true;
                    }
                    else { throw new Exception("Error: No se encontró la Mesa!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public BEMesa ListarObjeto(BEMesa oBEMesa)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var buscar = from m in BDXML.Root.Element("Mesas").Descendants("mesa")
                                 where m.Attribute("Id").Value.Trim() == oBEMesa.Id.ToString().Trim()
                                 select m;
                    if (buscar.Any())
                    {
                        var m = buscar.First();
                        return new BEMesa
                        {
                            Id = long.Parse(m.Attribute("Id").Value.Trim()),
                            NumeroMesa = int.Parse(m.Element("NumeroMesa").Value.Trim()),
                            Capacidad = int.Parse(m.Element("Capacidad").Value.Trim()),
                            EstadoMesa = m.Element("EstadoMesa").Value.Trim()
                        };
                    }
                    else { throw new Exception("Error: No se encontró la Mesa!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEMesa> ListarTodo()
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var lista = from m in BDXML.Root.Element("Mesas").Descendants("mesa")
                                select new BEMesa
                                {
                                    Id = long.Parse(m.Attribute("Id").Value.Trim()),
                                    NumeroMesa = int.Parse(m.Element("NumeroMesa").Value.Trim()),
                                    Capacidad = int.Parse(m.Element("Capacidad").Value.Trim()),
                                    EstadoMesa = m.Element("EstadoMesa").Value.Trim()
                                };
                    return lista.ToList();
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEMesa> ListarMesasDisponibles()
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var lista = from m in BDXML.Root.Element("Mesas").Descendants("mesa")
                                where m.Element("EstadoMesa").Value.Trim() == "Disponible"
                                select new BEMesa
                                {
                                    Id = long.Parse(m.Attribute("Id").Value.Trim()),
                                    NumeroMesa = int.Parse(m.Element("NumeroMesa").Value.Trim()),
                                    Capacidad = int.Parse(m.Element("Capacidad").Value.Trim()),
                                    EstadoMesa = m.Element("EstadoMesa").Value.Trim()
                                };
                    return lista.ToList();
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool ActualizarEstado(BEMesa oBEMesa)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var buscar = from m in BDXML.Root.Element("Mesas").Descendants("mesa")
                                 where m.Attribute("Id").Value.Trim() == oBEMesa.Id.ToString().Trim()
                                 select m;
                    if (buscar.Any())
                    {
                        buscar.First().Element("EstadoMesa").Value = oBEMesa.EstadoMesa.Trim();
                        BDXML.Save(ruta);
                        return true;
                    }
                    else { throw new Exception("Error: No se encontró la Mesa!"); }
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
                var ids = from m in BDXML.Root.Element("Mesas").Descendants("mesa")
                          select long.Parse(m.Attribute("Id").Value.Trim());
                return ids.Any() ? ids.Max() : 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool VerificarExistenciaObjeto(BEMesa oBEMesa)
        {
            try
            {
                BDXML = XDocument.Load(ruta);
                var buscar = from m in BDXML.Root.Element("Mesas").Descendants("mesa")
                             where m.Element("NumeroMesa").Value.Trim() == oBEMesa.NumeroMesa.ToString().Trim()
                             select m;
                return buscar.Any();
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}
