using Api.Context;
using Api.Model.Modelos;
using Api.Model.ViewModels;
using Api.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service.DataService
{
    public class ServiceBodega//: IBodega
    {
        private readonly CoreDBContext _db;
        public ServiceBodega()
        {
            this._db = new CoreDBContext();
        }

        /// <summary>
        /// Listar las bodegas activas
        /// </summary>
        /// <param name="responseModel"></param>
        /// <returns></returns>
        public async Task<List<Vendedores>> ListarBodegasAsync(ResponseModel responseModel)
        {
            var ListBodega = new List<Vendedores>();
            try
            {
                ListBodega = await _db.Vendedores.Where(b=>b.Activo=="S").ToListAsync();
                if (ListBodega.Count >0)
                {
                    responseModel.Exito = 1;
                    responseModel.Mensaje = "Consulta exitosa";
                }
                else
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje = "No hay registro de Bodega";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ListBodega;
        }
    }
}
