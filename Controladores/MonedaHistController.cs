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
    public class MonedaHistController
    {
        private readonly ServiceMoneda_Hist _serviceMoneda_Hist;

        public MonedaHistController()
        {
            this._serviceMoneda_Hist = new ServiceMoneda_Hist();
        }


        //[HttpGet("ObtenerTipoCambioDelDiaAsync")]
        public async Task<ResponseModel> ObtenerTipoCambioDelDiaAsync()
        {
            var responseModel = new ResponseModel();
            responseModel.Data = new Moneda_Hist();
            try
            {
                responseModel.Data = await _serviceMoneda_Hist.ObtenerTipoCambioDelDiaAsync(responseModel);
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
