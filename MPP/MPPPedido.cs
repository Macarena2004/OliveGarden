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
    public class MPPPedido : IGestor<BEPedido>
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
                    if (BDXML.Root.Element("Pedidos") == null)
                    {
                        BDXML.Root.Add(new XElement("Pedidos"));
                        BDXML.Save(ruta);
                    }
                    return true;
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Guardar(BEPedido oBEPedido)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEPedido != null)
                    {
                        if (oBEPedido.Id == 0)
                        {
                            long pId = ObtenerUltimoId() + 1;
                            oBEPedido.Id = pId;
                            XElement pedidoElement = new XElement("pedido",
                                new XAttribute("Id", oBEPedido.Id.ToString().Trim()),
                                new XElement("FechaPedido", oBEPedido.FechaPedido.ToString("dd/MM/yyyy").Trim()),
                                new XElement("HoraPedido", oBEPedido.HoraPedido.ToString().Trim()),
                                new XElement("EstadoPedido", oBEPedido.EstadoPedido.Trim()),
                                new XElement("TotalPedido", oBEPedido.TotalPedido.ToString("F2").Trim()),
                                new XElement("IdMesa", oBEPedido.IdMesa.ToString().Trim()));

                            // Guardar detalles
                            XElement detallesElement = new XElement("Detalles");
                            long detId = 1;
                            foreach (var d in oBEPedido.listaDetalles)
                            {
                                d.Id = detId++;
                                d.IdPedido = oBEPedido.Id;
                                detallesElement.Add(new XElement("detalle",
                                    new XAttribute("Id", d.Id.ToString().Trim()),
                                    new XElement("IdProducto", d.IdProducto.ToString().Trim()),
                                    new XElement("NombreProducto", d.NombreProducto.Trim()),
                                    new XElement("Cantidad", d.Cantidad.ToString().Trim()),
                                    new XElement("PrecioUnitario", d.PrecioUnitario.ToString("F2").Trim()),
                                    new XElement("Subtotal", d.CalcularSubtotal().ToString("F2").Trim())));
                            }
                            pedidoElement.Add(detallesElement);
                            BDXML.Root.Element("Pedidos").Add(pedidoElement);
                            BDXML.Save(ruta);
                            return true;
                        }
                        else
                        {
                            // Actualizar estado del pedido
                            var buscar = from p in BDXML.Root.Element("Pedidos").Descendants("pedido")
                                         where p.Attribute("Id").Value.Trim() == oBEPedido.Id.ToString().Trim()
                                         select p;
                            if (buscar.Any())
                            {
                                buscar.First().Element("EstadoPedido").Value = oBEPedido.EstadoPedido.Trim();
                                buscar.First().Element("TotalPedido").Value = oBEPedido.TotalPedido.ToString("F2").Trim();
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: No se encontró el Pedido!"); }
                        }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Eliminar(BEPedido oBEPedido) { throw new NotImplementedException(); }

        public BEPedido ListarObjeto(BEPedido oBEPedido)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var buscar = from p in BDXML.Root.Element("Pedidos").Descendants("pedido")
                                 where p.Attribute("Id").Value.Trim() == oBEPedido.Id.ToString().Trim()
                                 select p;
                    if (buscar.Any())
                    {
                        var p = buscar.First();
                        BEPedido pedido = new BEPedido
                        {
                            Id = long.Parse(p.Attribute("Id").Value.Trim()),
                            FechaPedido = DateTime.Parse(p.Element("FechaPedido").Value.Trim()),
                            HoraPedido = TimeSpan.Parse(p.Element("HoraPedido").Value.Trim()),
                            EstadoPedido = p.Element("EstadoPedido").Value.Trim(),
                            TotalPedido = double.Parse(p.Element("TotalPedido").Value.Trim()),
                            IdMesa = long.Parse(p.Element("IdMesa").Value.Trim())
                        };
                        var detalles = p.Element("Detalles")?.Descendants("detalle");
                        if (detalles != null)
                        {
                            foreach (var d in detalles)
                            {
                                pedido.listaDetalles.Add(new BEDetallePedido
                                {
                                    Id = long.Parse(d.Attribute("Id").Value.Trim()),
                                    IdPedido = pedido.Id,
                                    IdProducto = long.Parse(d.Element("IdProducto").Value.Trim()),
                                    NombreProducto = d.Element("NombreProducto").Value.Trim(),
                                    Cantidad = int.Parse(d.Element("Cantidad").Value.Trim()),
                                    PrecioUnitario = double.Parse(d.Element("PrecioUnitario").Value.Trim()),
                                    Subtotal = double.Parse(d.Element("Subtotal").Value.Trim())
                                });
                            }
                        }
                        return pedido;
                    }
                    else { throw new Exception("Error: No se encontró el Pedido!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEPedido> ListarTodo()
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var lista = from p in BDXML.Root.Element("Pedidos").Descendants("pedido")
                                select new BEPedido
                                {
                                    Id = long.Parse(p.Attribute("Id").Value.Trim()),
                                    FechaPedido = DateTime.Parse(p.Element("FechaPedido").Value.Trim()),
                                    HoraPedido = TimeSpan.Parse(p.Element("HoraPedido").Value.Trim()),
                                    EstadoPedido = p.Element("EstadoPedido").Value.Trim(),
                                    TotalPedido = double.Parse(p.Element("TotalPedido").Value.Trim()),
                                    IdMesa = long.Parse(p.Element("IdMesa").Value.Trim())
                                };
                    return lista.ToList();
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEPedido> ListarPorEstado(string estado)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var lista = from p in BDXML.Root.Element("Pedidos").Descendants("pedido")
                                where p.Element("EstadoPedido").Value.Trim() == estado
                                select new BEPedido
                                {
                                    Id = long.Parse(p.Attribute("Id").Value.Trim()),
                                    FechaPedido = DateTime.Parse(p.Element("FechaPedido").Value.Trim()),
                                    HoraPedido = TimeSpan.Parse(p.Element("HoraPedido").Value.Trim()),
                                    EstadoPedido = p.Element("EstadoPedido").Value.Trim(),
                                    TotalPedido = double.Parse(p.Element("TotalPedido").Value.Trim()),
                                    IdMesa = long.Parse(p.Element("IdMesa").Value.Trim())
                                };
                    return lista.ToList();
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEPedido> ListarPorMesa(long idMesa)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var lista = from p in BDXML.Root.Element("Pedidos").Descendants("pedido")
                                where p.Element("IdMesa").Value.Trim() == idMesa.ToString().Trim()
                                && p.Element("EstadoPedido").Value.Trim() == "Entregado"
                                select new BEPedido
                                {
                                    Id = long.Parse(p.Attribute("Id").Value.Trim()),
                                    FechaPedido = DateTime.Parse(p.Element("FechaPedido").Value.Trim()),
                                    EstadoPedido = p.Element("EstadoPedido").Value.Trim(),
                                    TotalPedido = double.Parse(p.Element("TotalPedido").Value.Trim()),
                                    IdMesa = long.Parse(p.Element("IdMesa").Value.Trim())
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
                var ids = from p in BDXML.Root.Element("Pedidos").Descendants("pedido")
                          select long.Parse(p.Attribute("Id").Value.Trim());
                return ids.Any() ? ids.Max() : 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool VerificarExistenciaObjeto(BEPedido objeto) { return false; }

        #endregion
    }
}
