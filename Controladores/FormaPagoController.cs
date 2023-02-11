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
    public class FormaPagoController
    {
        private readonly ServiceFormaPago _serviceFormaPago;

        public FormaPagoController()
        {
            _serviceFormaPago = new ServiceFormaPago();
        }


        //[HttpGet("ListarFormaPagoAsync")]
        public async Task<ResponseModel> ListarFormaPagoAsync()
        {
            var responseModel = new ResponseModel();
            responseModel.Data = new List<Forma_Pagos>();
            try
            {
                responseModel.Data = await _serviceFormaPago.ListarFormaDePago(responseModel);
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

