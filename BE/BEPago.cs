using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEPago : BEEntidad
    {
        #region "Propiedades"
        public long IdFactura { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; } // "Efectivo", "Tarjeta", "MercadoPago"
        public double Monto { get; set; }
        #endregion

        #region "Constructores"
        public BEPago() { }

        public BEPago(long pId, long pIdFactura, string pMetodoPago, double pMonto)
        {
            Id = pId;
            IdFactura = pIdFactura;
            FechaPago = DateTime.Today;
            MetodoPago = pMetodoPago;
            Monto = pMonto;
        }
        #endregion
    }
}
