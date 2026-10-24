using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEOrdenDeCompra : BEEntidad
    {
        #region "Propiedades"
        public long IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int CantidadSolicitada { get; set; }
        public string EstadoOrden { get; set; } // "Pendiente", "Enviada", "Recibida"
        public DateTime FechaOrden { get; set; }
        #endregion

        #region "Constructores"
        public BEOrdenDeCompra() { }

        public BEOrdenDeCompra(long pId, long pIdProducto, string pNombreProducto, int pCantidadSolicitada)
        {
            Id = pId;
            IdProducto = pIdProducto;
            NombreProducto = pNombreProducto;
            CantidadSolicitada = pCantidadSolicitada;
            EstadoOrden = "Pendiente";
            FechaOrden = DateTime.Today;
        }
        #endregion

        #region "Métodos"
        public void Generar() => EstadoOrden = "Pendiente";
        public void NotificarProveedor() => EstadoOrden = "Enviada";
        #endregion
    }
}