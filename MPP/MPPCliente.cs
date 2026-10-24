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
    public class MPPCliente : IGestor<BECliente>
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
                    if (BDXML.Root.Element("Clientes") == null)
                    {
                        BDXML.Root.Add(new XElement("Clientes"));
                        BDXML.Save(ruta);
                    }
                    return true;
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Guardar(BECliente oBECliente)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBECliente != null)
                    {
                        if (oBECliente.Id == 0)
                        {
                            if (!VerificarExistenciaObjeto(oBECliente))
                            {
                                long pId = ObtenerUltimoId() + 1;
                                oBECliente.Id = pId;
                                BDXML.Root.Element("Clientes").Add(new XElement("cliente",
                                    new XAttribute("Id", oBECliente.Id.ToString().Trim()),
                                    new XElement("Nombre", oBECliente.Nombre.Trim()),
                                    new XElement("Apellido", oBECliente.Apellido.Trim()),
                                    new XElement("Telefono", oBECliente.Telefono.Trim()),
                                    new XElement("Email", oBECliente.Email.Trim()),
                                    new XElement("FechaNacimiento", oBECliente.FechaNacimiento.ToString("dd/MM/yyyy").Trim())));
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: El Cliente ya existe!"); }
                        }
                        else
                        {
                            var buscar = from c in BDXML.Root.Element("Clientes").Descendants("cliente")
                                         where c.Attribute("Id").Value.Trim() == oBECliente.Id.ToString().Trim()
                                         select c;
                            if (buscar.Any())
                            {
                                var c = buscar.First();
                                c.Element("Nombre").Value = oBECliente.Nombre.Trim();
                                c.Element("Apellido").Value = oBECliente.Apellido.Trim();
                                c.Element("Telefono").Value = oBECliente.Telefono.Trim();
                                c.Element("Email").Value = oBECliente.Email.Trim();
                                c.Element("FechaNacimiento").Value = oBECliente.FechaNacimiento.ToString("dd/MM/yyyy").Trim();
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: No se encontró el Cliente!"); }
                        }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Eliminar(BECliente oBECliente)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBECliente != null)
                    {
                        var buscar = from c in BDXML.Root.Element("Clientes").Descendants("cliente")
                                     where c.Attribute("Id").Value.Trim() == oBECliente.Id.ToString().Trim()
                                     select c;
                        if (buscar.Any())
                        {
                            // Verificar que no tenga facturas asociadas
                            var enFacturas = from f in BDXML.Root.Element("Facturas").Descendants("factura")
                                             where f.Element("IdCliente").Value.Trim() == oBECliente.Id.ToString().Trim()
                                             select f;
                            if (!enFacturas.Any())
                            {
                                buscar.First().Remove();
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: No se puede eliminar un Cliente con Facturas asociadas!"); }
                        }
                        else { throw new Exception("Error: No se encontró el Cliente!"); }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public BECliente ListarObjeto(BECliente oBECliente)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBECliente != null)
                    {
                        var buscar = from c in BDXML.Root.Element("Clientes").Descendants("cliente")
                                     where c.Attribute("Id").Value.Trim() == oBECliente.Id.ToString().Trim()
                                     select c;
                        if (buscar.Any())
                        {
                            var c = buscar.First();
                            return new BECliente
                            {
                                Id = long.Parse(c.Attribute("Id").Value.Trim()),
                                Nombre = c.Element("Nombre").Value.Trim(),
                                Apellido = c.Element("Apellido").Value.Trim(),
                                Telefono = c.Element("Telefono").Value.Trim(),
                                Email = c.Element("Email").Value.Trim(),
                                FechaNacimiento = DateTime.Parse(c.Element("FechaNacimiento").Value.Trim())
                            };
                        }
                        else { throw new Exception("Error: No se encontró el Cliente!"); }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BECliente> ListarTodo()
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null)
                    {
                        var lista = from c in BDXML.Root.Element("Clientes").Descendants("cliente")
                                    select new BECliente
                                    {
                                        Id = long.Parse(c.Attribute("Id").Value.Trim()),
                                        Nombre = c.Element("Nombre").Value.Trim(),
                                        Apellido = c.Element("Apellido").Value.Trim(),
                                        Telefono = c.Element("Telefono").Value.Trim(),
                                        Email = c.Element("Email").Value.Trim(),
                                        FechaNacimiento = DateTime.Parse(c.Element("FechaNacimiento").Value.Trim())
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
                var ids = from c in BDXML.Root.Element("Clientes").Descendants("cliente")
                          select long.Parse(c.Attribute("Id").Value.Trim());
                return ids.Any() ? ids.Max() : 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool VerificarExistenciaObjeto(BECliente oBECliente)
        {
            try
            {
                BDXML = XDocument.Load(ruta);
                var buscar = from c in BDXML.Root.Element("Clientes").Descendants("cliente")
                             where c.Element("Email").Value.Trim() == oBECliente.Email.Trim()
                             select c;
                return buscar.Any();
            }
            catch (Exception ex) { throw ex; }
        }

        public int ContarFacturasDeCliente(BECliente oBECliente)
        {
            try
            {
                BDXML = XDocument.Load(ruta);
                var facturas = from f in BDXML.Root.Element("Facturas").Descendants("factura")
                               where f.Element("IdCliente").Value.Trim() == oBECliente.Id.ToString().Trim()
                               select f;
                return facturas.Count();
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}
