using Abstraccion;
using BE.BEComposite;
using MPP.MPPComposite;
using System;
using System.Collections.Generic;

namespace BLL_Negocio.BLLComposite
{
    public class BLLPermiso : IGestor<BEPermiso>
    {
        MPPPermiso oMPPPermiso;

        #region "Constructor"
        public BLLPermiso() { oMPPPermiso = new MPPPermiso(); }
        #endregion

        #region "Métodos"
        public bool CrearXML() { throw new NotImplementedException(); }
        public long ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool Guardar(BEPermiso oBEPermiso) { return oMPPPermiso.Guardar(oBEPermiso); }
        public bool Eliminar(BEPermiso oBEPermiso) { return oMPPPermiso.Eliminar(oBEPermiso); }
        public BEPermiso ListarObjeto(BEPermiso oBEPermiso) { return oMPPPermiso.ListarObjeto(oBEPermiso); }
        public List<BEPermiso> ListarTodo() { return oMPPPermiso.ListarTodo(); }
        public bool VerificarExistenciaObjeto(BEPermiso obj) { return oMPPPermiso.VerificarExistenciaObjeto(obj); }
        #endregion
    }
}
