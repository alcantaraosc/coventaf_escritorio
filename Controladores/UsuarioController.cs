using Api.Model.Modelos;
using Api.Model.View;
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
    public class UsuarioController
    {

        //private readonly ISecurityUsuarios _serviceSecurityUsuario;
        private ServiceUsuario _serviceUsuario;

        public UsuarioController()
        {
            _serviceUsuario = new ServiceUsuario();
        }

        private ResponseModel respuestModel()
        {
            //1-Exito 0-si Exito
            return new ResponseModel() { Exito = 1 };
        }

        // GET: Security
        //[HttpGet("ListarUsuariosAsync")]
        public async Task<ResponseModel> ListarUsuariosAsync()
        {
            var responseModel = respuestModel();
            responseModel.Data = new List<Usuarios>();

            try
            {
                responseModel.Data = await _serviceUsuario.ListarUsuarios(responseModel);
            }

            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;

        }

        //[HttpPost("GuardarUsuarioAsync")]
        public async Task<ResponseModel> GuardarUsuarioAsync(ViewModelSecurity model)
        {
            var responseModel = respuestModel();
            
            try
            {
                //validar que el modelo este correcto antes de guardar en la base de datos
                if (_serviceUsuario.ModeloUsuarioEsValido(model, responseModel))
                {
                    await _serviceUsuario.InsertOrUpdateUsuario(model, responseModel);
                }
            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }
            return responseModel;
        }            
        
        public ResponseModel ObtenerDatosUsuarioPorFiltroX(string tipoConsulta, string busqueda)
        {           
            var responseModel = respuestModel();
            responseModel.Data = new List<ViewUsuarios>();
            
            try
            {
                //obtener la consulta por Id del tipo de usuario
                responseModel.Data = _serviceUsuario.ObtenerDatosUsuarioPorFiltroX(tipoConsulta, busqueda, responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;
        }

        //[HttpGet("ObtenerUsuarioPorIdAsync/{usuarioID}")]
        public async Task<ResponseModel> ObtenerUsuarioPorIdAsync(string usuarioID)
        {
            var responseModel = respuestModel();
            responseModel.Data = new ViewModelSecurity();

            //obtener la consulta por Id del usuario
            try
            {
                //obtener la consulta por Id del tipo de usuario
                responseModel.Data = await _serviceUsuario.ObtenerUsuarioPorIdAsync(usuarioID, responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;
        }

        //[HttpPut("ActualizarUsuarioAsync/{usuarioID}")]
        public async Task<ResponseModel> ActualizarUsuarioAsync(string usuarioID,  ViewModelSecurity model)
        {
            var responseModel = respuestModel();
            int result = 0;
            try
            {
                if (_serviceUsuario.ExisteDataOnTablaUsuario(usuarioID, responseModel))
                {
                    //validar que el modelo este correcto antes de guardar en la base de datos
                    if (_serviceUsuario.ModeloUsuarioEsValido(model, responseModel))
                    {
                        await _serviceUsuario.InsertOrUpdateUsuario(model, responseModel);
                    }
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
        //[HttpDelete("EliminarUsuarioAsync/{usuarioID}")]
        public async Task<ResponseModel> EliminarUsuarioAsync(string usuarioID)
        {
            var responseModel = respuestModel();

            try
            {
                await _serviceUsuario.EliminarUsuario(usuarioID, responseModel);
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
