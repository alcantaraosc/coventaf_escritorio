using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.ViewModels
{
    public class DetalleRetenciones
    {
        public string Retencion { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public decimal Base { get; set; }
        public string Referencia { get; set; }
        public bool AutoRenedora { get; set; }
    }
}
