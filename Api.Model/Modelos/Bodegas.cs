using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{
    public class Bodegas
    {
        [Required]
        [Column(TypeName = "varchar(4)")]
        public string Bodega { get; set; }
        [Required]
        [Column(TypeName = "varchar(40)")]
        public string Nombre { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Tipo { get; set; }
        public bool Activo { get; set; }
        [Required]
        [Column(TypeName = "varchar(3)")]        
        public string U_Tienda_Madre { get; set; }
        
    }
}
