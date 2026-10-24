using Abstraccion;
using BE;
using MPP;
using System;
using System.Collections.Generic;

namespace BLL_Negocio
{
    public class BLLProducto : IGestor<BEProducto>
    {
        MPPProducto oMPPProducto;

        #region "Constructor"
        public BLLProducto() { oMPPProducto = new MPPProducto(); }
        #endregion

        #region "Métodos"
        public bool CrearXML() { throw new NotImplementedException(); }
        public long ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool Guardar(BEProducto oBEProducto) { return oMPPProducto.Guardar(oBEProducto); }
        public bool Eliminar(BEProducto oBEProducto) { return oMPPProducto.Eliminar(oBEProducto); }
        public BEProducto ListarObjeto(BEProducto oBEProducto) { return oMPPProducto.ListarObjeto(oBEProducto); }
        public List<BEProducto> ListarTodo() { return oMPPProducto.ListarTodo(); }
        public bool VerificarExistenciaObjeto(BEProducto oBEProducto) { return oMPPProducto.VerificarExistenciaObjeto(oBEProducto); }
        public List<BEProducto> ListarProductosCriticos() { return oMPPProducto.ListarProductosCriticos(); }
        public bool ActualizarStock(BEProducto oBEProducto) { return oMPPProducto.ActualizarStock(oBEProducto); }
        #endregion
    }
}

