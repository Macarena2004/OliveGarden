using Abstraccion;
using BE;
using MPP;
using System;
using System.Collections.Generic;

namespace BLL_Negocio
{
    public class BLLPedido : IGestor<BEPedido>
    {
        MPPPedido oMPPPedido;
        MPPProducto oMPPProducto;

        #region "Constructor"
        public BLLPedido()
        {
            oMPPPedido = new MPPPedido();
            oMPPProducto = new MPPProducto();
        }
        #endregion

        #region "Métodos"
        public bool CrearXML() { throw new NotImplementedException(); }
        public long ObtenerUltimoId() { throw new NotImplementedException(); }
        public bool VerificarExistenciaObjeto(BEPedido obj) { throw new NotImplementedException(); }
        public bool Eliminar(BEPedido objeto) { throw new NotImplementedException(); }

        public bool Guardar(BEPedido oBEPedido)
        {
            try
            {
                if (oBEPedido == null)
                    throw new Exception("Error: El Pedido no puede ser nulo!");

                // Verificar stock de cada producto antes de guardar
                foreach (var detalle in oBEPedido.listaDetalles)
                {
                    BEProducto producto = oMPPProducto.ListarObjeto(
                        new BEProducto { Id = detalle.IdProducto });

                    if (producto.Stock < detalle.Cantidad)
                        throw new Exception($"Error: No hay stock suficiente para el producto: {detalle.NombreProducto}!");
                }

                // Calcular total
                oBEPedido.CalcularTotal();

                bool resultado = oMPPPedido.Guardar(oBEPedido);

                // Descontar stock de cada producto
                if (resultado)
                {
                    foreach (var detalle in oBEPedido.listaDetalles)
                    {
                        BEProducto producto = oMPPProducto.ListarObjeto(
                            new BEProducto { Id = detalle.IdProducto });
                        producto.DescontarStock(detalle.Cantidad);
                        oMPPProducto.ActualizarStock(producto);
                    }
                }
                return resultado;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool ActualizarEstado(BEPedido oBEPedido) { return oMPPPedido.Guardar(oBEPedido); }
        public BEPedido ListarObjeto(BEPedido oBEPedido) { return oMPPPedido.ListarObjeto(oBEPedido); }
        public List<BEPedido> ListarTodo() { return oMPPPedido.ListarTodo(); }
        public List<BEPedido> ListarPorEstado(string estado) { return oMPPPedido.ListarPorEstado(estado); }
        public List<BEPedido> ListarPorMesa(long idMesa) { return oMPPPedido.ListarPorMesa(idMesa); }
        #endregion
    }
}