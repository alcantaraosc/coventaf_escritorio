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
        public bool exitoCierreCaja = false;
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
                MostrarDenomincaciones();
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
                }
            }
            catch (Exception ex)
            {
                //0 para indicar que existe algun error en la consulta 
                responseModel.Exito = -1;
                //indicar el mensaje del error
                responseModel.Mensaje = ex.Message;
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
                if (dgvGridRportadoXCajero.Rows[rows].Cells["Moneda"].Value.ToString() == "L")
                {

                    totalCordobas += Convert.ToDecimal(dgvGridRportadoXCajero.Rows[rows].Cells["Monto"].Value.ToString().Replace("C$", ""));
                }
                else if (dgvGridRportadoXCajero.Rows[rows].Cells["Moneda"].Value.ToString() == "D")
                {
                    totalDolares += Convert.ToDecimal(dgvGridRportadoXCajero.Rows[rows].Cells["Monto"].Value.ToString().Replace("U$", ""));
                }
            }

            this.txtTotalCordobasCajero.Text = totalCordobas.ToString("N2");
            this.txtTotalDolaresCajero.Text = totalDolares.ToString("N2");
        }

        void MostrarDenomincaciones()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }


        private void btnCierre_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}