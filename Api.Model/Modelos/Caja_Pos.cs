using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{
    public class Caja_Pos
    {
        [Required]
        [Column(TypeName = "varchar(6)")]
        public string Caja { get; set; }
        [Required]
        [Column(TypeName = "varchar(2)")]
        public string Codigo_Corto { get; set; }
        [Required]
        [Column(TypeName = "varchar(6)")]
        public string Sucursal { get; set; }
        [Required]
        [Column(TypeName = "varchar(4)")]
        public string Bodega { get; set; }        
        [Column(TypeName = "varchar(40)")]
        public string Ubicacion { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Asignado { get; set; }        
        [Column(TypeName = "varchar(20)")]
        public string Identificador { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string Centro_Costo { get; set; }
        [Required]        
        [Column(TypeName = "varchar(20)")]
        public string Cons_Cierre_Caja { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Consec_Doc_Espera { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Estado { get; set; }       
        [Column(TypeName = "text")]
        public string Firma { get; set; }
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
        public string Actividad_Comercial { get; set; }
    }
}
