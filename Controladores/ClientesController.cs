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
    public class ClientesController
    {
        //private readonly ICliente _dataCliente;
        private readonly ServiceCliente _serviceCliente;

        //aplicando inyeccion 
        public ClientesController()
        {
            this._serviceCliente = new ServiceCliente();
        }


        //[HttpGet("ObtenerClientePorIdAsync/{clienteID}")]
        public async Task<ResponseModel> ObtenerClientePorIdAsync(string clienteID)
        {
            var responseModel = new ResponseModel();
            responseModel.Data = new Clientes();
            try
            {
                //llamar al metodo ObtenerClientePorIdAsync para obtener el registro del cliente
                responseModel.Data = await _serviceCliente.ObtenerClientePorIdAsync(clienteID, responseModel);
            }
            catch (Exception ex)
            {
                //-1 significa que existe un error
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;
        }


    }
}
