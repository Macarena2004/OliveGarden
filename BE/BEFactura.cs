using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEFactura : BEEntidad
    {
        #region "Propiedades"
        public DateTime FechaFactura { get; set; }
        public double TotalFactura { get; set; }
        public string EstadoFactura { get; set; } // "PendientePago", "Pagada"
        public long IdCliente { get; set; }
        public long IdMesa { get; set; }
        public List<BEDetallePedido> listaDetalles { get; set; }
        #endregion

        #region "Constructores"
        public BEFactura()
        {
            listaDetalles = new List<BEDetallePedido>();
        }

        public BEFactura(long pId, long pIdCliente, long pIdMesa)
        {
            Id = pId;
            IdCliente = pIdCliente;
            IdMesa = pIdMesa;
            FechaFactura = DateTime.Today;
            EstadoFactura = "PendientePago";
            listaDetalles = new List<BEDetallePedido>();
        }
        #endregion

        #region "Métodos"
        public double CalcularTotal()
        {
            double subtotal = 0;
            foreach (var d in listaDetalles)
                subtotal += d.CalcularSubtotal();
            TotalFactura = subtotal;
            return TotalFactura;
        }

        public void AplicarDescuento(double porcentaje)
        {
            TotalFactura = TotalFactura - (TotalFactura * porcentaje / 100);
        }

        public void MarcarPagada() => EstadoFactura = "Pagada";
        #endregion
    }
}