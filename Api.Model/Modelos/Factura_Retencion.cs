using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{
    public class Factura_Retencion
    {
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Tipo_Documento { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Factura { get; set; }
        [Required]
        [Column(TypeName = "varchar(4)")]
        public string Codigo_Retencion { get; set; }
        [Required]
        [Column(TypeName = "decimal(28,8)")]
        public decimal Monto { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Doc_Referencia { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Base { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string AutoRetenedora { get; set; }
        public DateTime? Fecha_Documento { get; set; }
        public DateTime? Fecha_Rige { get; set; }

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
