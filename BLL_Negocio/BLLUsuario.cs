using Abstraccion;
using BE;
using BE.BEComposite;
using MPP;
using System;
using System.Collections.Generic;

namespace BLL_Negocio
{
    public class BLLUsuario : IGestor<BEUsuario>
    {
        MPPUsuario oMPPUsuario;

        #region "Constructor"
        public BLLUsuario() { oMPPUsuario = new MPPUsuario(); }
        #endregion

        #region "Métodos"
        public bool CrearXML() { return oMPPUsuario.CrearXML(); }
        public long ObtenerUltimoId() { throw new NotImplementedException(); }
        public bool VerificarExistenciaObjeto(BEUsuario obj) { throw new NotImplementedException(); }

        public bool Guardar(BEUsuario oBEUsuario) { return oMPPUsuario.Guardar(oBEUsuario); }
        public bool Eliminar(BEUsuario oBEUsuario) { return oMPPUsuario.Eliminar(oBEUsuario); }
        public BEUsuario ListarObjeto(BEUsuario oBEUsuario) { return oMPPUsuario.ListarObjeto(oBEUsuario); }
        public BEUsuario ListarObjetoPorId(BEUsuario oBEUsuario) { return oMPPUsuario.ListarObjetoPorId(oBEUsuario); }
        public List<BEUsuario> ListarTodo() { return oMPPUsuario.ListarTodo(); }
        public bool Login(BEUsuario oBEUsuario) { return oMPPUsuario.Login(oBEUsuario); }
        public string HashearPassword(string pPass) { return oMPPUsuario.HashearPassword(pPass); }
        public bool VerificarPassword(string pPass, string pAlmacenado) { return oMPPUsuario.VerificarPassword(pPass, pAlmacenado); }
        public bool AsociarUsuarioARol(BEUsuario u, BERol r) { return oMPPUsuario.AsociarUsuarioARol(u, r); }
        public bool DesasociarUsuarioARol(BEUsuario u, BERol r) { return oMPPUsuario.DesasociarUsuarioARol(u, r); }
        public bool AsociarPermisoAUsuario(BEUsuario u, BEPermiso p) { return oMPPUsuario.AsociarPermisoAUsuario(u, p); }
        public bool DesasociarPermisoAUsuario(BEUsuario u, BEPermiso p) { return oMPPUsuario.DesasociarPermisoAUsuario(u, p); }
        public List<BEPermiso> ListarTodosLosPermisosDelUsuario(BEUsuario u) { return oMPPUsuario.ListarTodosLosPermisosDelUsuario(u); }
        #endregion
    }
}
