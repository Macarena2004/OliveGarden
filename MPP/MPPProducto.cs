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
    public class MPPProducto : IGestor<BEProducto>
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
                    if (BDXML.Root.Element("Productos") == null)
                    {
                        BDXML.Root.Add(new XElement("Productos"));
                        BDXML.Save(ruta);
                    }
                    return true;
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Guardar(BEProducto oBEProducto)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    if (BDXML != null && oBEProducto != null)
                    {
                        if (oBEProducto.Id == 0)
                        {
                            if (!VerificarExistenciaObjeto(oBEProducto))
                            {
                                long pId = ObtenerUltimoId() + 1;
                                oBEProducto.Id = pId;
                                BDXML.Root.Element("Productos").Add(new XElement("producto",
                                    new XAttribute("Id", oBEProducto.Id.ToString().Trim()),
                                    new XElement("NombreProducto", oBEProducto.NombreProducto.Trim()),
                                    new XElement("Descripcion", oBEProducto.Descripcion.Trim()),
                                    new XElement("Precio", oBEProducto.Precio.ToString("F2").Trim()),
                                    new XElement("Stock", oBEProducto.Stock.ToString().Trim())));
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: El Producto ya existe!"); }
                        }
                        else
                        {
                            var buscar = from p in BDXML.Root.Element("Productos").Descendants("producto")
                                         where p.Attribute("Id").Value.Trim() == oBEProducto.Id.ToString().Trim()
                                         select p;
                            if (buscar.Any())
                            {
                                var p = buscar.First();
                                p.Element("NombreProducto").Value = oBEProducto.NombreProducto.Trim();
                                p.Element("Descripcion").Value = oBEProducto.Descripcion.Trim();
                                p.Element("Precio").Value = oBEProducto.Precio.ToString("F2").Trim();
                                p.Element("Stock").Value = oBEProducto.Stock.ToString().Trim();
                                BDXML.Save(ruta);
                                return true;
                            }
                            else { throw new Exception("Error: No se encontró el Producto!"); }
                        }
                    }
                    else { throw new Exception("Error: Datos inválidos!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool Eliminar(BEProducto oBEProducto)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var buscar = from p in BDXML.Root.Element("Productos").Descendants("producto")
                                 where p.Attribute("Id").Value.Trim() == oBEProducto.Id.ToString().Trim()
                                 select p;
                    if (buscar.Any())
                    {
                        buscar.First().Remove();
                        BDXML.Save(ruta);
                        return true;
                    }
                    else { throw new Exception("Error: No se encontró el Producto!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public BEProducto ListarObjeto(BEProducto oBEProducto)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var buscar = from p in BDXML.Root.Element("Productos").Descendants("producto")
                                 where p.Attribute("Id").Value.Trim() == oBEProducto.Id.ToString().Trim()
                                 select p;
                    if (buscar.Any())
                    {
                        var p = buscar.First();
                        return new BEProducto
                        {
                            Id = long.Parse(p.Attribute("Id").Value.Trim()),
                            NombreProducto = p.Element("NombreProducto").Value.Trim(),
                            Descripcion = p.Element("Descripcion").Value.Trim(),
                            Precio = double.Parse(p.Element("Precio").Value.Trim()),
                            Stock = int.Parse(p.Element("Stock").Value.Trim())
                        };
                    }
                    else { throw new Exception("Error: No se encontró el Producto!"); }
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEProducto> ListarTodo()
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var lista = from p in BDXML.Root.Element("Productos").Descendants("producto")
                                select new BEProducto
                                {
                                    Id = long.Parse(p.Attribute("Id").Value.Trim()),
                                    NombreProducto = p.Element("NombreProducto").Value.Trim(),
                                    Descripcion = p.Element("Descripcion").Value.Trim(),
                                    Precio = double.Parse(p.Element("Precio").Value.Trim()),
                                    Stock = int.Parse(p.Element("Stock").Value.Trim())
                                };
                    return lista.ToList();
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public List<BEProducto> ListarProductosCriticos()
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var lista = from p in BDXML.Root.Element("Productos").Descendants("producto")
                                where int.Parse(p.Element("Stock").Value.Trim()) <= 5
                                select new BEProducto
                                {
                                    Id = long.Parse(p.Attribute("Id").Value.Trim()),
                                    NombreProducto = p.Element("NombreProducto").Value.Trim(),
                                    Descripcion = p.Element("Descripcion").Value.Trim(),
                                    Precio = double.Parse(p.Element("Precio").Value.Trim()),
                                    Stock = int.Parse(p.Element("Stock").Value.Trim())
                                };
                    return lista.ToList();
                }
                else { throw new XmlException("Error: No se pudo recuperar el XML!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        public bool ActualizarStock(BEProducto oBEProducto)
        {
            try
            {
                if (CrearXML())
                {
                    BDXML = XDocument.Load(ruta);
                    var buscar = from p in BDXML.Root.Element("Productos").Descendants("producto")
                                 where p.Attribute("Id").Value.Trim() == oBEProducto.Id.ToString().Trim()
                                 select p;
                    if (buscar.Any())
                    {
                        buscar.First().Element("Stock").Value = oBEProducto.Stock.ToString().Trim();
                        BDXML.Save(ruta);
                        return true;
                    }
                    else { throw new Exception("Error: No se encontró el Producto!"); }
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
                var ids = from p in BDXML.Root.Element("Productos").Descendants("producto")
                          select long.Parse(p.Attribute("Id").Value.Trim());
                return ids.Any() ? ids.Max() : 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool VerificarExistenciaObjeto(BEProducto oBEProducto)
        {
            try
            {
                BDXML = XDocument.Load(ruta);
                var buscar = from p in BDXML.Root.Element("Productos").Descendants("producto")
                             where p.Element("NombreProducto").Value.Trim() == oBEProducto.NombreProducto.Trim()
                             select p;
                return buscar.Any();
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}