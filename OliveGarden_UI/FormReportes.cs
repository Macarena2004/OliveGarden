using BE;
using BLL_Negocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OliveGarden_UI
{
    public partial class FormReportes : Form
    {
        #region "Atributos"
        private BEUsuario _usuarioLogueado;
        BLLFactura oBLLFactura = new BLLFactura();
        #endregion

        #region "Constructor"
        public FormReportes(BEUsuario pUsuario)
        {
            InitializeComponent();
            _usuarioLogueado = pUsuario;
        }
        #endregion

        #region "Carga"
        private void FormReportes_Load(object sender, EventArgs e)
        {
            dtpInicio.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpFin.Value = DateTime.Today;
        }
        #endregion

        #region "Botones"
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpInicio.Value > dtpFin.Value)
                    throw new Exception("La fecha de inicio no puede ser mayor a la fecha de fin.");

                List<BEFactura> facturas = oBLLFactura.ListarPorPeriodo(
                    dtpInicio.Value.Date,
                    dtpFin.Value.Date);

                dgvFacturas.DataSource = null;
                dgvFacturas.DataSource = facturas;

                // Calcular totales
                double totalFacturado = 0;
                int cantidadPagadas = 0;

                foreach (var f in facturas)
                {
                    totalFacturado += f.TotalFactura;
                    if (f.EstadoFactura == "Pagada") cantidadPagadas++;
                }

                double promedio = facturas.Count > 0
                    ? totalFacturado / facturas.Count : 0;

                lblTotalFacturado.Text = $"Total facturado: ${totalFacturado:F2}";
                lblCantidadFacturas.Text = $"Facturas: {facturas.Count} total | {cantidadPagadas} pagadas";
                lblPromedioTicket.Text = $"Ticket promedio: ${promedio:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
