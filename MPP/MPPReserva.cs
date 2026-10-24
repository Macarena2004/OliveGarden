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
    public class MPPReserva : IGestor<BEReserva>
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
                    if (BDXML.Root.Element("Reservas") == null)
                    {
                        BDXML.Root.Add(new XElement("Reservas"));
                        BDXML.Save(ruta);
                    }
                    return true;
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Guardar(BEReserva oBEReserva)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEReserva != null)
                    {
                        if (oBEReserva.Id == 0)
                        {
                            long pId = ObtenerUltimoId() + 1;
                            oBEReserva.Id = pId;
                            BDXML.Root.Element("Reservas").Add(new XElement("reserva",
                                new XAttribute("Id", oBEReserva.Id.ToString().Trim()),
                                new XElement("Fecha", oBEReserva.Fecha.ToString("dd/MM/yyyy").Trim()),
                                new XElement("Hora", oBEReserva.Hora.ToString().Trim()),
                                new XElement("CantidadPersonas", oBEReserva.CantidadPersonas.ToString().Trim()),
                                new XElement("EstadoReserva", oBEReserva.EstadoReserva.Trim()),
                                new XElement("IdCliente", oBEReserva.IdCliente.ToString().Trim()),
                                new XElement("IdMesa", oBEReserva.IdMesa.ToString().Trim())));
                            BDXML.Save(ruta);
                            return true;
                        }
                        else
                        {
                            var buscar = from r in BDXML.Root.Element("Reservas").Descendants("reserva")
                                         where r.Attribute("Id").Value.Trim() == oBEReserva.Id.ToString().Trim()
                                         select r;
                            if (buscar.Any())
                            {
                                var r = buscar.First();
                                r.Element("Fecha").Value = oBEReserva.Fecha.ToString("dd/MM/yyyy").Trim();
                                r.Element("Hora").Value = oBEReserva.Hora.ToString().Trim();
                                r.Element("CantidadPersonas").Value = oBEReserva.CantidadPersonas.ToString().Trim();
                                r.Element("EstadoReserva").Value = oBEReserva.EstadoReserva.Trim();
                                r.Element("IdMesa").Value = oBEReserva.IdMesa.ToString().Trim();
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: No se encontró la Reserva!"); }
                        }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Eliminar(BEReserva oBEReserva)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var buscar = from r in BDXML.Root.Element("Reservas").Descendants("reserva")
                                 where r.Attribute("Id").Value.Trim() == oBEReserva.Id.ToString().Trim()
                                 select r;
                    if (buscar.Any())
                    {
                        buscar.First().Remove();
                        BDXML.Save(ruta);
                        return true;
                    }
                    else { throw new Exception("Error: No se encontró la Reserva!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public BEReserva ListarObjeto(BEReserva oBEReserva)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var buscar = from r in BDXML.Root.Element("Reservas").Descendants("reserva")
                                 where r.Attribute("Id").Value.Trim() == oBEReserva.Id.ToString().Trim()
                                 select r;
                    if (buscar.Any())
                    {
                        var r = buscar.First();
                        return new BEReserva
                        {
                            Id = long.Parse(r.Attribute("Id").Value.Trim()),
                            Fecha = DateTime.Parse(r.Element("Fecha").Value.Trim(),
                            System.Globalization.CultureInfo.GetCultureInfo("es-AR")),
                            Hora = TimeSpan.Parse(r.Element("Hora").Value.Trim(),
                            System.Globalization.CultureInfo.InvariantCulture),
                            CantidadPersonas = int.Parse(r.Element("CantidadPersonas").Value.Trim()),
                            EstadoReserva = r.Element("EstadoReserva").Value.Trim(),
                            IdCliente = long.Parse(r.Element("IdCliente").Value.Trim()),
                            IdMesa = long.Parse(r.Element("IdMesa").Value.Trim())
                        };
                    }
                    else { throw new Exception("Error: No se encontró la Reserva!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEReserva> ListarTodo()
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var lista = from r in BDXML.Root.Element("Reservas").Descendants("reserva")
                                select new BEReserva
                                {
                                    Id = long.Parse(r.Attribute("Id").Value.Trim()),
                                    Fecha = DateTime.Parse(r.Element("Fecha").Value.Trim(),
                                    System.Globalization.CultureInfo.GetCultureInfo("es-AR")),
                                    Hora = TimeSpan.Parse(r.Element("Hora").Value.Trim(),
                                    System.Globalization.CultureInfo.InvariantCulture),
                                    CantidadPersonas = int.Parse(r.Element("CantidadPersonas").Value.Trim()),
                                    EstadoReserva = r.Element("EstadoReserva").Value.Trim(),
                                    IdCliente = long.Parse(r.Element("IdCliente").Value.Trim()),
                                    IdMesa = long.Parse(r.Element("IdMesa").Value.Trim())
                                };
                    return lista.ToList();
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEReserva> ListarActivas()
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var lista = from r in BDXML.Root.Element("Reservas").Descendants("reserva")
                                where r.Element("EstadoReserva").Value.Trim() == "Activa"
                                select new BEReserva
                                {
                                    Id = long.Parse(r.Attribute("Id").Value.Trim()),
                                    Fecha = DateTime.Parse(r.Element("Fecha").Value.Trim(),
                                    System.Globalization.CultureInfo.GetCultureInfo("es-AR")),
                                    Hora = TimeSpan.Parse(r.Element("Hora").Value.Trim(),
                                    System.Globalization.CultureInfo.InvariantCulture),
                                    CantidadPersonas = int.Parse(r.Element("CantidadPersonas").Value.Trim()),
                                    EstadoReserva = r.Element("EstadoReserva").Value.Trim(),
                                    IdCliente = long.Parse(r.Element("IdCliente").Value.Trim()),
                                    IdMesa = long.Parse(r.Element("IdMesa").Value.Trim())
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
                var ids = from r in BDXML.Root.Element("Reservas").Descendants("reserva")
                          select long.Parse(r.Attribute("Id").Value.Trim());
                return ids.Any() ? ids.Max() : 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool VerificarExistenciaObjeto(BEReserva oBEReserva) { return false; }

        #endregion
    }
}
