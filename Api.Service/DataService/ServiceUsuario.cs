using Api.Context;
using Api.Helpers;
using Api.Model.Modelos;
using Api.Model.View;
using Api.Model.ViewModels;
using Api.Service.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Service.DataService
{
    public class ServiceUsuario//: ISecurityUsuarios
    {
        //private CoreDBContext _db;

        public ServiceUsuario()
        {
           // _db = new CoreDBContext();
        }

        /// <summary>
        /// Listar los usuarios existentes
        /// </summary>
        /// <returns></returns>
        public async Task<List<ViewUsuarios>> ListarUsuarios(ResponseModel responseModel)
        {
            var listaUsuario = new List<ViewUsuarios>();
            try
            {
                using(var _db = new CoreDBContext())
                {
                    //Lista los usuarios en orden ascendentes por nombres
                    listaUsuario = await _db.ViewUsuarios.OrderBy(user => user.NombreUsuario).ToListAsync();
                }
                

                //Lista los usuarios en orden ascendentes por nombres
                //    var listUser = await _db.Usuarios.Where(user => user.Activo == activo).
                //        Select(user => new { Usuario = user.Usuario, Nombre = user.Nombre, Activo = user.Activo, Grupo = user.Grupo }).
                //        OrderBy(user => user.Nombre).ToListAsync();
                //    listaUsuario = listUser.Select(user => new { Usuario = user.Usuario, Nombre = user.Nombre, Activo = user.Activo, Grupo = user.Grupo }) as List<ListUser>;

                if (listaUsuario.Count > 0)
                {
                    responseModel.Exito = 1;
                    responseModel.Mensaje = "consulta exitosa";
                }
                else
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje = "No se pudo realizar la consulta";
                }
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return listaUsuario;
        }


        /// <summary>
        /// listar los usuarios activos y desactivados
        /// </summary>
        /// <param name="activo"></param>
        /// <returns></returns>
        public async Task<List<Usuarios>> ListarUsuarios(string activo = "S")
        {
            List<Usuarios> listaUsuario = new List<Usuarios>();

            try
            {
                using (var _db = new CoreDBContext())
                {
                    //Lista los usuarios en orden ascendentes por nombres
                    listaUsuario = await _db.Usuarios.Where(user => user.Activo == activo).OrderBy(user => user.Nombre).ToListAsync();                    
                }
              
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return listaUsuario;
        }


        /// <summary> 
        /// validar si existe el login del usuario
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ExisteLogin(string loginUser)
        {
            bool existeRegistro;
            try
            {
                using (var _db = new CoreDBContext())
                {
                    existeRegistro = _db.Usuarios.Where(user => user.Usuario.Trim() == loginUser).Count() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }

            return existeRegistro;
        }


        /// <summary> 
        /// Obtener solo el login del usuario filtrado por el UsuarioID
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        public string ObtenerSoloLoginUserPorId(string usuarioID)
        {
            string loginUser;
            try
            {
                using (var _db = new CoreDBContext())
                {
                    loginUser = (from usr in _db.Usuarios
                             where usr.Usuario == usuarioID
                             select usr.Usuario).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return loginUser;
        }

        /// <summary>
        /// ESTE METODO ES PARA PODER FILTRAR X NOMBRE DEL usuairo EN EL LISTADO
        /// </summary>
        /// <param name="value"></param>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        public List<ViewUsuarios> ObtenerDatosUsuarioPorFiltroX(string tipoConsulta, string busqueda, ResponseModel responseModel)
        {
            var listUser = new List<ViewUsuarios>();

            try
            {
                switch (tipoConsulta)
                {
                    case "Usuario":
                        using (var _db = new CoreDBContext())
                        {
                            listUser = _db.ViewUsuarios.Where(user => user.Usuario.Contains(busqueda)).ToList();
                        }
                        break;

                
                    case "Nombre":
                        using (var _db = new CoreDBContext())
                        {
                            listUser = _db.ViewUsuarios.Where(user => user.NombreUsuario.Contains(busqueda)).ToList();
                        }

                   break;
                }

                if (listUser.Count == 0)
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje = "No se encontraron datos del usuario";

                }
                else
                {
                    responseModel.Exito = 1;
                    responseModel.Mensaje = "<< Usuario encontrado >>";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listUser;
        }
 
        /// <summary>
        /// Guardar o actualizar los datos del usuario
        /// </summary>
        /// <param name="model"></param>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        public async Task  InsertOrUpdateUsuario(ViewModelSecurity model, ResponseModel responseModel)
        {
            int result = 0;

            string ConvertirArrayString = new Utilidades().ConvertirEnCadenatring(model.RolesUsuarios, "RolesUsuarios", "FuncionID");
            try
            {
                using (SqlConnection cn = new SqlConnection(ADONET.strConnect))
                {
                    using (SqlCommand cmd = new SqlCommand(@"SP_InsertOrUpdateUsuario", cn))
                    {
                        //Aquí agregas los parámetros de tu procedimiento
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@NuevoUsuario", model.Usuarios.NuevoUsuario);
                        cmd.Parameters.AddWithValue("@Usuario", model.Usuarios.Usuario);
                        cmd.Parameters.AddWithValue("@Nombre", model.Usuarios.Nombre);
                        cmd.Parameters.AddWithValue("@Tipo", model.Usuarios.Tipo);
                        cmd.Parameters.AddWithValue("@Activo", model.Usuarios.Activo);
                        cmd.Parameters.AddWithValue("@Req_Cambio_Clave", model.Usuarios.Req_Cambio_Clave);
                        cmd.Parameters.AddWithValue("@Frecuencia_Clave", model.Usuarios.Frecuencia_Clave);
                        cmd.Parameters.AddWithValue("@Max_Intentos_Conex", model.Usuarios.Max_Intentos_Conex);
                        cmd.Parameters.AddWithValue("@Clave", model.Usuarios.Clave);
                        cmd.Parameters.AddWithValue("@Correo_Electronico", model.Usuarios.Correo_Electronico);
                        cmd.Parameters.AddWithValue("@Tipo_Acceso", model.Usuarios.Tipo_Acceso);                        
                        cmd.Parameters.AddWithValue("@NoteExistsFlag", model.Usuarios.NoteExistsFlag);
                        cmd.Parameters.AddWithValue("@RowPointer", model.Usuarios.RowPointer);
                        cmd.Parameters.AddWithValue("@CreatedBy", model.Usuarios.CreatedBy);
                        cmd.Parameters.AddWithValue("@UpdatedBy", model.Usuarios.UpdatedBy);
                        cmd.Parameters.AddWithValue("@ClaveCifrada", model.Usuarios.ClaveCifrada);
                        cmd.Parameters.AddWithValue("@Grupo", model.Usuarios.Grupo);
                        cmd.Parameters.AddWithValue("@ARRAY", ConvertirArrayString);

                        //Abres la conexión 
                        await cn.OpenAsync();
                        //Ejecutas el procedimiento, y guardas en una variable tipo int el número de lineas afectadas en las tablas que se insertaron
                        //(ExecuteNonQuery devuelve un valor entero, en éste caso, devolverá el número de filas afectadas después del insert, si es mayor a > 0, entonces el insert se hizo con éxito)
                        result = await cmd.ExecuteNonQueryAsync();

                        if (result > 0)
                        {
                            responseModel.Mensaje = (model.Usuarios.NuevoUsuario) ? "Los datos se ha guardado correctamente" : "Se ha actualizado correctamente";
                            responseModel.Exito = 1;
                        }
                        else
                        {
                            responseModel.Mensaje = (model.Usuarios.NuevoUsuario) ? "No se pueden guardar los datos" : "No se puede actualizar los datos";
                            responseModel.Exito = 0;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }         
        }
        public async Task<(bool, string)> DeleteBookAsync(Usuarios user)
        {
            try
            {
                using (var _db = new CoreDBContext())
                {
                    var dbBook = await _db.Usuarios.FindAsync(user.Usuario);

                    if (dbBook == null)
                    {
                        return (false, "Book could not be found.");
                    }

                    _db.Usuarios.Remove(user);
                    await _db.SaveChangesAsync();
                }

                return (true, "Book got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtener el usuario por ID para la editación
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        public async Task<ViewModelSecurity> ObtenerUsuarioPorIdAsync(string usuarioID, ResponseModel responseModel)
        {
            Usuarios usuario = new Usuarios(); //crea una instancia de Usuario llamada usuario el objeto es Usuario

            ViewModelSecurity viewModelSecurity = new ViewModelSecurity();
            viewModelSecurity.Usuarios = new Usuarios();
            viewModelSecurity.RolesUsuarios = new List<RolesUsuarios>();

            try
            {
                using (var _db = new CoreDBContext())
                {
                    viewModelSecurity.Usuarios = await _db.Usuarios.Include(rol => rol.RolesUsuarios).Where(usr => usr.Usuario == usuarioID).FirstOrDefaultAsync();
                }


                //verificar que tenga registro la consulta
                if (viewModelSecurity.Usuarios != null)
                {                   
                    //verificar si la clave cifra es null o si esta vacia entonces asignar null, de lo contrario desencriptar la cadena 
                   if (viewModelSecurity.Usuarios.ClaveCifrada is null || viewModelSecurity.Usuarios.ClaveCifrada.Trim().Length == 0)
                    {
                        viewModelSecurity.Usuarios.ClaveCifrada = null;
                    }
                    else
                    {                        
                        viewModelSecurity.Usuarios.ClaveCifrada = new EncryptMD5().DesencriptarMD5(viewModelSecurity.Usuarios.ClaveCifrada);
                    }

                    //viewModelSecurity.Usuarios.ClaveCifrada = (viewModelSecurity.Usuarios.ClaveCifrada is null || viewModelSecurity.Usuarios.ClaveCifrada.Trim().Length == 0 ? null :  new EncryptMD5().DesencriptarMD5(viewModelSecurity.Usuarios.ClaveCifrada): null);
                    responseModel.Exito = 1;
                    responseModel.Mensaje = "Consulta exitosa";
                 
                    foreach (var item in viewModelSecurity.Usuarios.RolesUsuarios)
                    {                                                
                        viewModelSecurity.RolesUsuarios.Add(new RolesUsuarios
                        {
                            UsuarioID = item.UsuarioID,
                            RolID = item.RolID,
                            NombreRol = new ServiceRoles().ObtenerSoloNombreRolPorId(item.RolID),
                            FechaCreacion = item.FechaCreacion
                        });
                    }
                }
                else
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje = "No existe el usuario en base de datos";
                    viewModelSecurity = null;
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }                       
           
            return viewModelSecurity;

        }

        /// <summary>
        /// Verificar si existe en la tabla usuario
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        public bool ExisteDataOnTablaUsuario(string usuarioID, ResponseModel responseModel)
        {
            bool existeRegistro;
            try
            {
                using (var _db = new CoreDBContext())
                {
                    //comprobar si en el la tabla usuario existe el registro 
                    existeRegistro = _db.Usuarios.Where(user => user.Usuario == usuarioID).Count() > 0 ? true : false;

                }

                if (!existeRegistro)
                {
                    responseModel.Mensaje = $"No existe el usuario {usuarioID} en la base de dato";
                    responseModel.Exito = 0;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }

            return existeRegistro;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        public async Task<int> EliminarUsuario(string usuarioID, ResponseModel responseModel)
        {
            var result = 0;
            try
            {
                //verificar si existe el usuario en la base de datos
                if (!ExisteDataOnTablaUsuario(usuarioID, responseModel))
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje = "No existe el usuario en la base de datos";
                }
                //verificar si existe el usuario  en otras  tablas
                else if (ExisteUsuarioEnTablaRolesUsuarios(usuarioID, responseModel))
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje = "No se puede eliminar el usuario, " +
                                            "esta siendo usada en otras tablas";
                }
                else
                {
                    //obtener los datos de la tabla usuario
                    var usuar = await ObtenerUsuarioPorId(usuarioID, responseModel);

                    using (var _db = new CoreDBContext())
                    {
                        //elimina los datos de la tabla usuario
                        _db.Usuarios.Remove(usuar);
                        result = _db.SaveChanges();
                    }

                    //comprobar si elimino el rol
                    if (result > 0)
                    {
                        responseModel.Mensaje = "Se ha eliminado exitosamente";
                        responseModel.Exito = 1;
                    }
                    else
                    {
                        responseModel.Mensaje = "No se ha eliminado el usuario";
                        responseModel.Exito = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        /// <summary>
        /// obtener usuario por ID
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        public async Task<Usuarios> ObtenerUsuarioPorId(string usuarioID, ResponseModel responseModel)
        {
            var model = new Usuarios();

            try
            {
                using (var _db = new CoreDBContext())
                {
                    model = await _db.Usuarios.Where(user => user.Usuario == usuarioID).FirstOrDefaultAsync();
                }

                //verificar si el modelo es null
                if (model == null)
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje = "No existe el usuario en la base de datos";
                }
                else
                {
                    responseModel.Exito = 1;
                    responseModel.Mensaje = "<<Usuario encontrado>>";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return model;
        }


        /// <summary>
        /// existe el usuario ID  en la tabla RolesUsuario
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        public bool ExisteUsuarioEnTablaRolesUsuarios(string usuarioID, ResponseModel responseModel)
        {
            bool existeusuario;

            try
            {
                using (var _db = new CoreDBContext())
                {
                    //comprobar si en el la tabla rolesusuairo existe id del usuario
                    existeusuario = _db.RolesUsuarios.Where(id => id.UsuarioID == usuarioID).Count() > 0 ? true : false;
                }
            }

            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
                throw new Exception("Error: " + ex.Message);
            }

            return existeusuario;

        }



        /// <summary> 
        /// validar el modelo del Usuario
        /// </summary>
        /// <param name="model"></param>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        public bool ModeloUsuarioEsValido(ViewModelSecurity model, ResponseModel responseModel)
        {
            bool modeloIsValido = false;
            try
            {
                //validar 
                if (model.Usuarios.Nombre == null)
                {

                    responseModel.Mensaje = "Debes de Ingresar el Nombre y Apellido del Usuario";
                    responseModel.NombreInput = "NombreUsuario";
                }

                else if (model.Usuarios.Usuario == null)
                {
                    responseModel.Mensaje = "Debes de Ingresar el Login del usuario";
                    responseModel.NombreInput = "LoginUsuario";
                }

                else if (model.Usuarios.ClaveCifrada == null)
                {
                    responseModel.Mensaje = "Debes de ingresar la clave";
                    responseModel.NombreInput = "Clave_Cifrada";
                }
                else if (model.Usuarios.ClaveCifrada != model.Usuarios.ConfirmarClaveCifrada)
                {
                    responseModel.Mensaje = "El Password de Confirmacion es diferente";
                    responseModel.NombreInput = "ConfirmarClaveCifrada";
                }
                else
                {
                    model.Usuarios.Nombre = model.Usuarios.Nombre.Trim();

                    //model.Usuario.Correo = model.Usuario.Correo.Trim();
                    model.Usuarios.Usuario = model.Usuarios.Usuario.Trim();
                    model.Usuarios.ClaveCifrada = model.Usuarios.ClaveCifrada.Trim();


                    //comprobar si es nuevo usuario
                    if (model.Usuarios.NuevoUsuario)
                    {
                        //comprobar si el modelo es valido cuando se esta agregando un nuevo registro
                        modeloIsValido = ModelIsValidWhenIsNewUsuario(model, responseModel);
                    }
                    else
                    {
                        //comprobar si el modelo es valido cuando se está editando el registro
                        modeloIsValido = ModelIsValidWhenIsEditUsuario(model, responseModel);
                    }

                    model.Usuarios.ClaveCifrada = new EncryptMD5().EncriptarMD5(model.Usuarios.ClaveCifrada);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return modeloIsValido;
        }

        /// <summary> continue 29/10/2021
        /// Verificar si el modelo es valido cuando se está editando el registro
        /// </summary>
        /// <param name="model"></param>
        /// <param name="respuesta"></param>
        /// <returns></returns>
        private bool ModelIsValidWhenIsEditUsuario(ViewModelSecurity model, ResponseModel responseModel)
        {
            bool modeloIsValido = false;
            try
            {
                //Obtener el login del Usuario filtrado por el usuarioID
                var loginUserConsultado = ObtenerSoloLoginUserPorId(model.Usuarios.Usuario);

                //comprobar si el login del usuario  es diferente la login de usuario que edito es diferente al login de usuario de la base de datos
                //y verifico en base de datos si existe el login de usuario que estoy editando.
                if ((loginUserConsultado.Trim() != model.Usuarios.Usuario.Trim()) && (ExisteLogin(model.Usuarios.Usuario.Trim())))
                {
                    responseModel.Mensaje = "El login del Usuario ya existe";
                    responseModel.NombreInput = "Usuario";
                }
                else
                {
                    modeloIsValido = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return modeloIsValido;
        }


        /// <summary> 
        /// Verificar si el modelo es valido cuando es un nuevo registro
        /// </summary>
        /// <param name="model"></param>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        private bool ModelIsValidWhenIsNewUsuario(ViewModelSecurity model, ResponseModel responseModel)
        {
            bool modeloIsValido = false;
            try
            {

                //comprobar si el existe el nombre del usuario
                if (ExisteLogin(model.Usuarios.Usuario.Trim()))
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje = "El login del usuario ya existe";
                    responseModel.NombreInput = "LoginUsuario";
                }
                //comprobar si tiene check al campo activo
                else if (model.Usuarios.Activo == "N")
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje = "Debes de activar el campo activo";
                    responseModel.NombreInput = "Activo";
                }
                else
                {
                    model.Usuarios.RowPointer = new Utilidades().GenerarGuid();
                    model.Usuarios.Fecha_Ult_Clave = DateTime.Now;
                    model.Usuarios.RecordDate = DateTime.Now;
                    modeloIsValido = true;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return modeloIsValido;
        }
    }
}