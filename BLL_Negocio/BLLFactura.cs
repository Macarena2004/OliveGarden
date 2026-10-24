using Abstraccion;
using BE;
using MPP;
using System;
using System.Collections.Generic;

namespace BLL_Negocio
{
    public class BLLFactura : IGestor<BEFactura>
    {
        MPPFactura oMPPFactura;
        MPPCliente oMPPCliente;

        #region "Constructor"
        public BLLFactura()
        {
            oMPPFactura = new MPPFactura();
            oMPPCliente = new MPPCliente();
        }
        #endregion

        #region "Métodos"
        public bool CrearXML() { throw new NotImplementedException(); }
        public long ObtenerUltimoId() { throw new NotImplementedException(); }
        public bool VerificarExistenciaObjeto(BEFactura obj) { throw new NotImplementedException(); }
        public bool Eliminar(BEFactura objeto) { throw new NotImplementedException(); }

        public bool Guardar(BEFactura oBEFactura) { return oMPPFactura.Guardar(oBEFactura); }
        public BEFactura ListarObjeto(BEFactura oBEFactura) { return oMPPFactura.ListarObjeto(oBEFactura); }
        public List<BEFactura> ListarTodo() { return oMPPFactura.ListarTodo(); }
        public List<BEFactura> ListarPorPeriodo(DateTime fi, DateTime ff) { return oMPPFactura.ListarPorPeriodo(fi, ff); }

        /// <summary>
        /// Calcula y aplica el descuento correspondiente según las reglas de negocio.
        /// Reglas: 5% miércoles/viernes | 10% cliente frecuente | 15% cumpleaños.
        /// Solo se aplica el mayor descuento, no se acumulan.
        /// </summary>
        public double AplicarDescuento(BEFactura oBEFactura, BECliente oBECliente)
        {
            try
            {
                if (oBEFactura == null || oBECliente == null)
                    throw new Exception("Error: La Factura o el Cliente no pueden ser nulos!");

                int cantidadFacturas = oMPPCliente.ContarFacturasDeCliente(oBECliente);
                double porcentaje = 0;

                // Regla 1: 15% si cumple años hoy (máxima prioridad)
                if (oBECliente.CumpleanosHoy())
                {
                    porcentaje = 15;
                }
                // Regla 2: 10% si es cliente frecuente (más de 8 facturas)
                else if (oBECliente.EsFrecuente(cantidadFacturas))
                {
                    porcentaje = 10;
                }
                // Regla 3: 5% si es miércoles o viernes
                else if (DateTime.Today.DayOfWeek == DayOfWeek.Wednesday ||
                         DateTime.Today.DayOfWeek == DayOfWeek.Friday)
                {
                    porcentaje = 5;
                }

                if (porcentaje > 0)
                {
                    oBEFactura.AplicarDescuento(porcentaje);
                }

                return porcentaje;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Genera la factura completa: calcula total, aplica descuento y guarda.
        /// </summary>
        public BEFactura GenerarFactura(long idMesa, long idCliente, List<BEDetallePedido> detalles)
        {
            try
            {
                BECliente cliente = oMPPCliente.ListarObjeto(new BECliente { Id = idCliente });
                if (cliente == null)
                    throw new Exception("Error: No se encontró el Cliente!");

                BEFactura factura = new BEFactura(0, idCliente, idMesa);
                factura.listaDetalles = detalles;
                factura.CalcularTotal();

                double porcentaje = AplicarDescuento(factura, cliente);

                oMPPFactura.Guardar(factura);
                return factura;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
    }
}
