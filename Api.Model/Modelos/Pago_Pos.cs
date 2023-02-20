using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{
    public class Pago_Pos
    {
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Documento { get; set; }
        [Required]
        [Column(TypeName = "varchar(4)")]      
        public string Pago { get; set; }  /*el campo pago es consecutivo e inicia  desde : 0,1,2 .... n  el codigo -1, es para identificar que el registro es un vuelto del cliente, este codigo (-1) ya esta definido en softland  */
        [Required]
        [Column(TypeName = "varchar(6)")]
        public string Caja { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Tipo { get; set; }  /*F es factura*/
        [Column(TypeName = "varchar(4)")]
        public string Condicion_Pago { get; set; }
        [Column(TypeName = "varchar(8)")]
        public string Entidad_Financiera { get; set; }
        [Column(TypeName = "varchar(12)")]
        public string Tipo_Tarjeta { get; set; }
        [Required]
        [Column(TypeName = "varchar(4)")]
        public string Forma_Pago { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string Numero { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Monto_Local { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Monto_Dolar { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string Autorizacion { get; set; }
        [Column(TypeName = "varchar(5)")]
        public string Fecha_Expiracion { get; set; }
        public int? Cobro { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Cliente_Liquidador { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Tipo_Cobro { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string Referencia { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string Num_Seguimiento { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Num_Transac_Tarjeta { get; set; }       
        [Column(TypeName = "varchar(30)")]
        public string Campo1 { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Valor1 { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Campo2 { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Valor2 { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Campo3 { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Valor3 { get; set; }
        [Column(TypeName = "varchar(30)")]        
        public string Campo4 { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Valor4 { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Campo5 { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Valor5 { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Campo6 { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Valor6 { get; set; }
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
