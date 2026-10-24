using Abstraccion;
using BE;
using MPP;
using System;
using System.Collections.Generic;

namespace BLL_Negocio
{
    public class BLLMesa : IGestor<BEMesa>
    {
        MPPMesa oMPPMesa;

        #region "Constructor"
        public BLLMesa() { oMPPMesa = new MPPMesa(); }
        #endregion

        #region "Métodos"
        public bool CrearXML()                                  { throw new NotImplementedException(); }
        public long ObtenerUltimoId()                           { throw new NotImplementedException(); }

        public bool Guardar(BEMesa oBEMesa)                     { return oMPPMesa.Guardar(oBEMesa); }
        public bool Eliminar(BEMesa oBEMesa)                    { return oMPPMesa.Eliminar(oBEMesa); }
        public BEMesa ListarObjeto(BEMesa oBEMesa)              { return oMPPMesa.ListarObjeto(oBEMesa); }
        public List<BEMesa> ListarTodo()                        { return oMPPMesa.ListarTodo(); }
        public List<BEMesa> ListarMesasDisponibles()            { return oMPPMesa.ListarMesasDisponibles(); }
        public bool VerificarExistenciaObjeto(BEMesa oBEMesa)   { return oMPPMesa.VerificarExistenciaObjeto(oBEMesa); }
        public bool ActualizarEstado(BEMesa oBEMesa)            { return oMPPMesa.ActualizarEstado(oBEMesa); }
        #endregion
    }
}
