using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.View
{
    public class ViewFactura
    {
        public string Tipo_Documento { get; set; }
        public string Factura { get; set; }
        public string Cliente {get; set;} 
        public string Nombre_Cliente { get; set; }
        public decimal Total_Factura { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total_Unidades { get; set; }

    }
}
