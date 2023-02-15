using Api.Model.View;
using Api.Model.ViewModels;
using Api.Service.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladores
{
    public class CajaPosController
    {

        private readonly ServiceCaja_Pos _serviceCaja_Pos;
           


        public CajaPosController()
        {
            /* this._serviceFactura = new FacturaService();
             this._dataBodega = new DataBodega();
             this._dataFormaPago = new DataFormaPago();
             this._dataMonedaHist = new DataMoneda_Hist();*/
            _serviceCaja_Pos = new ServiceCaja_Pos();
        }

        public async Task<ResponseModel> ListarCajasDisponible(string cajero, string sucursalID)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.Data = new List<ViewCajaDisponible>();

            try
            {
                var _serviceCajaPos = new ServiceCaja_Pos();
                responseModel.Data =await _serviceCajaPos.ListarCajasDisponibles(cajero, sucursalID, responseModel);// _serviceFactura.ListarFacturasAsync(filtroFactura, responseModel);                                
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

        public async Task<ResponseModel> GuardarAperturaCaja(string caja, string cajero, string sucursalID, decimal montoApertura)
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel.Data = new List<string>();

            try
            {
                if (_serviceCaja_Pos.ModeloAperturaCajaEsValido(caja, cajero, responseModel))
                {
                    responseModel.Data = await _serviceCaja_Pos.GuardarAperturaCaja(caja, cajero, sucursalID, montoApertura, responseModel);
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

        public ResponseModel VerificarsiExisteAperturaCaja(string cajero, string sucursalID)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                 _serviceCaja_Pos.VerificarExistenciaAperturaCaja(cajero, sucursalID, responseModel);                
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


       

    }
}
