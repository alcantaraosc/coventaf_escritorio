using Api.Context;
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
    public class ServiceArticulo//: IArticulo
    {
        private readonly CoreDBContext _db;
        public ServiceArticulo()
        {
            this._db = new CoreDBContext();
        }

        /// <summary>
        /// obtener el registro de un cliente por medio el codigo de cliente
        /// </summary>
        /// <param name="codigoBarra"></param>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        public async Task<ViewArticulo> ObtenerArticuloPorIdAsync( ResponseModel responseModel, string codigoBarra, string bodegaID, string nivelPrecio)
        {
            var Articulo = new ViewArticulo ();
   
            try
            {
                using (SqlConnection cn = new SqlConnection(ADONET.strConnect))
                {
                    //Abrir la conección 
                    await cn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("SP_PrecioArticulos", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@CodigoBarra", codigoBarra);
                    cmd.Parameters.AddWithValue("@BodegaID", bodegaID);
                    cmd.Parameters.AddWithValue("@NivelPrecio", nivelPrecio);


                    var dr = await cmd.ExecuteReaderAsync();
                    if (await dr.ReadAsync())
                    {
                        Articulo.ArticuloID = dr["ARTICULOID"].ToString();
                        Articulo.CodigoBarra = dr["CODIGOBARRA"].ToString();
                        Articulo.Descripcion = dr["DESCRIPCION"].ToString();
                        Articulo.Precio = Convert.ToDecimal(dr["PRECIO"]);
                        Articulo.UnidadVenta = dr["UNIDAD_VENTA"].ToString();
                        Articulo.BodegaID = dr["BODEGAID"].ToString();
                        Articulo.NombreBodega = dr["NOMBREBODEGA"].ToString();
                        Articulo.NivelPrecio = dr["NIVELPRECIO"].ToString();
                        Articulo.Existencia = Convert.ToDecimal(dr["EXISTENCIA"]);
                        Articulo.Moneda = Convert.ToChar(dr["MONEDA"]);
                        Articulo.Descuento = Convert.ToDecimal(dr["DESCUENTO"]);
                    }
                }


                if (Articulo != null)
                {
                    //1 signinfica que la consulta fue exitosa
                    responseModel.Exito = 1;
                    responseModel.Mensaje = "Consulta exitosa";
                }
                else
                {
                    //0 signinfica que la consulta no se encontro en la base de datos
                    responseModel.Exito = 0;
                    responseModel.Mensaje = $"El articulo {codigoBarra} no existe en la base de datos";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Articulo;
        }

    }
}
