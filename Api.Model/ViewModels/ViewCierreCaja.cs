
using Api.Model.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Api.Model.ViewModels
{
    public class ViewCierreCaja
    {
               
        public string NumCierre { get; set; }      
        public string Cajero { get; set; }       
        public string Caja { get; set; }
        public decimal Total_Local { get; set; }
        public decimal Total_Dolar { get; set; }       
        public decimal Ventas_Efectivo { get; set; }
        public string Notas { get; set; }
        public decimal Cobro_Efectivo_Rep { get; set; }
        public string Num_Cierre_Caja { get; set; }
        public List<Cierre_Det_Pago> Cierre_Det_Pago { get; set; }
    }
}