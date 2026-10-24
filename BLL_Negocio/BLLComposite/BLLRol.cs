using Abstraccion;
using BE;
using BE.BEComposite;
using MPP.MPPComposite;
using System;
using System.Collections.Generic;

namespace BLL_Negocio.BLLComposite
{
    public class BLLRol : IGestor<BERol>
    {
        MPPRol oMPPRol;

        #region "Constructor"
        public BLLRol() { oMPPRol = new MPPRol(); }
        #endregion

        #region "Métodos"
        public bool CrearXML() { throw new NotImplementedException(); }
        public long ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool Guardar(BERol oBERol) { return oMPPRol.Guardar(oBERol); }
        public bool Eliminar(BERol oBERol) { return oMPPRol.Eliminar(oBERol); }
        public BERol ListarObjeto(BERol oBERol) { return oMPPRol.ListarObjeto(oBERol); }
        public List<BERol> ListarTodo() { return oMPPRol.ListarTodo(); }
        public bool VerificarExistenciaObjeto(BERol oBERol) { return oMPPRol.VerificarExistenciaObjeto(oBERol); }
        public bool AsociarRolAPermiso(BERol r, BEPermiso p) { return oMPPRol.AsociarRolAPermiso(r, p); }
        public bool DesasociarRolAPermiso(BERol r, BEPermiso p) { return oMPPRol.DesasociarRolAPermiso(r, p); }
        #endregion
    }
}
