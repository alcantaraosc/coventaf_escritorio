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
        public bool aplicarRetenciones=false;
        public decimal totalRetenciones = 0.0000M;

        public decimal montoTotal = 0.0000M;
        public List<DetalleRetenciones> _detalleRetenciones = new List<DetalleRetenciones>();
        
        private List<Retenciones> listaRetenciones;
                       

        ServiceFormaPago _serviceRetenciones;
        public frmRetenciones()
        {
            InitializeComponent();
            _serviceRetenciones = new ServiceFormaPago();
           
        }

        private void frmRetenciones_Load(object sender, EventArgs e)
        {
            ListarRetenciones();

            if (_detalleRetenciones.Count >0)
            {
                /*dgvDetalleRetenciones.DataSource = null;
                dgvDetalleRetenciones.DataSource = _detalleRetenciones;*/
                CargarRetencionesGrid();
                CalcularRetencion();
            }

                       
        }

        private async void ListarRetenciones()
        {
            listaRetenciones = new List<Retenciones>();
            listaRetenciones = await _serviceRetenciones.ListarRetenciones();
            this.cboRetenciones.ValueMember = "Codigo_Retencion";
            this.cboRetenciones.DisplayMember = "Descripcion";
            this.cboRetenciones.DataSource = listaRetenciones;
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
                //consultar la retencion seleccionada
                var _datos = listaRetenciones.Where(x => x.Codigo_Retencion == this.cboRetenciones.SelectedValue.ToString()).FirstOrDefault();
                var longitudGrid = dgvDetalleRetenciones.RowCount;
                //agregar un tipo de retencion al grid
                this.dgvDetalleRetenciones.Rows.Add( this.cboRetenciones.SelectedValue.ToString(), this.cboRetenciones.Text, Math.Round(montoTotal * (_datos.Porcentaje / 100), 2),
                                                    montoTotal, $"RET-#{longitudGrid+1}", (_datos.Es_AutoRetenedor =="S" ? true : false));
                 //calcular las retanciones  
                CalcularRetencion();
            }
            else
            {
                MessageBox.Show("Ya aplicaste la retencion seleccionada", "Sistema COVENTAF");
            }

        }

        //cargar las retenciones al iniciar el form.
        private void CargarRetencionesGrid()
        {
            foreach(var item in _detalleRetenciones)
            {
                dgvDetalleRetenciones.Rows.Add(item.Retencion, item.Descripcion, item.Monto, item.Base, item.Referencia, item.AutoRetenedora );
            }
  
        }

        private void CalcularRetencion()
        {
            //Reiniciar los valores
            totalRetenciones = 0.00M;

            for( var rows=0; rows < dgvDetalleRetenciones.RowCount; rows ++)
            {
                //sumar los montos(celda 2 = montos)
                totalRetenciones += Convert.ToDecimal(dgvDetalleRetenciones.Rows[rows].Cells["Monto"].Value);
               // marBO.Marca = dtg_Marca.Rows[filaSeleccionada].Cells["nombre_marca"].Value.ToString();
            }

            this.lblTotalRetenciones.Text = $"Total de Retenciones: C$ {totalRetenciones.ToString("N2")}";
        }

        private bool existeRetencionenGrid(string codigoRetencion)
        {
            bool existe = false;
            for(var rows=0; rows < dgvDetalleRetenciones.RowCount; rows ++)
            {
                //comprobar si existe el codigo de la retencion
                if (dgvDetalleRetenciones.Rows[rows].Cells["Retencion"].Value.ToString() == codigoRetencion)
                {
                    existe = true;
                    break;
                }
            }


            /*var list = _detalleRetenciones.Where(x => x.Retencion == codigoRetencion).FirstOrDefault();
            if (list is not null)
            {
                existe = true;
            }*/

            return existe;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvDetalleRetenciones.RowCount > 0)
                {
                    //seleccionar la fila
                    int fila = dgvDetalleRetenciones.CurrentRow.Index;
                    //eliminar la fila seleccionada
                    dgvDetalleRetenciones.Rows.RemoveAt(fila);                   
                    //recalcular la retencion
                    CalcularRetencion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema COVENTAF");
            }
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            //recalcular la retencion
            CalcularRetencion();
            //limpiar todo el registro
            _detalleRetenciones = null;
            _detalleRetenciones = new List<DetalleRetenciones>();

            for(var rows=0; rows < dgvDetalleRetenciones.RowCount; rows ++)
            {
                var datosRetenciones = new DetalleRetenciones()
                {
                    Retencion = this.dgvDetalleRetenciones.Rows[rows].Cells["Retencion"].Value.ToString(),
                    Descripcion = this.dgvDetalleRetenciones.Rows[rows].Cells["Descripcion"].Value.ToString(),
                    Monto = Convert.ToDecimal(this.dgvDetalleRetenciones.Rows[rows].Cells["Monto"].Value),
                    Base = Convert.ToDecimal(this.dgvDetalleRetenciones.Rows[rows].Cells["Base"].Value),                    
                    Referencia = this.dgvDetalleRetenciones.Rows[rows].Cells["Referencia"].Value.ToString(),
                    AutoRetenedora = Convert.ToBoolean(this.dgvDetalleRetenciones.Rows[rows].Cells["AutoRetenedora"].Value)
                };
                //agregar nuevo registro de las retenciones
                _detalleRetenciones.Add(datosRetenciones);
            }

               

            
            
            //var frmPedirAutorizacion = new frmAutorizacion();
            //this.Hide();
            //if (frmPedirAutorizacion.DialogResult == DialogResult.OK)
            //{
            aplicarRetenciones = true;
            this.Close();
            //}
                
        }

       
    }
}
