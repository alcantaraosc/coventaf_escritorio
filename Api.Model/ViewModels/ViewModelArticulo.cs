using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.ViewModels
{
    public class ViewModelArticulo
    {
        public string ArticuloID { get; set; }
        public string CodigoBarra { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Existencia { get; set; }
        public string BodegaID { get; set; }
        public string NombreBodega { get; set; }
        public string NivelPrecio { get; set; }
        public string UnidadVenta { get; set; }
        public char Moneda { get; set; }
        public decimal Descuento { get; set; }        
        public decimal Cost_Prom_Dol { get; set; }
        public decimal Costo_Prom_Loc { get; set; }
    }
}
