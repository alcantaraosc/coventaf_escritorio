using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.ViewModels
{
    public class ListUser
    {
        public string Usuario { get; set; }
            
        [StringLength(100)]
        public string Nombre { get; set; }

              
        [StringLength(1)]
        public string Activo { get; set; }

        public string Grupo { get; set; }
    }
}
