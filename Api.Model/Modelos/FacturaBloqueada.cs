using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{
    public class FacturaBloqueada
    {
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string NoFactura { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Cajero { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string Caja { get; set; }
        [Required]
        [StringLength(50)]
        public string NumCierreCT { get; set; }

        [Required]
        [Column(TypeName = "varchar(4)")]
        public string TipoDocumento { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string EstadoFactura { get; set; }
    }
}
