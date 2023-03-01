using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.ViewModels
{
    public class VariableCierreCaja
    {

        //TotalCordoba =TOTAL_LOCAL= [es la suma de todo lo q se recibió solo en cordoba incluyendo efectivo, tarjeta, cheque, creditos]
        public decimal TotalCordoba { get; set; } = 0;
        //--TotalDolar=TOTAL_DOLAR=[es la suma de todo el dinero q se recibió en dolares incluyendo efectivo, tarjeta cheque, pero que solo sea dólar      
        public decimal TotalDolar { get; set; } = 0;
        //--VentaEfectivoCordoba=VENTAS_EFECTIVO= [es la suma  solo en efectivo en cordoba y nada mas]
        public decimal VentaEfectivoCordoba { get; set; } = 0;
        //--VentaEfectivoDolar=COBRO_EFECTIVO_REP= [es la suma  solo en efectivo en dolares y nada mas]
        public decimal VentaEfectivoDolar { get; set; } = 0;
        public decimal TotalDiferencia { get; set; } = 0;
        


        /*
        public decimal TotalCajaCordoba { get; set; } = 0;
        public decimal TotalCajaDolares { get; set; } = 0;

        public decimal SumaDenomCordoba { get; set; } = 0;
        public decimal SumaDenomDolar { get; set; } = 0;*/
    }
}
