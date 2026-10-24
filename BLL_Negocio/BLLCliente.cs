using Abstraccion;
using BE;
using MPP;
using System;
using System.Collections.Generic;

namespace BLL_Negocio
{
    public class BLLCliente : IGestor<BECliente>
    {
        MPPCliente oMPPCliente;

        #region "Constructor"
        public BLLCliente() { oMPPCliente = new MPPCliente(); }
        #endregion

        #region "Métodos"
        public bool CrearXML() { throw new NotImplementedException(); }
        public long ObtenerUltimoId() { throw new NotImplementedException(); }

        public bool Guardar(BECliente oBECliente) { return oMPPCliente.Guardar(oBECliente); }
        public bool Eliminar(BECliente oBECliente) { return oMPPCliente.Eliminar(oBECliente); }
        public BECliente ListarObjeto(BECliente oBECliente) { return oMPPCliente.ListarObjeto(oBECliente); }
        public List<BECliente> ListarTodo() { return oMPPCliente.ListarTodo(); }
        public bool VerificarExistenciaObjeto(BECliente oBECliente) { return oMPPCliente.VerificarExistenciaObjeto(oBECliente); }
        public int ContarFacturasDeCliente(BECliente oBECliente) { return oMPPCliente.ContarFacturasDeCliente(oBECliente); }
        #endregion
    }
}
