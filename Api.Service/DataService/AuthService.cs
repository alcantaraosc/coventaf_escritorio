using Api.Context;
using Api.Helpers;
using Api.Model.Modelos;
using Api.Model.View;
using Api.Model.ViewModels;
using Api.Service.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Api.Service.DataService
{
    public class AuthService //: IAuthService
    {
        private readonly CoreDBContext _db;
        public AuthService()
        {
            this._db = new CoreDBContext();
            //this._db = db;
        }

        //public async Task<ViewModelUsuario> ValidateLogin(string username, string password, ResponseModel responseModel)
        //{

        //    //encryptar la constraseña
        //    var passwordCifrado =  new EncryptMD5().EncriptarMD5(password);
        //    var ViewModelUser = new ViewModelUsuario();

        //    try
        //    {
        //        using(var _db = new CoreDBContext())
        //        {
        //            //consultar el usuario
        //            // UserGrupo = _db.ViewUsuarioGrupo.Where(user => user.Usuario == username && user.ClaveCifrada == passwordCifrado).FirstOrDefault();
        //            var user = await _db.Usuarios.Where(user => user.Usuario == username && user.ClaveCifrada == passwordCifrado).FirstAsync();

        //            //comprobar si el usau
        //            if (user == null)
        //            {
        //                //cambiar el estado

        //                responseModel.SetFocus = true;
        //                //hacer una consulta para comprobar si el usuario existe
        //                user = _db.Usuarios.Where(u => u.Usuario == username).FirstOrDefault();
        //                if (user == null)
        //                {
        //                    responseModel.Mensaje = "El usuario no existe en la base de datos";
        //                    responseModel.NombreInput = "usuario";
        //                }
        //                else
        //                {
        //                    responseModel.Mensaje = "El Password es incorrecto!";
        //                    responseModel.NombreInput = "password";
        //                }
        //            }
        //            else
        //            {
        //                ViewModelUser.Usuario = user.Usuario;
        //                ViewModelUser.NombreUsuario = user.Nombre;
        //                //obtener el resto de informacion 
        //                bool result = await GetDatosLogIn(username, ViewModelUser);

        //                //if (!result)
        //                //{
        //                //    user = null;
        //                //    responseModel.Exito = 0;
        //                //    responseModel.Mensaje = "El usuario no pertenece a un grupo";
        //                //}

        //                responseModel.DataAux = new List<RolesUsuarioActual>();
        //                var roles = new List<RolesUsuarioActual>();

        //                var rolesUser = new List<RolesUsuarios>();
        //                rolesUser = _db.RolesUsuarios.Where(ruser => ruser.UsuarioID == username).ToList();
        //                int index = 0;
        //                foreach (var item in rolesUser)
        //                {
        //                    //asignar el nombre del rol                        
        //                    string nombreRol = new ServiceRoles().ObtenerSoloNombreRolPorId(item.RolID);
        //                    roles.Add(new RolesUsuarioActual() { RolID = item.RolID, NombreRol = nombreRol });

        //                    //incrementar
        //                    index = index + 1;
        //                }

        //                responseModel.DataAux = roles;

        //            }


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //    return ViewModelUser;         
        //}


        public async Task<ViewModelUsuario> LogearseIn(string username, string password, ResponseModel responseModel)
        {
            //encryptar la constraseña
            var passwordCifrado = new EncryptMD5().EncriptarMD5(password);
            var ViewModelUser = new ViewModelUsuario();                                 
            responseModel.DataAux = new List<RolesUsuarioActual>();
            var roles = new List<RolesUsuarioActual>();
            bool respuesta = false;
            
            try
            {
                using (SqlConnection cn = new SqlConnection(ADONET.strConnect))
                {
                    //Abrir la conección 
                    await cn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("SP_LogearseIn", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@Usuario", username);
                    cmd.Parameters.AddWithValue("@Clave", passwordCifrado);
                    
                    // Se añaden los parámetros de salida y se crean variables para facilitar su recuperacion
                    SqlParameter paramOutMensaje = cmd.Parameters.Add("@MensajeOutput", SqlDbType.VarChar, 100);
                    paramOutMensaje.Direction = ParameterDirection.Output;

                    SqlParameter paramOutPutMensajeReturned = cmd.Parameters.Add("@MensajeOutput", SqlDbType.VarChar, 100);
                    paramOutPutMensajeReturned.Direction = ParameterDirection.ReturnValue;

                    var dr = await cmd.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        respuesta = true;
                        ViewModelUser.Usuario = dr["Usuario"]?.ToString();
                        ViewModelUser.NombreUsuario = dr["NombreUsuario"]?.ToString();
                        ViewModelUser.Grupo = dr["TiendaID"]?.ToString();
                        ViewModelUser.NombreTienda = dr["NombreTienda"]?.ToString();
                        ViewModelUser.NivelPrecio = dr["Nivel_Precio"]?.ToString();
                        ViewModelUser.MonedaNivel = dr["Moneda_Nivel"]?.ToString();
                        ViewModelUser.DireccionTienda = dr["Direccion"]?.ToString();// is null ? null : dr["Direccion"].ToString();
                        ViewModelUser.DireccionTienda = dr["Telefono"]?.ToString();

                        roles.Add(new RolesUsuarioActual() { RolID = Convert.ToInt32(dr["RolID"]), NombreRol = dr["NombreRol"]?.ToString() });
                    }
                    
                    //si
                    if (respuesta  && ViewModelUser.Grupo.Length==0 && roles[0].NombreRol.ToString() != "ADMIN" )
                    {
                        respuesta = false;
                        ViewModelUser = null;
                        responseModel.Exito = 0;
                        responseModel.Mensaje = "El Cajero no tiene una tienda asignada";
                    }

                    else if (respuesta)
                    {
                        responseModel.DataAux = roles;
                        responseModel.Exito = 1;
                        responseModel.Mensaje = "Usuario Logeado";
                    }
                    else                    
                    {
                        ViewModelUser = null;
                        responseModel.Exito = 0;
                        responseModel.Mensaje = paramOutMensaje.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ViewModelUser;
        }
           
    
            
         
        

        public async Task<bool> GetDatosLogIn(string usuario, ViewModelUsuario ViewModelUser, ResponseModel responseModel)
        {
            bool resultExitoso = false;

            responseModel.DataAux = new List<RolesUsuarioActual>();
            var roles = new List<RolesUsuarioActual>();

         

            responseModel.DataAux = roles;



            try
            {
                using (SqlConnection cn = new SqlConnection(ADONET.strConnect))
                {
                    //Abrir la conección 
                    await cn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("SP_LogearseIn", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    //cmd.Parameters.AddWithValue("@Clave", );


                    // Se añaden los parámetros de salida y se crean variables para facilitar su recuperacion
                    SqlParameter paramOutMensaje = cmd.Parameters.Add("@MensajeOutput", SqlDbType.VarChar, 100);
                    paramOutMensaje.Direction = ParameterDirection.Output;

                    SqlParameter paramOutPutMensajeReturned = cmd.Parameters.Add("@MensajeOutput", SqlDbType.VarChar, 100);
                    paramOutPutMensajeReturned.Direction = ParameterDirection.ReturnValue;


                    var dr = await cmd.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        //resultado exitoso
                        resultExitoso = true;

                        ViewModelUser.Usuario = dr["Usuario"]?.ToString();
                        ViewModelUser.NombreUsuario = dr["NombreUsuario"]?.ToString();
                        ViewModelUser.Grupo = dr["TiendaID"]?.ToString();
                        ViewModelUser.NombreTienda = dr["NombreTienda"]?.ToString();
                        ViewModelUser.NivelPrecio = dr["Nivel_Precio"]?.ToString();
                        ViewModelUser.MonedaNivel = dr["Moneda_Nivel"]?.ToString();
                        ViewModelUser.DireccionTienda = dr["Direccion"]?.ToString();// is null ? null : dr["Direccion"].ToString();
                        ViewModelUser.DireccionTienda = dr["Telefono"]?.ToString();

                        roles.Add(new RolesUsuarioActual() { RolID = Convert.ToInt32(dr["RolID"]), NombreRol = dr["NombreRol"]?.ToString()});                                                            
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return resultExitoso;
        }

        public string GenerateToken(DateTime fechaActual, string username, TimeSpan tiempoValidez)
        {
            var fechaExpiracion = fechaActual.Add(tiempoValidez);
            //Configuramos las claims
            var claims = new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.Sub,username),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(fechaActual).ToUniversalTime().ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64
            ),
            new Claim("roles","Cliente"),
            new Claim("roles","Administrador"),
            };

            //Añadimos las credenciales
            var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("G3VF4C6KFV43JH6GKCDFGJH45V36JHGV3H4C6F3GJC63HG45GH6V345GHHJ4623FJL3HCVMO1P23PZ07W8")),
                    SecurityAlgorithms.HmacSha256Signature
            );//luego se debe configurar para obtener estos valores, así como el issuer y audience desde el appsetings.json

            //Configuracion del jwt token
            var jwt = new JwtSecurityToken(
                issuer: "Peticionario",
                audience: "Public",
                claims: claims,
                notBefore: fechaActual,
                expires: fechaExpiracion,
                signingCredentials: signingCredentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}