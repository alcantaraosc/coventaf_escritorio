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
        public bool exitoCierreCaja=false;
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
               var x =  ListarCajaParaCierre(User.Caja, User.Usuario,  User.ConsecCierreCT);
            }
            catch (Exception ex)
            {

            }
            this.lblTitulo.Text = $"Cierre de Caja: {User.Caja}";
            this.lblTituloCaja.Text = $"Cierre de Caja {User.Caja}. con el Numero de Cierre: {User.ConsecCierreCT}";
        }

     

        public async Task<ResponseModel> ListarCajaParaCierre(string caja, string cajero, string numeroCierre)
        {
            ResponseModel responseModel = new ResponseModel();
            _datosCierreCaja = new List<ViewModelCierreCaja>();

            try
            {
                _datosCierreCaja = await _serviceCajaPos.ObtenerDatosParaCierreCaja(caja, cajero, numeroCierre, responseModel);
                if (responseModel.Exito == 1)
                {
                   
                }
            }
            catch (Exception ex)
            {
                //0 para indicar que existe algun error en la consulta 
                responseModel.Exito = -1;
                //indicar el mensaje del error
                responseModel.Mensaje = ex.Message;
            }
            return responseModel;
        }

        private void btnCierre_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
