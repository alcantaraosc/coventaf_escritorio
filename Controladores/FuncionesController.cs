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
    public class FuncionesController
    {
        private readonly ServiceFunciones _serviceFuncion;

        public FuncionesController() => _serviceFuncion = new ServiceFunciones();


        private ResponseModel respuestModel()
        {
            //1-Exito 0-si Exito
            return new ResponseModel() { Exito = 1 };
        }


        
        public async Task<ResponseModel> ListarFuncionesAsync()
        {
            var listrFunciones = new List<Funciones>();
            var responseModel = respuestModel();
            responseModel.Data = new Funciones();

            try
            {
                listrFunciones = await _serviceFuncion.ListarFuncionesAsync(responseModel);
                responseModel.Data = listrFunciones;
               
            }

            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;              
            }

            return responseModel;
        }

        //[HttpGet("ListarFuncionesAsync/{activo}")]
        public async Task<ResponseModel> ListarFuncionesAsync(bool activo)
        {

            var responseModel = respuestModel();
            responseModel.Data = new List<Funciones>();

            try
            {
                responseModel.Data = await _serviceFuncion.ListarFuncionesAsync(responseModel, activo);
            }

            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;
        }

        //ESTE METODO ES PARA PODER FILTRAR X NOMBRE_FUNCIONES EN EL  LISTADO
        //[HttpGet("ObtenerFuncionesPorNombre/{nombfuncion}")]
        public ResponseModel ObtenerFuncionesPorNombre(string nombfuncion)
        {
            var responseModel = respuestModel();
            responseModel.Data = new List<Funciones>();

            try
            {
                //obtener la consulta por Id de la funcion
                responseModel.Data = _serviceFuncion.ObtenerFuncionesPorNombre(nombfuncion, responseModel);

            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;
        }

        //[HttpPost("GuardarFuncionesAsync")]
        public ResponseModel GuardarFuncionesAsync(ViewModelSecurity dataFuncionesRoles)
        {
            var responseModel = respuestModel();
            int result = 0;
            try
            {
                //validar que el modelo este correcto antes de guardar en la base de datos
                if (_serviceFuncion.ModeloFuncionesEsValido(dataFuncionesRoles, responseModel))
                {
                    result = _serviceFuncion.InsertOrUpdateFunciones(dataFuncionesRoles, responseModel);
                }
            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }
            return responseModel;

        }

        //ActualizarFuncionesAsync
        //[HttpPut("ActualizarFuncionesAsync/{funcionID}")]
        public ResponseModel ActualizarFuncionesAsync(int funcionID, ViewModelSecurity dataFuncionesRoles)
        {
            var responseModel = respuestModel();
            int result = 0;

            try
            {
                //validar que el modelo este correcto antes de guardar cambios en la base de datos
                if (_serviceFuncion.ModeloFuncionesEsValido(dataFuncionesRoles, responseModel, funcionID))
                {
                    result = _serviceFuncion.InsertOrUpdateFunciones(dataFuncionesRoles, responseModel, funcionID);
                }

            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;
        }


       // [HttpGet("ObtenerFuncionesPorIdAsync/{funcionID}")]
        public async Task<ResponseModel> ObtenerFuncionesPorIdAsync(int funcionID)
        {
            var responseModel = respuestModel();
            responseModel.Data = new ViewModelSecurity();

            //obtener la consulta por funcionID
            try
            {
                //obtener la consulta por funcionID
                responseModel.Data = await _serviceFuncion.ObtenerFuncionesPorIdAsync(funcionID, responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;

        }

        // DELETE: api/security/funcionID
        //[HttpDelete("EliminarFuncionesAsync/{funcionID}")]
        public async Task<ResponseModel> EliminarFuncionesAsync(int funcionID)
        {
            var responseModel = respuestModel();
            try
            {
                await _serviceFuncion.EliminarFunciones(funcionID, responseModel);
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


