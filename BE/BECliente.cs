using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BECliente : BEPersona
    {
        #region "Propiedades"
        public DateTime FechaNacimiento { get; set; }
        public List<BEFactura> listaFacturas { get; set; }
        #endregion

        #region "Constructores"
        public BECliente()
        {
            listaFacturas = new List<BEFactura>();
        }

        public BECliente(long pId, string pNombre, string pApellido,
                         string pTelefono, string pEmail, DateTime pFechaNacimiento)
            : base(pId, pNombre, pApellido, pTelefono, pEmail)
        {
            FechaNacimiento = pFechaNacimiento;
            listaFacturas = new List<BEFactura>();
        }
        #endregion

        #region "Métodos"
        public bool EsFrecuente(int cantidadFacturas)
        {
            return cantidadFacturas > 8;
        }

        public bool CumpleanosHoy()
        {
            return FechaNacimiento.Day == DateTime.Today.Day &&
                   FechaNacimiento.Month == DateTime.Today.Month;
        }
        #endregion
    }
}
