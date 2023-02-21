using Api.Model.Modelos;
using Api.Model.ViewModels;
using Api.Service.DataService;
using COVENTAF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVENTAF.PuntoVenta
{
    public partial class frmRetenciones : Form
    {
        public decimal montoTotal = 0.0000M;
        public List<DetalleRetenciones> _detalleRetenciones;
        private List<Retenciones> retenciones;

        ServiceFormaPago _serviceRetenciones;
        public frmRetenciones(List<DetalleRetenciones> detalleRetenciones)
        {
            InitializeComponent();
            _serviceRetenciones = new ServiceFormaPago();
            this._detalleRetenciones = detalleRetenciones;
        }

        private void frmRetenciones_Load(object sender, EventArgs e)
        {
            ListarRetenciones();
            if (dgvDetalleRetenciones.Rows.Count >0)
            {
                dgvDetalleRetenciones.DataSource = null;
                dgvDetalleRetenciones.DataSource = _detalleRetenciones;
            }

                       
        }

        private async void ListarRetenciones()
        {
            retenciones = new List<Retenciones>();
            retenciones = await _serviceRetenciones.ListarRetenciones();
            this.cboRetenciones.ValueMember = "Codigo_Retencion";
            this.cboRetenciones.DisplayMember = "Descripcion";
            this.cboRetenciones.DataSource = retenciones;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {    
            //si no existe la retencion el grid
            if (!existeRetencionenGrid(this.cboRetenciones.SelectedValue.ToString()))
            {
                var _datos = retenciones.Where(x => x.Codigo_Retencion == this.cboRetenciones.SelectedValue.ToString()).FirstOrDefault();
                // var count = detalleRetenciones.Count;

                var datosRetenciones = new DetalleRetenciones()
                {
                    Retencion = this.cboRetenciones.SelectedValue.ToString(),
                    Descripcion = this.cboRetenciones.Text,
                    Monto =Math.Round( montoTotal * (_datos.Porcentaje / 100), 2),
                    Base = montoTotal,
                    Referencia = "Ref -#", ///"Ref-#" + (detalleRetenciones.Count + 1).ToString(),
                    AutoRenedora = _datos.Es_AutoRetenedor == "S" ? true : false
                };

                _detalleRetenciones.Add(datosRetenciones);

                this.dgvDetalleRetenciones.DataSource = null;
                this.dgvDetalleRetenciones.DataSource = _detalleRetenciones;
            }
            else
            {
                MessageBox.Show("Ya aplicaste la retencion seleccionada", "Sistema COVENTAF");
            }


        }

        private bool existeRetencionenGrid(string codigoRetencion)
        {
            bool existe = false;
            var list = _detalleRetenciones.Where(x => x.Retencion == codigoRetencion).FirstOrDefault();
            if (list is not null)
            {
                existe = true;
            }

            return existe;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvDetalleRetenciones.RowCount > 0)
                {
                    int NumeroFilaSeleccionada = dgvDetalleRetenciones.CurrentRow.Index;
                    //if (MessageBox.Show("¿ Estas seguro de eliminar el articulo seleccionado ?", "Sistema COVENTAF", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //{
                        var articuloId = dgvDetalleRetenciones.Rows[NumeroFilaSeleccionada].Cells[0].Value.ToString();
                    //eliminar el registro de la lista.
                    _detalleRetenciones.RemoveAt(NumeroFilaSeleccionada);                    
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema COVENTAF");
            }
        }
    }
}
