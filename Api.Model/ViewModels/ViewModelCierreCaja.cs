using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.ViewModels
{
    public class ViewModelCierreCaja
    {
        public string Id { get; set; }
        public string Forma_Pago { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; }        
        
    }
}
