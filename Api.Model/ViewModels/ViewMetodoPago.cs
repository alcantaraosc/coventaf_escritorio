using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.ViewModels
{
    public class ViewMetodoPago
    {
        
        public int Indice { get; set; }
        public string Pago { get; set; } //PagoID=-2 es el total a Cobrar
        public string FormaPago { get; set; }
        public string DescripcionFormPago { get; set; }
        public string TipoTarjeta { get; set; }
        public string CondicionPago { get; set; }
        public string DescripcionCondicionPago { get; set; }
        public decimal MontoCordoba { get; set; }  
        public char Moneda { get; set; } //L=Local(C$), D=Dolar(U$)
        public decimal MontoDolar { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal Diferencia { get; set; }

        public bool TeclaPresionaXCajero { get; set; }
        public string DescripcionTecla { get; set; }

    }
}
