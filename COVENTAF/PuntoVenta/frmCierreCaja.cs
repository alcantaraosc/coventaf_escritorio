using Api.Model.Modelos;
using Api.Model.ViewModels;
using Api.Service.DataService;
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
    public partial class frmCierreCaja : Form
    {
        private bool existeEfectivoDolar = false;
        private bool existeEfectivoCordoba = false;

        public bool exitoCierreCaja = false;
        List<Denominacion> denominacion = new List<Denominacion>();
        ServiceCaja_Pos _serviceCajaPos;
        List<ViewModelCierreCaja> _datosCierreCaja;
        public frmCierreCaja()
        {
            InitializeComponent();
            _serviceCajaPos = new ServiceCaja_Pos();
        }

        private void frmCierreCaja_Load(object sender, EventArgs e)
        {
            try
            {
                //
                PrepararCajaParaCierre(User.Caja, User.Usuario, User.ConsecCierreCT);
            }
            catch (Exception ex)
            {

            }

            this.lblTitulo.Text = $"Cierre de Caja: {User.Caja}";
            this.lblTituloCaja.Text = $"Cierre de Caja {User.Caja}. con el Numero de Cierre: {User.ConsecCierreCT}";
        }



        public async void PrepararCajaParaCierre(string caja, string cajero, string numeroCierre)
        {
            ResponseModel responseModel = new ResponseModel();
            _datosCierreCaja = new List<ViewModelCierreCaja>();

            try
            {
                _datosCierreCaja = await _serviceCajaPos.ObtenerDatosParaCierreCaja(caja, cajero, numeroCierre, responseModel);
                if (responseModel.Exito == 1)
                {
                    //llenar el grid reportado por el sistema
                    LlenarGridReportadoXSistema(_datosCierreCaja);
                    LlenarGridReportadoCajero(_datosCierreCaja);
                    CalcularTotalReportadoCajero();
                    ListarDenomincaciones();
                }
                else
                {
                    MessageBox.Show(responseModel.Mensaje, "Sistema COVENTAF");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LlenarGridReportadoXSistema(List<ViewModelCierreCaja> _datosCierreCaja)
        {


            decimal totalCordobas = 0.00M;
            decimal totalDolares = 0.00M;
            foreach (var itemSistema in _datosCierreCaja)
            {
               
                this.dgvGridReportadoPorSistema.Rows.Add(itemSistema.Descripcion, (itemSistema.Moneda == "L" ? $"C$ {itemSistema.Monto.ToString("N2")}" : $"U$ {itemSistema.Monto.ToString("N2")}"), itemSistema.Moneda);
                //comprobar si la moneda es Local =L (C$)
                if (itemSistema.Moneda == "L")
                {
                    totalCordobas += itemSistema.Monto;
                }
                else if (itemSistema.Moneda == "D")
                {
                    totalDolares += itemSistema.Monto;
                }

                //comprobar si existe forma de pago 0001=Efectivo cordoba
                if (itemSistema.Forma_Pago == "0001" && itemSistema.Moneda == "L")
                {
                    existeEfectivoCordoba = true;
                }
                //comprobar si existe forma de pago 0001=Efectivo dolar
                if (itemSistema.Forma_Pago == "0001" && itemSistema.Moneda == "D")
                {
                    existeEfectivoDolar = true;
                }

            }
           
            this.txtTotalCordobasSistema.Text = totalCordobas.ToString("N2");
            this.txtTotalDolaresSistema.Text = totalDolares.ToString("N2");

        }

        private void LlenarGridReportadoCajero(List<ViewModelCierreCaja> _datosCierreCaja)
        {
            foreach (var itemSistema in _datosCierreCaja)
            {
                this.dgvGridRportadoXCajero.Rows.Add(itemSistema.Descripcion, (itemSistema.Moneda == "L" ? "C$ 0.00" : "U$ 0.00"), itemSistema.Moneda);
            }
        }

        void CalcularTotalReportadoCajero()
        {
            decimal totalCordobas = 0.00M;
            decimal totalDolares = 0.00M;

            for (var rows = 0; rows < dgvGridRportadoXCajero.RowCount; rows++)
            {
                //comprobar si la moneda es Local =L (C$)
                if (dgvGridRportadoXCajero.Rows[rows].Cells["datRepCajeroMoneda"].Value.ToString() == "L")
                {

                    totalCordobas += Convert.ToDecimal(dgvGridRportadoXCajero.Rows[rows].Cells["datRepCajeroMonto"].Value.ToString().Replace("C$", ""));
                }
                else if (dgvGridRportadoXCajero.Rows[rows].Cells["datRepCajeroMoneda"].Value.ToString() == "D")
                {
                    totalDolares += Convert.ToDecimal(dgvGridRportadoXCajero.Rows[rows].Cells["datRepCajeroMonto"].Value.ToString().Replace("U$", ""));
                }
            }

            this.txtTotalCordobasCajero.Text = totalCordobas.ToString("N2");
            this.txtTotalDolaresCajero.Text = totalDolares.ToString("N2");
        }

        private async void ListarDenomincaciones()
        {
            ResponseModel responseModel = new ResponseModel();

            try
            {
                denominacion = await _serviceCajaPos.ObtenerListaDenominacion(responseModel);
                if (responseModel.Exito ==1)
                {
                    LlenarGridDenominaciones();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private void btnCierre_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void  LlenarGridDenominaciones()
        {
            try
            {
                //comprobar si existe el efectivo en cordobas
                if (existeEfectivoCordoba)
                {
                    foreach (var itemDenominacion in denominacion)
                    {
                        this.dgvReportePagoCajero.Rows.Add(Descripcion, itemDenominacion.Tipo, itemDenominacion.Denom_Monto, 0, "L");
                    }
                }

                //comprobar si existe el efectivo en cordobas
                if (existeEfectivoDolar)
                {
                    foreach (var itemDenominacion in denominacion)
                    {
                        this.dgvReportePagoCajero.Rows.Add("TARJETA (DOLAR)", itemDenominacion.Tipo, itemDenominacion.Denom_Monto, 0, "D");
                    }
                }

                foreach(var itemDenomincacion in _datosCierreCaja)
                {
                    if (itemDenomincacion.Forma_Pago != "0001")
                    {
                        this.dgvReportePagoCajero.Rows.Add(itemDenomincacion.Forma_Pago, itemDenomincacion.Descripcion, "0", itemDenomincacion.Moneda);
                    }
                }
                
            }
            catch (Exception ex)
            {

            }

        }
    }

}