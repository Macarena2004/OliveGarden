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
    public class MPPFactura : IGestor<BEFactura>
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
                    if (BDXML.Root.Element("Facturas") == null)
                    {
                        BDXML.Root.Add(new XElement("Facturas"));
                        BDXML.Save(ruta);
                    }
                    return true;
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Guardar(BEFactura oBEFactura)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEFactura != null)
                    {
                        if (oBEFactura.Id == 0)
                        {
                            long pId = ObtenerUltimoId() + 1;
                            oBEFactura.Id = pId;
                            XElement facturaElement = new XElement("factura",
                                new XAttribute("Id", oBEFactura.Id.ToString().Trim()),
                                new XElement("FechaFactura", oBEFactura.FechaFactura.ToString("dd/MM/yyyy").Trim()),
                                new XElement("TotalFactura", oBEFactura.TotalFactura.ToString("F2").Trim()),
                                new XElement("EstadoFactura", oBEFactura.EstadoFactura.Trim()),
                                new XElement("IdCliente", oBEFactura.IdCliente.ToString().Trim()),
                                new XElement("IdMesa", oBEFactura.IdMesa.ToString().Trim()));

                            // Guardar detalles
                            XElement detallesElement = new XElement("Detalles");
                            foreach (var d in oBEFactura.listaDetalles)
                            {
                                detallesElement.Add(new XElement("detalle",
                                    new XAttribute("Id", d.Id.ToString().Trim()),
                                    new XElement("IdProducto", d.IdProducto.ToString().Trim()),
                                    new XElement("NombreProducto", d.NombreProducto.Trim()),
                                    new XElement("Cantidad", d.Cantidad.ToString().Trim()),
                                    new XElement("PrecioUnitario", d.PrecioUnitario.ToString("F2").Trim()),
                                    new XElement("Subtotal", d.CalcularSubtotal().ToString("F2").Trim())));
                            }
                            facturaElement.Add(detallesElement);
                            BDXML.Root.Element("Facturas").Add(facturaElement);
                            BDXML.Save(ruta);
                            return true;
                        }
                        else
                        {
                            // Actualizar estado
                            var buscar = from f in BDXML.Root.Element("Facturas").Descendants("factura")
                                         where f.Attribute("Id").Value.Trim() == oBEFactura.Id.ToString().Trim()
                                         select f;
                            if (buscar.Any())
                            {
                                buscar.First().Element("EstadoFactura").Value = oBEFactura.EstadoFactura.Trim();
                                buscar.First().Element("TotalFactura").Value = oBEFactura.TotalFactura.ToString("F2").Trim();
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: No se encontró la Factura!"); }
                        }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Eliminar(BEFactura objeto) { throw new NotImplementedException(); }

        public BEFactura ListarObjeto(BEFactura oBEFactura)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var buscar = from f in BDXML.Root.Element("Facturas").Descendants("factura")
                                 where f.Attribute("Id").Value.Trim() == oBEFactura.Id.ToString().Trim()
                                 select f;
                    if (buscar.Any())
                    {
                        var f = buscar.First();
                        BEFactura factura = new BEFactura
                        {
                            Id = long.Parse(f.Attribute("Id").Value.Trim()),
                            FechaFactura = DateTime.Parse(f.Element("FechaFactura").Value.Trim()),
                            TotalFactura = double.Parse(f.Element("TotalFactura").Value.Trim()),
                            EstadoFactura = f.Element("EstadoFactura").Value.Trim(),
                            IdCliente = long.Parse(f.Element("IdCliente").Value.Trim()),
                            IdMesa = long.Parse(f.Element("IdMesa").Value.Trim())
                        };
                        var detalles = f.Element("Detalles")?.Descendants("detalle");
                        if (detalles != null)
                        {
                            foreach (var d in detalles)
                            {
                                factura.listaDetalles.Add(new BEDetallePedido
                                {
                                    Id = long.Parse(d.Attribute("Id").Value.Trim()),
                                    IdProducto = long.Parse(d.Element("IdProducto").Value.Trim()),
                                    NombreProducto = d.Element("NombreProducto").Value.Trim(),
                                    Cantidad = int.Parse(d.Element("Cantidad").Value.Trim()),
                                    PrecioUnitario = double.Parse(d.Element("PrecioUnitario").Value.Trim()),
                                    Subtotal = double.Parse(d.Element("Subtotal").Value.Trim())
                                });
                            }
                        }
                        return factura;
                    }
                    else { throw new Exception("Error: No se encontró la Factura!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEFactura> ListarTodo()
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var lista = from f in BDXML.Root.Element("Facturas").Descendants("factura")
                                select new BEFactura
                                {
                                    Id = long.Parse(f.Attribute("Id").Value.Trim()),
                                    FechaFactura = DateTime.Parse(f.Element("FechaFactura").Value.Trim()),
                                    TotalFactura = double.Parse(f.Element("TotalFactura").Value.Trim()),
                                    EstadoFactura = f.Element("EstadoFactura").Value.Trim(),
                                    IdCliente = long.Parse(f.Element("IdCliente").Value.Trim()),
                                    IdMesa = long.Parse(f.Element("IdMesa").Value.Trim())
                                };
                    return lista.ToList();
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEFactura> ListarPorPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var lista = from f in BDXML.Root.Element("Facturas").Descendants("factura")
                                let fecha = DateTime.Parse(f.Element("FechaFactura").Value.Trim())
                                where fecha >= fechaInicio && fecha <= fechaFin
                                select new BEFactura
                                {
                                    Id = long.Parse(f.Attribute("Id").Value.Trim()),
                                    FechaFactura = fecha,
                                    TotalFactura = double.Parse(f.Element("TotalFactura").Value.Trim()),
                                    EstadoFactura = f.Element("EstadoFactura").Value.Trim(),
                                    IdCliente = long.Parse(f.Element("IdCliente").Value.Trim()),
                                    IdMesa = long.Parse(f.Element("IdMesa").Value.Trim())
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
                var ids = from f in BDXML.Root.Element("Facturas").Descendants("factura")
                          select long.Parse(f.Attribute("Id").Value.Trim());
                return ids.Any() ? ids.Max() : 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool VerificarExistenciaObjeto(BEFactura objeto) { return false; }

        #endregion
    }
}
