using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{
    public  class Retenciones
    {
        [Required]
        [Column(TypeName = "varchar(4)")]
        public string Codigo_Retencion { get; set; }

        [Required]
        [Column(TypeName = "varchar(40)")]
        public string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "Decimal(28, 8)")]
        public decimal Porcentaje { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Estado { get; set; }
        
        [Column(TypeName = "varchar(1)")]
        public string Es_AutoRetenedor { get; set; }
       
    }
}
