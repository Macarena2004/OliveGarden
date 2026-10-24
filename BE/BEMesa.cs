using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEMesa : BEEntidad
    {
        #region "Propiedades"
        public int NumeroMesa { get; set; }
        public int Capacidad { get; set; }
        public string EstadoMesa { get; set; } // "Disponible", "Reservada", "Ocupada"
        #endregion

        #region "Constructores"
        public BEMesa() { }

        public BEMesa(long pId, int pNumeroMesa, int pCapacidad, string pEstadoMesa)
        {
            Id = pId;
            NumeroMesa = pNumeroMesa;
            Capacidad = pCapacidad;
            EstadoMesa = pEstadoMesa;
        }
        #endregion

        #region "Métodos"
        public void Disponibilizar() => EstadoMesa = "Disponible";
        public void Reservar() => EstadoMesa = "Reservada";
        public void Ocupar() => EstadoMesa = "Ocupada";
        #endregion
    }
}