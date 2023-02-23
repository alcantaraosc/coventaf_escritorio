﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.ViewModels
{
    public class varFacturacion
    {     
        public string NoFactura { get; set; }
        public string inputActivo { get; set; }
        public string IdActivo { get; set; }

        //indica si el descuento esta aplicado o no esta aplicado
        public bool DescuentoActivo { get; set; }
  
        public decimal TipoDeCambio { get; set; }
        public string BodegaId { get; set; }        
        //mostrar los datos del cliente en el html
        public string NombreCliente { get; set; }
        public decimal SaldoDisponible { get; set; }
        public decimal PorCentajeDescCliente { get; set; }
        public decimal PorCentajeDescGeneral { get; set; }
         // fechaDay = new Date();

        /**Totales */
        public decimal SubTotalDolar { get; set; }
        public decimal SubTotalCordoba { get; set; }
        //descuento
        public decimal DescuentoPorLineaDolar { get; set; }
        public decimal DescuentoPorLineaCordoba { get; set; }
        
        //subtotales 
        public decimal DescuentoGeneralCordoba { get; set; }
        public decimal DescuentoGeneralDolar { get; set; }

        public decimal SubTotalDescuentoDolar { get; set; }
        public decimal SubTotalDescuentoCordoba { get; set; }

        public decimal TotalRetencion { get; set; }

        public decimal IvaCordoba { get; set; }
        public decimal IvaDolar { get; set; }
        public decimal TotalDolar { get; set; }
        public decimal TotalCordobas { get; set; }
        public decimal TotalUnidades { get; set; }
        public DateTime FechaFactura { get; set; }

        public string TicketFormaPago { get; set; }
      

    }
}
