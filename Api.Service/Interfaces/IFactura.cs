using Api.Model.Modelos;
using Api.Model.View;
using Api.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service.Interfaces
{
    public  interface IFactura
    {
        public Task<List<ViewFactura>> ListarFacturasAsync(FiltroFactura filtroFactura, ResponseModel responseModel);
        public Task<List<Facturando>> ListarFacturaTemporalesAsync(FiltroFactura filtroFactura, ResponseModel responseModel);
        public Task<string> ObtenerNoFactura(ResponseModel responseModel);
        public Task<List<Tipo_Tarjetas>> ListarTipoTarjeta(ResponseModel responseModel);
        public Task<List<Condicion_Pagos>> ListarCondicionPago(ResponseModel responseModel);
        public bool ModeloUsuarioEsValido(ViewModelFacturacion model, ResponseModel responseModel);     
        public Task<int> InsertOrUpdateCierreCaja(ResponseModel responseModel, Cierre_Caja model);
        public Task<int> InsertOrUpdateFacturaTemporal( Facturando model, ResponseModel responseModel);      
        public Task<int> InsertOrUpdateFactura(ViewModelFacturacion model, ResponseModel responseModel);
        public Task<int> EliminarFacturaTemporal(ResponseModel responseModel, string noFactura, string articulo);
    }
}
