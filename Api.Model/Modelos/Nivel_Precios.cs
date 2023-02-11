using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{
    public class Nivel_Precios
    {
        [Required]
        [Column(TypeName = "varchar(12)")]
        public string Nivel_Precio { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Moneda { get; set; }
       
        [Column(TypeName = "varchar(4)")]
        public string Condicion_Pago { get; set; }

        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Esquema_Trabajo { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Descuentos { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Sugerir_Descuento { get; set; }
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
