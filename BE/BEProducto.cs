using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEProducto : BEEntidad
    {
        #region "Propiedades"
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        #endregion

        #region "Constructores"
        public BEProducto() { }

        public BEProducto(long pId, string pNombre, string pDescripcion, double pPrecio, int pStock)
        {
            Id = pId;
            NombreProducto = pNombre;
            Descripcion = pDescripcion;
            Precio = pPrecio;
            Stock = pStock;
        }
        #endregion

        #region "Métodos"
        public bool HayStock() => Stock > 0;
        public void DescontarStock(int cantidad) => Stock -= cantidad;
        public void ReponerStock(int cantidad) => Stock += cantidad;
        #endregion
    }
}
