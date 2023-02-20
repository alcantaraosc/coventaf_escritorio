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
        public string Pago { get; set; } //PagoID=1 es el total a Cobrar
        //codigo forma de pago (0001, 0002, ...)
        public string FormaPago { get; set; }
        //nombre de la forma de pago (Efectivo, Cheque, Tarjeta, ...)
        public string DescripcionFormPago { get; set; }
       
        //cheque
        public string EntidadFinanciera { get; set; }
        //tarjeta
        public string TipoTarjeta { get; set; }   
       //codigo de condicion de pago para Credito
        public string CondicionPago { get; set; }
        //nombre de la descripcion de la condicion de pago para Credito
        public string DescripcionCondicionPago { get; set; }

        public string Numero { get; set; }

        public decimal MontoCordoba { get; set; }  
        public char Moneda { get; set; } //L=Local(C$), D=Dolar(U$)
        public decimal MontoDolar { get; set; }

        public bool TeclaPresionaXCajero { get; set; }
        public string DescripcionTecla { get; set; }

        public string TipoPago { get; set; }
        public decimal Monto { get; set; }
        public string Detalle { get; set; }

    }
}
