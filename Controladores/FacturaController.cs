using Api.Model.Modelos;
using Api.Model.ViewModels;
using Api.Service.DataService;
using Api.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladores
{
    public class FacturaController
    {
        //declarar la interfaz
        //private readonly IFactura _serviceFactura;
        //private readonly IBodega _dataBodega;
        //private readonly IFormaPago _dataFormaPago;
        //private readonly IMoneda_Hist _dataMonedaHist;

        private ServiceFactura _serviceFactura;
        private ServiceBodega _serviceBodega;
        private ServiceFormaPago _serviceFormaPago;
        private ServiceMoneda_Hist _serviceMonedaHist;

        public FacturaController()
        {
            this._serviceFactura = new ServiceFactura();
            this._serviceBodega = new ServiceBodega();
            this._serviceFormaPago = new ServiceFormaPago();
            this._serviceMonedaHist = new ServiceMoneda_Hist();
        }

        // GET: FacturaController
        //[HttpPost("ListarFacturas")]
        public async Task<ResponseModel> ListarFacturas(FiltroFactura filtroFactura)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.Data = new List<Facturas>();

            try
            {
                responseModel.Data = await _serviceFactura.ListarFacturasAsync(filtroFactura, responseModel);
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


       // [HttpGet("llenarComboxFacturaAsync")]
        public async Task<ListarDrownList> llenarComboxFacturaAsync()
        {
            var listarCombox = new ListarDrownList();
            //se refiere a la bodega. mal configurado en base de datos
            listarCombox.bodega = new List<Vendedores>();
            listarCombox.FormaPagos = new List<Forma_Pagos>();
            listarCombox.CondicionPago = new List<Condicion_Pagos>();
            listarCombox.NoFactura = "";
            var responseModel = new ResponseModel();

            try
            {
                var moneda_Hist = new Moneda_Hist();
               
                moneda_Hist = await _serviceMonedaHist.ObtenerTipoCambioDelDiaAsync(responseModel);

                //en caso que no encuentre el tipo de cambio del dia el sistema mandara una alerta
                 listarCombox.tipoDeCambio = (moneda_Hist is null ? 0.0000M : moneda_Hist.Monto);                
                //obtener la lista de bodegas que estan activas
                listarCombox.bodega = await _serviceBodega.ListarBodegasAsync(responseModel);
                listarCombox.FormaPagos = (List<Forma_Pagos>)await _serviceFormaPago.ListarFormaDePago(responseModel);
                responseModel = null;
                responseModel = new ResponseModel();
                listarCombox.NoFactura = await _serviceFactura.ObtenerNoFactura(responseModel, User.Usuario, User.Caja, User.ConsecCierreCT);
                listarCombox.TipoTarjeta = await _serviceFactura.ListarTipoTarjeta(responseModel);
                listarCombox.CondicionPago = await _serviceFactura.ListarCondicionPago(responseModel);
                listarCombox.Exito = 1;
                listarCombox.Mensaje = "Consulta Exitosa";

            }
            catch (Exception ex)
            {
                //-1 indica que existe algun error del servidor
                listarCombox.Exito = -1;
                listarCombox.Mensaje = ex.Message;
            }

            return listarCombox;
        }


        public async Task<ResponseModel> CancelarNoFacturaBloqueada(string noFactura)
        {
            ResponseModel responseModel = new ResponseModel();
            
            try
            {
                 await _serviceFactura.CancelarNoFacturaBloqueada(responseModel, noFactura);
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

       // [HttpDelete("EliminarArticuloDetalleFacturaAsync/{noFactura}/{articulo}")]
        public async Task<ResponseModel> EliminarArticuloDetalleFacturaAsync(string noFactura, string articulo)
        {
            var responseModel = new ResponseModel();
            responseModel.Data = new Facturando();
            try
            {
                responseModel.Data = await _serviceFactura.EliminarFacturaTemporal(responseModel, noFactura, articulo);
            }
            catch (Exception ex)
            {
                //-1 indica que existe algun error del servidor
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;
        }



        //[HttpGet("ObtenerNoFacturaAsync")]
        /*public async Task<ResponseModel> ObtenerNoFacturaAsync()
        {
            var responseModel = new ResponseModel();
            responseModel.Data = new object();

            try
            {
                responseModel.Data = await _serviceFactura.ObtenerNoFactura(responseModel);
            }
            catch (Exception ex)
            {
                //-1 indica que existe algun error del servidor
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;
        }*/





        //[HttpPost("GuardarDatosFacturaTemporal")]
        public async Task<ResponseModel> GuardarDatosFacturaTemporal(Facturando model)
        {
            var result = 0;
            ResponseModel responseModel = new ResponseModel();
            try
            {
                result = await _serviceFactura.InsertOrUpdateFacturaTemporal(model, responseModel);
                responseModel.Exito = 1;
            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;
        }


        //[HttpPost("GuardarFacturaAsync")]
        //public async Task<ActionResult<ResponseModel>> GuardarFacturaAsync(ViewModelFacturacion model)
        public async Task<ResponseModel> GuardarFacturaAsync(ViewModelFacturacion model)
        {
            var responseModel = new ResponseModel();

            int result = 0;
            try
            {
                //validar que el modelo este correcto antes de guardar en la base de datos
                if (_serviceFactura.ModeloUsuarioEsValido(model, responseModel))
                {
                    result = await _serviceFactura.InsertOrUpdateFactura(model, responseModel);
                }
            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }
            return responseModel;
        }
  
    }
}


