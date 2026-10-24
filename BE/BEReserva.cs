using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEReserva : BEEntidad
    {
        #region "Propiedades"
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public int CantidadPersonas { get; set; }
        public string EstadoReserva { get; set; } // "Activa", "Cancelada", "Finalizada"
        public long IdCliente { get; set; }
        public long IdMesa { get; set; }
        #endregion

        #region "Constructores"
        public BEReserva() { }

        public BEReserva(long pId, DateTime pFecha, TimeSpan pHora,
                         int pCantidadPersonas, long pIdCliente, long pIdMesa)
        {
            Id = pId;
            Fecha = pFecha;
            Hora = pHora;
            CantidadPersonas = pCantidadPersonas;
            EstadoReserva = "Activa";
            IdCliente = pIdCliente;
            IdMesa = pIdMesa;
        }
        #endregion

        #region "Métodos"
        public bool PuedeModificarOCancelar()
        {
            DateTime fechaHoraReserva = Fecha.Date + Hora;
            return (fechaHoraReserva - DateTime.Now).TotalHours > 3;
        }

        public void Confirmar() => EstadoReserva = "Activa";
        public void Cancelar() => EstadoReserva = "Cancelada";
        public void Finalizar() => EstadoReserva = "Finalizada";
        #endregion
    }
}
