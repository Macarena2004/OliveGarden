using Abstraccion;
using BE;
using MPP;
using System;
using System.Collections.Generic;

namespace BLL_Negocio
{
    public class BLLBitacora : IGestor<BEBitacora>
    {
        MPPBitacora oMPPBitacora;

        #region "Constructor"
        public BLLBitacora() { oMPPBitacora = new MPPBitacora(); }
        #endregion

        #region "Métodos"
        public bool CrearXML() { throw new NotImplementedException(); }
        public bool Eliminar(BEBitacora objeto) { throw new NotImplementedException(); }
        public BEBitacora ListarObjeto(BEBitacora objeto) { throw new NotImplementedException(); }
        public bool VerificarExistenciaObjeto(BEBitacora obj) { throw new NotImplementedException(); }
        public long ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool Guardar(BEBitacora oBEBitacora) { return oMPPBitacora.Guardar(oBEBitacora); }
        public List<BEBitacora> ListarTodo() { return oMPPBitacora.ListarTodo(); }
        public List<BEBitacora> ListarTodoPorTipo(bool pTipo) { return oMPPBitacora.ListarTodoPorTipo(pTipo); }
        #endregion
    }
}
