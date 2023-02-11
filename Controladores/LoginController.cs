using Api.Model.Modelos;
using Api.Model.Request;
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
    public class LoginController
    {//public IActionResult Index()
     //{
     //    return View();
     //}

        
        private readonly AuthService _authService;        

        public LoginController()
        {
            this._authService = new AuthService();
        }


        //public ActionResult Token(UserLogin credenciales)
        //[HttpPost("Authenticate")]
        public async Task<ResponseModel> Authenticate(AuthRequest crendenciales)
        {
            ResponseModel responseModel = new ResponseModel();               
            //responseModel.Data= new usu
            try
            {
                //validar las credenciales del usuarios.
                var datosUsuario = await _authService.LogearseIn(crendenciales.Usuario, crendenciales.Password, responseModel);
                if (datosUsuario is not null)
                {
                    //obtener la fecha
                    var fechaActual = DateTime.UtcNow;
                    //tiempo valido para el token
                    var validez = TimeSpan.FromHours(5);
                    //fecha de expiracion del token
                    var fechaExpiracion = fechaActual.Add(validez);

                    //obtener el token del JWT
                    var token = _authService.GenerateToken(fechaActual, crendenciales.Usuario, validez);
                    responseModel.Exito = 1;
                    responseModel.Mensaje = "usuario logeado exitosamente";

                    User.Usuario = datosUsuario.Usuario;
                    User.NombreUsuario = datosUsuario.NombreUsuario;
                    User.TiendaID = datosUsuario.Grupo;
                    User.NombreTienda = datosUsuario.NombreTienda;
                    User.DireccionTienda = datosUsuario.DireccionTienda;
                    User.TelefonoTienda = datosUsuario.TelefonoTienda;
                    User.NivelPrecio = datosUsuario.NivelPrecio;
                    User.MonedaNivel = datosUsuario.MonedaNivel;
                    //bodega por defecto
                    User.BodegaID = datosUsuario.Bodega;
                    User.NombreBodega = datosUsuario.NombreBodega;
                    User.Caja = datosUsuario.Caja;                                 
                    User.Token = token;
                    User.expireAt = fechaExpiracion;

                    responseModel.Data = new
                    {
                        Usuario = datosUsuario.Usuario,
                        Nombre = datosUsuario.NombreUsuario,
                        Token = datosUsuario.Token,
                        NombreTienda = datosUsuario.NombreTienda,
                        DireccionTienda = datosUsuario.DireccionTienda,
                        Telefono = datosUsuario.TelefonoTienda,
                        token = token,
                        expireAt = fechaExpiracion,
                    };

                }
            }
            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
                responseModel.Data = null;
            }


            return responseModel;
        }
    }
}
