using Abstraccion;
using BE;
using MPP;
using System;
using System.Collections.Generic;

namespace BLL_Negocio
{
    public class BLLReserva : IGestor<BEReserva>
    {
        MPPReserva oMPPReserva;
        MPPMesa oMPPMesa;

        #region "Constructor"
        public BLLReserva()
        {
            oMPPReserva = new MPPReserva();
            oMPPMesa = new MPPMesa();
        }
        #endregion

        #region "Métodos"
        public bool CrearXML() { throw new NotImplementedException(); }
        public long ObtenerUltimoId() { throw new NotImplementedException(); }
        public bool VerificarExistenciaObjeto(BEReserva obj) { throw new NotImplementedException(); }

        public bool Guardar(BEReserva oBEReserva)
        {
            try
            {
                if (oBEReserva == null)
                    throw new Exception("Error: La Reserva no puede ser nula!");

                bool esNueva = oBEReserva.Id == 0;
                bool resultado = oMPPReserva.Guardar(oBEReserva);

                // Solo actualizar mesa si es una reserva nueva
                if (esNueva && resultado)
                {
                    BEMesa mesa = oMPPMesa.ListarObjeto(new BEMesa { Id = oBEReserva.IdMesa });
                    mesa.Reservar();
                    oMPPMesa.ActualizarEstado(mesa);
                }
                return resultado;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool Eliminar(BEReserva oBEReserva)
        {
            try
            {
                if (!oBEReserva.PuedeModificarOCancelar())
                    throw new Exception("Error: No se puede cancelar una reserva con menos de 3 horas de anticipación!");

                // Liberar la mesa
                BEMesa mesa = oMPPMesa.ListarObjeto(new BEMesa { Id = oBEReserva.IdMesa });
                mesa.Disponibilizar();
                oMPPMesa.ActualizarEstado(mesa);

                oBEReserva.Cancelar();
                return oMPPReserva.Guardar(oBEReserva);
            }
            catch (Exception ex) { throw ex; }
        }

        public bool Modificar(BEReserva oBEReserva)
        {
            try
            {
                if (!oBEReserva.PuedeModificarOCancelar())
                    throw new Exception("Error: No se puede modificar una reserva con menos de 3 horas de anticipación!");

                return oMPPReserva.Guardar(oBEReserva);
            }
            catch (Exception ex) { throw ex; }
        }

        public BEReserva ListarObjeto(BEReserva oBEReserva) { return oMPPReserva.ListarObjeto(oBEReserva); }
        public List<BEReserva> ListarTodo() { return oMPPReserva.ListarTodo(); }
        public List<BEReserva> ListarActivas() { return oMPPReserva.ListarActivas(); }
        #endregion
    }
}