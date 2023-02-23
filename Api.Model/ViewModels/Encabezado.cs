using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.ViewModels
{
    public class Encabezado
    {
        public string noFactura { get; set; }
        public DateTime fecha { get; set; }
        public string bodega { get; set; }
        public string caja { get; set; }
        public decimal tipoCambio { get; set; }
        public string codigoCliente { get; set; }
        public string cliente { get; set; }
        public decimal subTotalDolar { get; set; }
        public decimal descuentoDolar { get; set; }

        public decimal MontoRetencion { get; set; }

        public decimal ivaDolar { get; set; }
        public decimal totalDolar { get; set; }
        public decimal subTotalCordoba { get; set; }
        public decimal descuentoCordoba { get; set; }
        public decimal ivaCordoba { get; set; }
        public decimal totalCordoba { get; set; }
        public string formaDePago { get; set; }
        public string atentidoPor { get; set; }
        public string observaciones { get; set; }
    }
}
