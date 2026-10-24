using Abstraccion;
using BE;
using MPP;
using System;
using System.Collections.Generic;

namespace BLL_Negocio
{
    public class BLLOrdenDeCompra : IGestor<BEOrdenDeCompra>
    {
        MPPOrdenDeCompra oMPPOrden;
        MPPProducto oMPPProducto;

        #region "Constructor"
        public BLLOrdenDeCompra()
        {
            oMPPOrden = new MPPOrdenDeCompra();
            oMPPProducto = new MPPProducto();
        }
        #endregion

        #region "Métodos"
        public bool CrearXML() { throw new NotImplementedException(); }
        public long ObtenerUltimoId() { throw new NotImplementedException(); }
        public bool Eliminar(BEOrdenDeCompra objeto) { throw new NotImplementedException(); }
        public bool VerificarExistenciaObjeto(BEOrdenDeCompra obj) { throw new NotImplementedException(); }

        public bool Guardar(BEOrdenDeCompra oBEOrden) { return oMPPOrden.Guardar(oBEOrden); }
        public BEOrdenDeCompra ListarObjeto(BEOrdenDeCompra obj) { return oMPPOrden.ListarObjeto(obj); }
        public List<BEOrdenDeCompra> ListarTodo() { return oMPPOrden.ListarTodo(); }

        /// <summary>
        /// Devuelve los productos con stock crítico (5 unidades o menos).
        /// </summary>
        public List<BEProducto> ObtenerProductosCriticos()
        {
            try
            {
                return oMPPProducto.ListarProductosCriticos();
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Genera una orden de compra para un producto crítico.
        /// </summary>
        public bool GenerarOrden(BEProducto oBEProducto, int cantidadSolicitada)
        {
            try
            {
                if (oBEProducto == null)
                    throw new Exception("Error: El Producto no puede ser nulo!");

                if (cantidadSolicitada <= 0)
                    throw new Exception("Error: La cantidad solicitada debe ser mayor a cero!");

                BEOrdenDeCompra orden = new BEOrdenDeCompra(
                    0,
                    oBEProducto.Id,
                    oBEProducto.NombreProducto,
                    cantidadSolicitada);

                orden.Generar();
                orden.NotificarProveedor();

                return oMPPOrden.Guardar(orden);
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
    }
}
