using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.View
{
    [Keyless]
    public class ViewCajaDisponible
    {
        
        public string Caja { get; set; }
        public string Ubicacion { get; set; }
        public string Sucursal { get; set; }
    }
}
