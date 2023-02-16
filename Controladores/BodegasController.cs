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
    public class BodegasController
    {
        private readonly ServiceBodega _serviceBodega;

        //aplicando inyeccion 
        public BodegasController() => _serviceBodega = new ServiceBodega();

        //[HttpGet("ListarBodegasAsync")]
        public async Task<ResponseModel> ListarBodegasAsync(string sucursalID)
        {
            var responseModel = new ResponseModel();
            responseModel.Data = new List<Bodegas>();
            try
            {
                //obtener la lista de bodegas que estan activas
                responseModel.Data = await _serviceBodega.ListarBodegasAsync(sucursalID, responseModel);
            }
            catch (Exception ex)
            {
                //-1 indica que existe algun error del servidor
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;
        }


    
    }
}
