using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.ViewModels
{
    public class ViewModelUsuario
    {
        public string Usuario { get; set; }
        public string NombreUsuario { get; set; } 
        public string ClaveCifrada { get; set; }
        public string Grupo { get; set; }
        public string NombreTienda { get; set; }
        public string DireccionTienda { get; set; }
        public string TelefonoTienda { get; set; }    
        public string NivelPrecio { get; set; }
        public string MonedaNivel { get; set; }
        public string Caja { get; set; }
        public string Bodega { get; set; }
        public string NombreBodega { get; set; }        
        public string Token { get; set; }       
        public DateTime ExpireAt { get; set; }
    }
}
