using BE;
using BLL_Negocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OliveGarden_UI
{
    public partial class FormReservas : Form
    {
        #region "Atributos"
        private BEUsuario _usuarioLogueado;
        BLLReserva oBLLReserva = new BLLReserva();
        BLLMesa oBLLMesa = new BLLMesa();
        BLLCliente oBLLCliente = new BLLCliente();
        #endregion

        #region "Constructor"
        public FormReservas(BEUsuario pUsuario)
        {
            InitializeComponent();
            _usuarioLogueado = pUsuario;
        }
        #endregion

        #region "Carga"
        private void FormReservas_Load(object sender, EventArgs e)
        {
            CargarMesasDisponibles();
            CargarGrilla();
            dtpFecha.MinDate = DateTime.Today;
        }

        private void CargarMesasDisponibles()
        {
            string rutaReal = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "BaseDatos", "BD.Xml");
            MessageBox.Show("Leyendo desde: " + rutaReal + 
            "\n¿Existe? " + System.IO.File.Exists(rutaReal));
            try
            {
                List<BEMesa> mesas = oBLLMesa.ListarMesasDisponibles();

                if (mesas == null || mesas.Count == 0)
                {
                    cmbMesas.DataSource = null;
                    cmbMesas.Items.Clear();
                    return;
                }

                cmbMesas.DataSource = null;
                cmbMesas.DataSource = mesas;
                cmbMesas.DisplayMember = "NumeroMesa";
                cmbMesas.ValueMember = "Id";
                cmbMesas.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var test = oBLLMesa.ListarMesasDisponibles();
            MessageBox.Show($"Mesas disponibles encontradas: {test?.Count ?? 0}");
        }

        private void CargarGrilla()
        {
            try
            {
                dgvReservas.DataSource = null;
                dgvReservas.DataSource = oBLLReserva.ListarActivas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Eventos grilla"
        private void dgvReservas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var fila = dgvReservas.Rows[e.RowIndex];

                // Diagnóstico — borrar después
                string cols = "";
                foreach (DataGridViewColumn col in dgvReservas.Columns)
                    cols += col.Name + "=" + fila.Cells[col.Index].Value + " | ";
                MessageBox.Show(cols);

                // Buscar columna Id
                object idVal = null;
                if (dgvReservas.Columns.Contains("Id"))
                    idVal = fila.Cells["Id"].Value;
                else
                    idVal = fila.Cells[0].Value;

                if (idVal == null || idVal == DBNull.Value) return;
                lblId.Text = idVal.ToString();

                long idReserva = long.Parse(lblId.Text);
                BEReserva reserva = oBLLReserva.ListarObjeto(new BEReserva { Id = idReserva });
                if (reserva == null) return;

                dtpFecha.Value = reserva.Fecha;
                dtpHora.Value = DateTime.Today.Add(reserva.Hora);
                nudPersonas.Value = reserva.CantidadPersonas;

                if (reserva.IdCliente > 0)
                {
                    try
                    {
                        BECliente cliente = oBLLCliente.ListarObjeto(
                            new BECliente { Id = reserva.IdCliente });
                        if (cliente != null)
                        {
                            lblIdCliente.Text = cliente.Id.ToString();
                            txtNombreCliente.Text = cliente.Nombre;
                            txtApellidoCliente.Text = cliente.Apellido;
                            txtTelefonoCliente.Text = cliente.Telefono;
                            txtEmailCliente.Text = cliente.Email;
                            if (cliente.FechaNacimiento > DateTime.MinValue)
                                dtpFechaNacimiento.Value = cliente.FechaNacimiento;
                        }
                    }
                    catch { }
                }

                if (cmbMesas.Items.Count > 0)
                    cmbMesas.SelectedValue = reserva.IdMesa;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblId.Text = "";
            }
        }
        #endregion

        #region "Botones"
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarCampos();

                // Buscar o crear cliente
                BECliente cliente = BuscarOCrearCliente();

                // Crear reserva
                BEMesa mesaSeleccionada = (BEMesa)cmbMesas.SelectedItem;
                BEReserva reserva = new BEReserva(
                    0,
                    dtpFecha.Value.Date,
                    dtpHora.Value.TimeOfDay,
                    (int)nudPersonas.Value,
                    cliente.Id,
                    mesaSeleccionada.Id);

                oBLLReserva.Guardar(reserva);
                MessageBox.Show("Reserva registrada correctamente.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarGrilla();
                CargarMesasDisponibles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lblId.Text))
                    throw new Exception("Seleccioná una reserva de la grilla.");

                ValidarCampos();

                BEMesa mesaSeleccionada = (BEMesa)cmbMesas.SelectedItem;
                BEReserva reserva = new BEReserva
                {
                    Id = long.Parse(lblId.Text),
                    Fecha = dtpFecha.Value.Date,
                    Hora = dtpHora.Value.TimeOfDay,
                    CantidadPersonas = (int)nudPersonas.Value,
                    EstadoReserva = "Activa",
                    IdCliente = long.Parse(lblIdCliente.Text),
                    IdMesa = mesaSeleccionada.Id
                };

                oBLLReserva.Modificar(reserva);
                MessageBox.Show("Reserva modificada correctamente.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lblId.Text))
                    throw new Exception("Seleccioná una reserva de la grilla.");

                var confirmacion = MessageBox.Show(
                    "¿Estás segura de que querés cancelar esta reserva?",
                    "Cancelar reserva",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    BEReserva reserva = oBLLReserva.ListarObjeto(
                        new BEReserva { Id = long.Parse(lblId.Text) });
                    oBLLReserva.Eliminar(reserva);
                    MessageBox.Show("Reserva cancelada correctamente.", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    CargarGrilla();
                    CargarMesasDisponibles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        #endregion

        #region "Métodos"
        private void ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
                throw new Exception("Ingresá el nombre del cliente.");
            if (string.IsNullOrWhiteSpace(txtApellidoCliente.Text))
                throw new Exception("Ingresá el apellido del cliente.");
            if (string.IsNullOrWhiteSpace(txtTelefonoCliente.Text))
                throw new Exception("Ingresá el teléfono del cliente.");
            if (cmbMesas.SelectedItem == null)
                throw new Exception("No hay mesas disponibles para el horario seleccionado.");
        }

        private BECliente BuscarOCrearCliente()
        {
            // Buscar por email si existe
            if (!string.IsNullOrWhiteSpace(txtEmailCliente.Text))
            {
                List<BECliente> clientes = oBLLCliente.ListarTodo();
                BECliente existente = clientes.Find(c =>
                    c.Email.Trim().ToLower() == txtEmailCliente.Text.Trim().ToLower());
                if (existente != null) return existente;
            }

            // Crear nuevo cliente
            BECliente nuevoCliente = new BECliente(
                0,
                txtNombreCliente.Text.Trim(),
                txtApellidoCliente.Text.Trim(),
                txtTelefonoCliente.Text.Trim(),
                txtEmailCliente.Text.Trim(),
                dtpFechaNacimiento.Value.Date);

            oBLLCliente.Guardar(nuevoCliente);
            return nuevoCliente;
        }

        private void Limpiar()
        {
            lblId.Text = "";
            lblIdCliente.Text = "";
            txtNombreCliente.Text = "";
            txtApellidoCliente.Text = "";
            txtTelefonoCliente.Text = "";
            txtEmailCliente.Text = "";
            dtpFecha.Value = DateTime.Today;
            nudPersonas.Value = 1;
            dgvReservas.ClearSelection();
            CargarMesasDisponibles();
        }
        #endregion
    }
}
