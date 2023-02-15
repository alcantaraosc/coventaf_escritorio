using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{
    public class DatosCierreCaja
    {
        public decimal Monto_Local { get; set; }
        public decimal Monto_Dolar { get; set; }
        public string Forma_Pago { get; set; }
        public string Descripcion { get; set; }
    }
}
