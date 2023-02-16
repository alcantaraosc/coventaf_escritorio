using Api.Model.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.ViewModels
{
    public class ListarDrownList
    {
        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public string NoFactura { get; set; }
        public decimal tipoDeCambio { get; set; }
        //Vendedores se refiere a la bodega
        public List<Bodegas> bodega { get; set; }
        public List<Forma_Pagos> FormaPagos { get; set; }
        public List<Tipo_Tarjeta_Pos> TipoTarjeta { get; set; }
        public List<Condicion_Pagos> CondicionPago { get; set; }
    }
}
