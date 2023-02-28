using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{
    public class Cierre_Det_Pago
    {
        [NotMapped]
        public string Identificacion { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Num_Cierre { get; set; }
        [Required]
        [Column(TypeName = "varchar(25)")]
        public string Cajero { get; set; }

        [Required]
        [Column(TypeName = "varchar(6)")]
        public string Caja { get; set; }

        [Required]
        [Column(TypeName = "varchar(40)")]
        public string Tipo_Pago { get; set; }

        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Total_Sistema { get; set; }

        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Total_Usuario { get; set; }

        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Diferencia { get; set; }

        [Required]        
        public int Orden { get; set; }

        [Required]
        public byte NoteExistsFlag { get; set; }
        [Required]
        public DateTime RecordDate { get; set; }
        [Required]
        public Guid RowPointer { get; set; }
        [Required]
        [Column(TypeName = "varchar(30)")]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "varchar(30)")]
        public string UpdatedBy { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }    
    }
}
