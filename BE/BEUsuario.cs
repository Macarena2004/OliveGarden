using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BEComposite;

namespace BE
{
    public class BEUsuario : BEEntidad
    {
        #region "Propiedades"
        public string Usuario { get; set; }
        public string Password { get; set; }
        public bool Activo { get; set; }
        public bool Bloqueado { get; set; }
        public List<BERol> listaRoles { get; set; }
        public List<BEPermiso> listaPermisos { get; set; }
        #endregion

        #region "Constructores"
        public BEUsuario()
        {
            listaRoles = new List<BERol>();
            listaPermisos = new List<BEPermiso>();
        }

        public BEUsuario(long pId, string pUsuario, string pPassword, bool pActivo, bool pBloqueado)
        {
            Id = pId;
            Usuario = pUsuario;
            Password = pPassword;
            Activo = pActivo;
            Bloqueado = pBloqueado;
            listaRoles = new List<BERol>();
            listaPermisos = new List<BEPermiso>();
        }
        #endregion

        #region "Métodos"
        public override string ToString()
        {
            return Id.ToString().Trim();
        }
        #endregion
    }
}
