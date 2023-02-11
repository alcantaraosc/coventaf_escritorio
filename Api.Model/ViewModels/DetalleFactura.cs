using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.ViewModels
{
    public class DetalleFactura
    {
        public int consecutivo { get; set; }
        public string articuloId { get; set; }
        public bool inputArticuloDesactivado { get; set; }
        public string codigoBarra { get; set; }
        public decimal cantidad { get; set; }
        public string descripcion { get; set; }
        public string unidad { get; set; }        
        public decimal cantidadExistencia { get; set; }
        public bool inputCantidadDesactivado { get; set; }
        public decimal precioDolar { get; set; }        
        public decimal precioCordobas { get; set; }
        public char moneda { get; set; }
        public string BodegaID { get; set; }
        public string NombreBodega { get; set; }
        public decimal subTotalDolar { get; set; }
        public decimal subTotalCordobas { get; set; }
        public decimal porCentajeDescuentoXArticulo { get; set; }
        public decimal descuentoInactivo { get; set; }
        public decimal descuentoDolar { get; set; }        
        public decimal descuentoCordoba { get; set; }
        
        public decimal descuentoGeneralCordoba { get; set; }
        public decimal descuentoGeneralDolar { get; set; }

        public decimal totalDolar { get; set; }
       
        public decimal totalCordobas { get; set; }
        //con esta propiedad controlo si ya tengo en la lista  { get; set; }
        //el unico input para buscar el proximo articulo { get; set; }
       public bool inputActivoParaBusqueda { get; set; }
        public bool botonEliminarDesactivado { get; set; }



    }
}
