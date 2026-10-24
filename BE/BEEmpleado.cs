using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEEmpleado : BEPersona
    {
        #region "Propiedades"
        public string Cargo { get; set; }  // "Recepcionista", "Cocinero", "Mozo", "Administrador"
        public BEUsuario oBEUsuario { get; set; }
        #endregion

        #region "Constructores"
        public BEEmpleado() { }

        public BEEmpleado(long pId, string pNombre, string pApellido,
                          string pTelefono, string pEmail, string pCargo)
            : base(pId, pNombre, pApellido, pTelefono, pEmail)
        {
            Cargo = pCargo;
        }
        #endregion
    }
}
