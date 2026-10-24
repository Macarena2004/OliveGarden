using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEDetallePedido : BEEntidad
    {
        #region "Propiedades"
        public long IdPedido { get; set; }
        public long IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double Subtotal { get; set; }
        #endregion

        #region "Constructores"
        public BEDetallePedido() { }

        public BEDetallePedido(long pId, long pIdPedido, long pIdProducto,
                               string pNombreProducto, int pCantidad, double pPrecioUnitario)
        {
            Id = pId;
            IdPedido = pIdPedido;
            IdProducto = pIdProducto;
            NombreProducto = pNombreProducto;
            Cantidad = pCantidad;
            PrecioUnitario = pPrecioUnitario;
            Subtotal = CalcularSubtotal();
        }
        #endregion

        #region "Métodos"
        public double CalcularSubtotal() => Cantidad * PrecioUnitario;
        #endregion
    }
}
