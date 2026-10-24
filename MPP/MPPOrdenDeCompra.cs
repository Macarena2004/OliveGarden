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
    public class MPPOrdenDeCompra : IGestor<BEOrdenDeCompra>
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
                    if (BDXML.Root.Element("OrdenesDeCompra") == null)
                    {
                        BDXML.Root.Add(new XElement("OrdenesDeCompra"));
                        BDXML.Save(ruta);
                    }
                    return true;
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Guardar(BEOrdenDeCompra oBEOrden)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEOrden != null)
                    {
                        long pId = ObtenerUltimoId() + 1;
                        oBEOrden.Id = pId;
                        BDXML.Root.Element("OrdenesDeCompra").Add(new XElement("orden",
                            new XAttribute("Id", oBEOrden.Id.ToString().Trim()),
                            new XElement("IdProducto", oBEOrden.IdProducto.ToString().Trim()),
                            new XElement("NombreProducto", oBEOrden.NombreProducto.Trim()),
                            new XElement("CantidadSolicitada", oBEOrden.CantidadSolicitada.ToString().Trim()),
                            new XElement("EstadoOrden", oBEOrden.EstadoOrden.Trim()),
                            new XElement("FechaOrden", oBEOrden.FechaOrden.ToString("dd/MM/yyyy").Trim())));
                        BDXML.Save(ruta);
                        return true;
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Eliminar(BEOrdenDeCompra objeto) { throw new NotImplementedException(); }

        public BEOrdenDeCompra ListarObjeto(BEOrdenDeCompra oBEOrden)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var buscar = from o in BDXML.Root.Element("OrdenesDeCompra").Descendants("orden")
                                 where o.Attribute("Id").Value.Trim() == oBEOrden.Id.ToString().Trim()
                                 select o;
                    if (buscar.Any())
                    {
                        var o = buscar.First();
                        return new BEOrdenDeCompra
                        {
                            Id = long.Parse(o.Attribute("Id").Value.Trim()),
                            IdProducto = long.Parse(o.Element("IdProducto").Value.Trim()),
                            NombreProducto = o.Element("NombreProducto").Value.Trim(),
                            CantidadSolicitada = int.Parse(o.Element("CantidadSolicitada").Value.Trim()),
                            EstadoOrden = o.Element("EstadoOrden").Value.Trim(),
                            FechaOrden = DateTime.Parse(o.Element("FechaOrden").Value.Trim())
                        };
                    }
                    else { throw new Exception("Error: No se encontró la Orden de Compra!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEOrdenDeCompra> ListarTodo()
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var lista = from o in BDXML.Root.Element("OrdenesDeCompra").Descendants("orden")
                                select new BEOrdenDeCompra
                                {
                                    Id = long.Parse(o.Attribute("Id").Value.Trim()),
                                    IdProducto = long.Parse(o.Element("IdProducto").Value.Trim()),
                                    NombreProducto = o.Element("NombreProducto").Value.Trim(),
                                    CantidadSolicitada = int.Parse(o.Element("CantidadSolicitada").Value.Trim()),
                                    EstadoOrden = o.Element("EstadoOrden").Value.Trim(),
                                    FechaOrden = DateTime.Parse(o.Element("FechaOrden").Value.Trim())
                                };
                    return lista.ToList();
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
                var ids = from o in BDXML.Root.Element("OrdenesDeCompra").Descendants("orden")
                          select long.Parse(o.Attribute("Id").Value.Trim());
                return ids.Any() ? ids.Max() : 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool VerificarExistenciaObjeto(BEOrdenDeCompra objeto) { return false; }

        #endregion
    }
}
