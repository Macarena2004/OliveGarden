using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEPersona : BEEntidad
    {
        #region "Propiedades"
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        #endregion

        #region "Constructores"
        public BEPersona() { }

        public BEPersona(long pId, string pNombre, string pApellido, string pTelefono, string pEmail)
        {
            Id = pId;
            Nombre = pNombre;
            Apellido = pApellido;
            Telefono = pTelefono;
            Email = pEmail;
        }
        #endregion
    }
}
