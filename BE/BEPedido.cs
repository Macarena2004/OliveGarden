using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEPedido : BEEntidad
    {
        #region "Propiedades"
        public DateTime FechaPedido { get; set; }
        public TimeSpan HoraPedido { get; set; }
        public string EstadoPedido { get; set; } // "Pendiente", "EnPreparacion", "Terminado", "Entregado"
        public double TotalPedido { get; set; }
        public long IdMesa { get; set; }
        public List<BEDetallePedido> listaDetalles { get; set; }
        #endregion

        #region "Constructores"
        public BEPedido()
        {
            listaDetalles = new List<BEDetallePedido>();
        }

        public BEPedido(long pId, long pIdMesa)
        {
            Id = pId;
            IdMesa = pIdMesa;
            FechaPedido = DateTime.Today;
            HoraPedido = DateTime.Now.TimeOfDay;
            EstadoPedido = "Pendiente";
            listaDetalles = new List<BEDetallePedido>();
        }
        #endregion

        #region "Métodos"
        public double CalcularTotal()
        {
            TotalPedido = listaDetalles.Sum(d => d.CalcularSubtotal());
            return TotalPedido;
        }

        public void EnviarACocina() => EstadoPedido = "Pendiente";
        public void MarcarEnPreparacion() => EstadoPedido = "EnPreparacion";
        public void MarcarTerminado() => EstadoPedido = "Terminado";
        public void MarcarEntregado() => EstadoPedido = "Entregado";
        #endregion
    }
}
