using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{
    public class Cierre_Pos
    {
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
        [Column(TypeName = "varchar(1)")]
        public string Tipo_Cierre { get; set; }
        [Required]
        [Column(TypeName = "varchar(40)")]
        public string Nombre_Vendedor { get; set; }
        [Required]
        public DateTime Fecha_Hora { get; set; }        
        [Required]
        public decimal Tipo_Cambio { get; set; }
        [Required]
        public decimal Monto_Apertura { get; set; }
        [Required]
        public decimal Total_Diferencia { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Documento_Ajuste { get; set; }
        [Required]
        public decimal Total_Local { get; set; }
        [Required]
        public decimal Total_Dolar { get; set; }
        [Required]
        public decimal Ventas_Efectivo { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        //Indica si el cierre esta abierto(A); cerrado(C) o anulado(N)
        public string Estado { get; set; }
        public DateTime? Fecha_Hora_Inicio { get; set; }
        [Column(TypeName = "varchar(4000)")]
        public string Notas { get; set; }
        [Required]    
        public decimal Cobro_Efectivo_Rep { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Num_Cierre_Caja { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Doc_Fiscal { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Documento { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string Correlativo { get; set; }        
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
