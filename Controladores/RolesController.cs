using Api.Model.Modelos;
using Api.Model.ViewModels;
using Api.Service.DataService;
using Api.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Controladores
{
    public class RolesController
    {
        private readonly ServiceRoles _serviceRol;

        public RolesController()
        {
            this._serviceRol = new ServiceRoles();
        }

        private ResponseModel respuestModel()
        {
            //1-exito 0-si exito
            return new ResponseModel() { Exito = 1 };
        }


        //[HttpGet("ListarRolesAsync")]
        public async Task<ResponseModel> ListarRolesAsync()
        {           
            var responseModel = respuestModel();
            responseModel.Data = new List<Roles>();

            try
            {
                responseModel.Data = await _serviceRol.ListarRolesAsync(responseModel);
               
            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;                
            }

            return responseModel;
        }

        //[HttpGet("ListarRolesAsync/{activo}")]
        public async Task<ResponseModel> ListarRolesAsync(bool activo)
        {

            var responseModel = respuestModel();
            responseModel.Data = new List<Roles>();

            try
            {
                responseModel.Data = await _serviceRol.ListarRolesAsync(activo, responseModel);
            }

            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;
        }

        //continue 10 11 2021 
        //[HttpPost("GuardarRolesAsync")]
        public ResponseModel GuardarRolesAsync(ViewModelSecurity dataFuncionesRoles)
        {
            var responseModel = respuestModel();
            int result = 0;

            try
            {
                //validar que el modelo este correcto antes de guardar en la base de datos
                if (_serviceRol.ModeloRolesEsValido(dataFuncionesRoles, responseModel))
                {
                    result = _serviceRol.InsertOrUpdateRoles(dataFuncionesRoles, responseModel);
                }
            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }
            return responseModel;

        }

        //ESTE METODO ES PARA PODER FILTRAR X NOMBRE DEL ROL EN EL LISTADO
        //[HttpGet("ObtenerRolPorNombre/{nombreRol}")]
        public ResponseModel ObtenerRolPorNombre(string nombreRol)
        {
            var responseModel = respuestModel();
            responseModel.Data = new List<Roles>();

            try
            {
                //obtener la consulta por Id del ROL
                responseModel.Data = _serviceRol.ObtenerRolPorNombre(nombreRol, responseModel);

            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;
        }

        //[HttpGet("ObtenerRolPorIdAsync/{rolID}")]
        public async Task<ResponseModel> ObtenerRolPorIdAsync(int rolID)
        {

            ResponseModel responseModel = respuestModel();
            responseModel.Data = new ViewModelSecurity();

            try
            {
                //obtener la consulta del ObtenerRolPorIdAsync
                responseModel.Data = await _serviceRol.ObtenerRolPorIdAsync(rolID, responseModel);

            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            //var userJson = JsonSerializer.Serialize(user, options);

            return responseModel;

        }



       // [HttpPut("ActualizarRolesAsync/{rolID}")]
        public ResponseModel ActualizarRolesAsync(int rolID, ViewModelSecurity dataFuncionesRoles)
        {
            var responseModel = respuestModel();
            int result = 0;
            try
            {
                //validar que el modelo este correcto antes de guardar en la base de datos
                if (_serviceRol.ModeloRolesEsValido(dataFuncionesRoles, responseModel, rolID))
                {
                    result = _serviceRol.InsertOrUpdateRoles(dataFuncionesRoles, responseModel, rolID);
                }
            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;
        }

        // DELETE: api/security/5
        //[HttpDelete("EliminarRolesAsync/{rolID}")]
        public async Task<ResponseModel> EliminarRolesAsync(int rolID)
        {
            var responseModel = respuestModel();
            try
            {
                await _serviceRol.EliminarRoles(rolID, responseModel);
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
