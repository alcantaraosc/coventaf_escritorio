using Api.Context;
using Api.Helpers;
using Api.Model.Modelos;
using Api.Model.View;
using Api.Model.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service.DataService
{
    public class ServiceCaja_Pos
    {
       

        public ServiceCaja_Pos()
        {
            
        }

        /// <summary>
        /// Listar las cajas disponible para cada sucursal
        /// </summary>
        /// <param name="cajero"></param>
        /// <param name="sucursalID"></param>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        public async Task<List<ViewCajaDisponible>> ListarCajasDisponibles( string cajero, string sucursalID, ResponseModel responseModel)
        {
            var listaCajaDisponible = new List<ViewCajaDisponible>();
            try
            {
                using (var _db = new CoreDBContext())
                {
                    //primero verificar si el cajero esta en la tabla de cajeros
                    var datoCajero = await _db.Cajeros.Where(cj => cj.Cajero == cajero).FirstOrDefaultAsync();
                    if (datoCajero is not null)
                    {
                        //listar todas las cajas disponibles que forman parte de la sucursal vinculada
                        listaCajaDisponible = await _db.ViewCajaDisponible.Where(cd=>cd.Sucursal==sucursalID).ToListAsync();
                    }

                    if (datoCajero is not null && listaCajaDisponible.Count > 0)
                    {
                        responseModel.Exito = 1;
                        responseModel.Mensaje = "Consulta exitosa";
                    }
                    else
                    {
                        responseModel.Exito = 0;
                        if (datoCajero is null)
                        {
                            responseModel.Mensaje = "Este usuario no pertenece a la lista de Cajero";
                        }
                        else if (listaCajaDisponible.Count ==0)
                        {
                            responseModel.Mensaje = "Actualmente el sistema ya no tiene caja disponible";
                        }
                        
                    }


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listaCajaDisponible;

        }

        /// <summary>
        /// verificar si la caja de apertura ya esta ocupada antes de guardar, esto validacion se da por que el usuario 
        /// puede ser que deje la ventana abierta y haga apertura al instante.
        /// </summary>
        public bool CajaAperturaOcupada(string caja, ResponseModel responseModel)
        {
            bool ocupada = false;

            try
            {
                using( var _db = new CoreDBContext())
                {
                    //obtener el registor de la caja x que este abierta
                    var result = _db.Cierre_Pos.Where(cp => cp.Caja == caja && cp.Estado == "A").FirstOrDefault();
                    if (result is null)
                    {
                        responseModel.Exito = 1;
                        responseModel.Mensaje = "Caja Libre";
                        ocupada = false;
                    }
                    else
                    {
                        responseModel.Exito = 0;
                        responseModel.Mensaje = $"La Caja {caja} ya fue aperturada";
                        ocupada = true;
                    }
                    
                }
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ocupada;
        }
        /// <summary>
        /// revisar si la caja ya fue aperturada por el cajero actual
        /// </summary>
        /// <param name="cajero"></param>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        private bool AperturadoPorCajeroActual(string cajero, ResponseModel responseModel)
        {
            bool  aperturadoPorCajero = false;

            try
            {
                using (var _db = new CoreDBContext())
                {
                    //obtener el registor de la caja x que este abierta
                    var result = _db.Cierre_Pos.Where(cp => cp.Cajero == cajero && cp.Estado == "A").FirstOrDefault();
                    responseModel.Data = result as Cierre_Pos;
                    if (result is not null)
                        aperturadoPorCajero = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return aperturadoPorCajero;
        }


        /*
        public bool AperturadoPorCajeroActual(string cajero, ResponseModel responseModel)
        {
            bool aperutadoPorCajero = false;

            try
            {
                using (var _db = new CoreDBContext())
                {
                    //obtener el registor de la caja x que este abierta
                    var result = _db.Cierre_Pos.Where(cp => cp.Cajero == cajero && cp.Estado == "A").FirstOrDefault();
                    if (result is null)
                    {
                        responseModel.Exito = 1;
                        responseModel.Mensaje = "El Cajero ya aperturo";
                        aperutadoPorCajero = false;
                        responseModel.Data = result as Cierre_Pos;
                    }
                    else
                    {
                        responseModel.Exito = 0;
                        responseModel.Mensaje = "La Caja se encuentra ocupada";
                        aperutadoPorCajero = true;
                        //responseModel.Data = result.Num_Cierre as string;
                        responseModel.Data = result as Cierre_Pos;
                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return aperutadoPorCajero;
        }*/
        /// <summary>
        /// verifica que la caja que esta aperturada esta ocupado por un cajero
        /// </summary>
        /// <param name="caja"></param>
        /// <param name="cajero"></param>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        public bool CajaAperturaOcupadaConCajeroX(string caja, string cajero, ResponseModel responseModel)
        {
            bool ocupada = false;

            try
            {
                using (var _db = new CoreDBContext())
                {
                    //obtener el registor de la caja x que este abierta
                    var result = _db.Cierre_Pos.Where(cp => cp.Caja == caja && cp.Cajero == cajero && cp.Estado == "A").FirstOrDefault();
                    if (result is null)
                    {
                        responseModel.Exito = 1;
                        responseModel.Mensaje = "Caja Libre";
                        ocupada = false;
                    }
                    else
                    {
                        responseModel.Exito = 0;
                        responseModel.Mensaje = $"El cajero {cajero} ya hizo apertura con la caja {caja}";
                        ocupada = true;
                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ocupada;
        }


        public bool ModeloAperturaCajaEsValido(string caja, string cajero, ResponseModel responseModel)
        {
            //verificar si el cajero actual ya aperturo      
            if (AperturadoPorCajeroActual(cajero, responseModel))
            {
                //indicar que se no se procedio a guardar
                responseModel.Exito = 0;
                responseModel.Mensaje =$"El Cajero {cajero} ya hizo apertura de caja";
                return false;
            }
            //validar si la caja que intentas aperturar ya esta ocupada
            else if (CajaAperturaOcupada(caja, responseModel))
            {
                return false;
            }
            //validar si la caja ocupada con el cajero
            else if (CajaAperturaOcupadaConCajeroX(caja, cajero, responseModel))
            {
                return false;
            }
           else
            {
                return true;
            }
        }

          
        /// <summary>
        /// Crear la apertura de Caja
        /// </summary>
        /// <param name="caja"></param>
        /// <param name="cajero"></param>
        /// <param name="sucursalID"></param>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        public async Task<List<string>> GuardarAperturaCaja(string caja, string cajero, string sucursalID, decimal montoApertura, ResponseModel responseModel)
        {
            //aqui vas almacenar la bodegaId y Consec_Cierre_CT
            var listResult = new List<string>();
           // listResult = new List<string>(new string { d})
            string Consec_Cierre_CT = "";
            try
            {
                //model.Fecha = DateTime.Now.Date;
                using (SqlConnection cn = new SqlConnection(ADONET.strConnect))
                {
                    //Abrir la conección 
                    await cn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("SP_AperturaCaja", cn);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Caja", caja);
                    cmd.Parameters.AddWithValue("@Cajero", cajero);
                    cmd.Parameters.AddWithValue("@Sucursal", sucursalID);
                    cmd.Parameters.AddWithValue("@MontoApertura", montoApertura);

                    // Se añaden los parámetros de salida y se crean variables para facilitar su recuperacion
                    SqlParameter paramOutConsec_Cierre_CT = cmd.Parameters.Add("@ConsecutivoCierreCT", SqlDbType.VarChar, 50);
                    paramOutConsec_Cierre_CT.Direction = ParameterDirection.Output;

                    SqlParameter paramReturned = cmd.Parameters.Add("@ConsecutivoCierreCT", SqlDbType.VarChar, 50);
                    paramReturned.Direction = ParameterDirection.ReturnValue;
                                     

                    var dr = await cmd.ExecuteReaderAsync();
                    if (await dr.ReadAsync())
                    {
                        //bodegaId
                        listResult.Add(dr["BodegaId"].ToString());                       
                        //ConsecutivoCierreCT
                        listResult.Add(dr["ConsecutivoCierreCT"].ToString());                       
                    }
                                    
                }

                if (listResult.Count > 0)
                {
                    responseModel.Exito = 1;
                    responseModel.Mensaje = $"La Apertura de caja {caja} se realizó exitosamente";
                }
                else
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje =$"La Apertura de caja {caja} no se pudo realizar";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listResult;
        }

        private string ObtenerBodegaId(string caja, string sucursal, ResponseModel responseModel)
        {
            var bodegaID = "";
            try
            {
                using (var _db= new CoreDBContext())
                {
                    bodegaID = _db.Caja_Pos.Where(cp => cp.Caja == caja && cp.Sucursal == sucursal).Select(x => x.Bodega).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bodegaID;
        }

        private bool CajaEstaAperturadoPorCajeroActual(string cajero, string sucursalID, ResponseModel responseModel)
        {
            bool aperturadoPorCajero = false;
            var bodegaID = "";

            try
            {
                using (var _db = new CoreDBContext())
                {
                    //obtener el registor de la caja x que este abierta
                    var cierre_Pos = _db.Cierre_Pos.Where(cp => cp.Cajero == cajero && cp.Estado == "A").FirstOrDefault();                                       
                    //comprobar si la caja ya fue aperturada
                    if (cierre_Pos is not null)
                    {
                        aperturadoPorCajero = true;
                        //luego obtener la bodega ID
                        bodegaID = _db.Caja_Pos.Where(cp => cp.Caja == cierre_Pos.Caja && cp.Sucursal == sucursalID).Select(x => x.Bodega).FirstOrDefault();
                        //asignar los datos para enviarlos
                        responseModel.Data = cierre_Pos as Cierre_Pos;
                        //asignar la bodega
                        responseModel.DataAux = bodegaID.ToString();
                    }                        
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return aperturadoPorCajero;
        }

        public void VerificarExistenciaAperturaCaja(string cajero, string sucursalID, ResponseModel responseModel)
        { 
            //si ya existe la apertura de caja entonces obtener la bodega por defecto           
            if (CajaEstaAperturadoPorCajeroActual(cajero, sucursalID, responseModel))
            {          
                responseModel.Exito = 1;
                responseModel.Mensaje = "Apertura de Caja ya existe";
            }
            else
            {
                responseModel.Exito = 0;
                responseModel.Mensaje = "No existe la apertura de Caja";
            }
        }

        /*public async GuardarCierreCaja()
        {

        }*/

        public async Task<List<ViewModelCierreCaja>> ObtenerDatosParaCierreCaja(string caja, string cajero, string numCierre, ResponseModel responseModel)
        {
            //aqui vas almacenar la bodegaId y Consec_Cierre_CT
            var datosCierreCaja = new List<ViewModelCierreCaja>();           
            try
            {
                //model.Fecha = DateTime.Now.Date;
                using (SqlConnection cn = new SqlConnection(ADONET.strConnect))
                {
                    //Abrir la conección 
                    await cn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("SP_PrepararCierreCaja", cn);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Caja", caja);
                    cmd.Parameters.AddWithValue("@Cajero", cajero);
                    cmd.Parameters.AddWithValue("@NumCierre", numCierre);
                   
                    //// Se añaden los parámetros de salida y se crean variables para facilitar su recuperacion
                    //SqlParameter paramOutConsec_Cierre_CT = cmd.Parameters.Add("@ConsecutivoCierreCT", SqlDbType.VarChar, 50);
                    //paramOutConsec_Cierre_CT.Direction = ParameterDirection.Output;

                    //SqlParameter paramReturned = cmd.Parameters.Add("@ConsecutivoCierreCT", SqlDbType.VarChar, 50);
                    //paramReturned.Direction = ParameterDirection.ReturnValue;


                    var dr = await cmd.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        var datos_ = new ViewModelCierreCaja()
                        {
                            Monto = Convert.ToDecimal(dr["MONTO"]),
                            //Monto_Dolar = Convert.ToDecimal(dr["MONTO_DOLAR"]),
                            Forma_Pago = dr["FORMA_PAGO"].ToString(),
                            Descripcion = dr["DESCRIPCION"].ToString()                            
                        };

                        datosCierreCaja.Add(datos_);
                                               
                    }

                }

                if (datosCierreCaja.Count > 0)
                {
                    responseModel.Exito = 1;
                    responseModel.Mensaje = $"Consulta exitosa";
                }
                else
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje = $"No hay registro para el cierre";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return datosCierreCaja;
        }
    }
}