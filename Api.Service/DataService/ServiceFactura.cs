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
using System.Threading.Tasks;

namespace Api.Service.DataService
{
    public class ServiceFactura//: IFactura
    {
        private readonly CoreDBContext _db;
        public ServiceFactura()
        {
            this._db = new CoreDBContext();
        }

        public async Task<List<ViewFactura>> ListarFacturasAsync(FiltroFactura filtroFactura, ResponseModel responseModel)
        {
            var listaFactura = new List<ViewFactura>();
            

            try
            {
                switch (filtroFactura.Tipofiltro)
                {
                    case "Factura del dia":
                        var fechaDeHoy = DateTime.Now.Date;
                       
                        listaFactura = await _db.ViewFactura.Where(x => x.Fecha.Date == fechaDeHoy.Date && x.Usuario== filtroFactura.Cajero).ToListAsync();
                        //listaArticulo =await _db.ARTICULOS.FromSqlRaw("SELECT ARTICULO, DESCRIPCION From TIENDA.ARTICULO Where ARTICULO = {0}", consulta).FirstOrDefault();
                        break;

                    //case "Recuperar factura":
                    //    listaFactura = await _db.FacturaTemporal.Where(x => x.Factura == filtroFactura.Busqueda).ToListAsync();
                    //    //listaArticulo =await _db.ARTICULOS.FromSqlRaw("SELECT ARTICULO, DESCRIPCION From TIENDA.ARTICULO Where ARTICULO = {0}", consulta).FirstOrDefault();
                    //    break;

                    case "Rango de fecha":
                        listaFactura = await _db.ViewFactura.Where(x => x.Fecha >= filtroFactura.FechaInicio && x.Fecha <= filtroFactura.FechaFinal).ToListAsync();
                        //listaArticulo =await _db.ARTICULOS.FromSqlRaw("SELECT ARTICULO, DESCRIPCION From TIENDA.ARTICULO Where ARTICULO = {0}", consulta).FirstOrDefault();
                        break;

                }


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listaFactura;
        }

        public async Task<List<Facturando>> ListarFacturaTemporalesAsync(FiltroFactura filtroFactura, ResponseModel responseModel)
        {
                    
            var listaFacturaTemp = new List<Facturando>();

            try
            {
                switch (filtroFactura.Tipofiltro)
                {
               

                    case "Factura Perdidas":
                        listaFacturaTemp = await _db.Facturando.Where(x => x.Factura == filtroFactura.Busqueda).ToListAsync();
                        //listaArticulo =await _db.ARTICULOS.FromSqlRaw("SELECT ARTICULO, DESCRIPCION From TIENDA.ARTICULO Where ARTICULO = {0}", consulta).FirstOrDefault();
                        break;
                }


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listaFacturaTemp;
        }

        //public async Task<FacturaTemporal> ObtenerFacturaTemporalesAsync(FiltroFactura filtroFactura, ResponseModel responseModel)
        //{

        //}


        public async Task<string> ObtenerNoFactura(ResponseModel responseModel, string cajero, string caja, string numCierre)
        {
            string result = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(ADONET.strConnect))
                {
                    //Abrir la conección 
                    await cn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("SP_GenerarNumeroFactura", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;               
                    cmd.Parameters.AddWithValue("@Cajero", cajero );
                    cmd.Parameters.AddWithValue("@Caja", caja);
                    cmd.Parameters.AddWithValue("@NumCierre", numCierre);

                    var dr = await cmd.ExecuteReaderAsync();
                    if (await dr.ReadAsync())
                    {
                        result = dr["NoFactura"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        /*  public string ObtenerNoFactura()
          {
              string result = "";
              try
              {
                  using (SqlConnection cn = new SqlConnection(ADONET.strConnect))
                  {
                      //Abrir la conección 
                      cn.Open();
                      SqlCommand cmd = new SqlCommand("USP_ObtenerNumeroFactura", cn);
                      cmd.CommandType = CommandType.StoredProcedure;
                      cmd.CommandTimeout = 0;

                      var dr = cmd.ExecuteReader();
                      if (dr.Read())
                      {
                          result = dr["NoFactura"].ToString();
                      }
                  }

              }
              catch (Exception ex)
              {
                  throw new Exception(ex.Message);
              }
              return result;
          }*/

        public async Task<int> InsertOrUpdateFacturaTemporal(Facturando model, ResponseModel responseModel)
        {
            int result = 0;
            try
            {               
                model.FechaRegistro = DateTime.Now.Date;
                using (SqlConnection cn = new SqlConnection(ADONET.strConnect))
                {
                    //Abrir la conección 
                    await cn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("[SP_GuardarFacturaTemporal]", cn);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Factura", model.Factura);
                    cmd.Parameters.AddWithValue("@ArticuloID", model.ArticuloID);
                    cmd.Parameters.AddWithValue("@CodigoCliente", model.CodigoCliente);
                    cmd.Parameters.AddWithValue("@FacturaEnEspera", model.FacturaEnEspera);
                    cmd.Parameters.AddWithValue("@Cajero", model.Cajero);
                    cmd.Parameters.AddWithValue("@Caja", model.Caja);
                    cmd.Parameters.AddWithValue("@NumCierre", model.NumCierre);
                    cmd.Parameters.AddWithValue("@TiendaID", model.TiendaID);
                    cmd.Parameters.AddWithValue("@TipoCambio", model.TipoCambio);
                    cmd.Parameters.AddWithValue("@Bodega", model.BodegaID);
                    cmd.Parameters.AddWithValue("@Consecutivo", model.Consecutivo);
                    cmd.Parameters.AddWithValue("@CodigoBarra", model.CodigoBarra);
                    cmd.Parameters.AddWithValue("@Cantidad", model.Cantidad);
                    cmd.Parameters.AddWithValue("@Descripcion", model.Descripcion);
                    cmd.Parameters.AddWithValue("@Unidad", model.Unidad);
                    cmd.Parameters.AddWithValue("@Precio", model.Precio);
                    cmd.Parameters.AddWithValue("@Moneda", model.Moneda);
                    cmd.Parameters.AddWithValue("@DescuentoLinea", model.DescuentoLinea);
                    cmd.Parameters.AddWithValue("@DescuentoGeneral", model.DescuentoGeneral);
                    cmd.Parameters.AddWithValue("@AplicarDescuento", model.AplicarDescuento);           
                    cmd.Parameters.AddWithValue("@Observaciones", model.Observaciones);
                    result = await cmd.ExecuteNonQueryAsync();
                }
                            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;

        }

        private Facturas AsignarNuevosRegistro(ViewModelFacturacion model)
        {
            Facturas factura = new Facturas();
            var FacturaLinea = new List<Factura_Linea>();
            factura = model.Factura;
            FacturaLinea = model.FacturaLinea;

            return factura;
        }

        /// <summary>
        /// guardar o actualizar los datos de la factura
        /// </summary>
        /// <param name="model"></param>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        public async Task<int> InsertOrUpdateFactura(ViewModelFacturacion model, ResponseModel responseModel )
        {
           
            var utilidad = new Utilidades();
            //Facturas facturas = new Facturas();
            ////List<FACTURA_LINEA> facturaLinea = new List<FACTURA_LINEA>();
            //facturas = model.Factura;
            //facturaLinea = model.FacturaLinea;
           

            int result = 0;
            //utilizar transacciones
            using var transaction = await _db.Database.BeginTransactionAsync();
            {
                try
                {                    
                    //generar el Guid
                    model.Factura.RowPointer = utilidad.GenerarGuid();
                    
                    //agregar la factura
                    _db.Add<Facturas>(model.Factura);
                    //guardar los cambios en la tabla Factura
                    await _db.SaveChangesAsync();

                    for (int row = 0; row < model.FacturaLinea.Count; ++row)
                    {                        
                        //instanciar la clase FACTURA_LINEA
                        var facturaLinea = new Factura_Linea();
                        //agregar la primera fila a la clase facturaLinea
                        facturaLinea = model.FacturaLinea.ElementAt<Factura_Linea>(row);
                       
                        //generar el Guid
                        facturaLinea.RowPointer = utilidad.GenerarGuid();
                        //insertar en la base de datos el p
                        _db.Add<Factura_Linea>(facturaLinea);
                        await _db.SaveChangesAsync();                      
                    }

                    for(int row =0; row < model.PagoPos.Count; row ++)
                    {                        
                        //instanciar la clase Pago_Pos
                        var pagoPos = new Pago_Pos();
                        //agregar la primera fila a la clase Pago_Pos
                        pagoPos = model.PagoPos.ElementAt<Pago_Pos>(row);
                        //asignar el numero de facturas.                        
                        //generar el Guid
                        pagoPos.RowPointer = utilidad.GenerarGuid();
                        //insertar en la base de datos el p
                        _db.Add<Pago_Pos>(pagoPos);
                        await _db.SaveChangesAsync();
                    }

                    //consultar la factura de la tabla temporal
                   // var listFactTemp = await _db.FacturaTemporal.Where(x => x.Factura == model.Factura.Factura).ToListAsync();
                    //eliminar la factura de la tabla temporal
                   // _db.FacturaTemporal.RemoveRange(listFactTemp);
                    await _db.SaveChangesAsync();
                    

                    await transaction.CommitAsync();
                    result = 1;                   
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }          
            }

            
            if (result > 0)
            {
                responseModel.Mensaje = "La factura se ha guardado exitosamente";                
                responseModel.Exito = 1;
            }
            else
            {
                responseModel.Mensaje = "No se pudo guardar la informacion";
                responseModel.Exito = 0;
            }

            return result;
        }
        public async Task<int> EliminarFacturaTemporal(ResponseModel responseModel, string noFactura, string articulo)
        {
          
            int result = 0;
            try
            {
                
                using (SqlConnection cn = new SqlConnection(ADONET.strConnect))
                {
                    //Abrir la conección 
                    await cn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("SP_EliminarArticuloTablaTemp", cn);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Factura", noFactura);
                    cmd.Parameters.AddWithValue("@ArticuloID", articulo);
                    
                    result = await cmd.ExecuteNonQueryAsync();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;

        }
        public async Task<List<Tipo_Tarjeta_Pos>> ListarTipoTarjeta(ResponseModel responseModel)
        {
            var listaTipoTarjeta = new List<Tipo_Tarjeta_Pos>();
            
            try
            {
                listaTipoTarjeta = await _db.Tipo_Tarjeta_Pos.ToListAsync();                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listaTipoTarjeta;
        }

        /// <summary>
        /// Listar el catalogo de condicion de pagos
        /// </summary>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        public async Task<List<Condicion_Pagos>> ListarCondicionPago(ResponseModel responseModel)
        {
            var listaCondicionPago = new List<Condicion_Pagos>();

            try
            {
                using (var _db = new CoreDBContext())
                {
                    listaCondicionPago = await _db.Condicion_Pagos.Where(x => x.Condicion_Pago != "0").ToListAsync();
                }                    
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listaCondicionPago;
        }


        /// <summary>
        /// validar los campos de tabla factura
        /// </summary>
        /// <param name="model"></param>
        /// <param name="responseModel"></param>
        /// <param name="factura"></param>
        /// <returns></returns>

        
        public bool ModeloUsuarioEsValido(ViewModelFacturacion model, ResponseModel responseModel)
        {
            bool modeloIsValido = false;

            try
            {
               
                if (model.Factura.Factura  == null)
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje = "No existe el numero de factura";
                    responseModel.NombreInput = "Factura";
                }
                else if (model.Factura.Cliente == null)
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje = "Debes de ingresar el codigo del cliente";
                    responseModel.NombreInput = "Cliente";
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


        public async Task CancelarNoFacturaBloqueada(ResponseModel responseModel, string noFactura)
        {
            int result = 0;
            FacturaBloqueada datosFactBloq = new FacturaBloqueada();
           
            try
            {
                using (var _db = new CoreDBContext())
                {
                    datosFactBloq = await _db.FacturaBloqueada.Where(fb => fb.NoFactura == noFactura).FirstOrDefaultAsync();

                    if (datosFactBloq is not null)
                    {
                        datosFactBloq.EstadoFactura = "FACT_DISPONIBLE";

                        _db.Update(datosFactBloq);
                        result = await _db.SaveChangesAsync();
                    }             
                }
               
                if (result >0)
                {
                    responseModel.Exito = 1;
                    responseModel.Mensaje = "Factura anulada";
                }
                else
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje = "Factura no se pudo anular";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }          
        }

    }
}
