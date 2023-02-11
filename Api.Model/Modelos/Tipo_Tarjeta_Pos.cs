using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{
    public class Tipo_Tarjeta_Pos
    {
        [Required]
        [Column(TypeName = "varchar(12)")]
        public string Tipo_Tarjeta { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Cliente { get; set; }
        [Required]
        public decimal Comision { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string Ctr_Autorizador { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string Cta_Autorizador { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string Ctr_Comision { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string Cta_Comision { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Tipo_Cobro { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Configuracion_Pago { get; set; }
        public decimal? Porcentaje_Pago { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Porcentaje_Fijo { get; set; }        
        [Column(TypeName = "varchar(20)")]
        public string Ncf_Factura { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Ncf_Devolucion { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Configuracion_Cobro { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Tipo_Conf_Cobro_Cliente { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Generar_Cobro { get; set; }
        [Column(TypeName = "varchar(3)")]
        public string Tipo_Doc_Cxc { get; set; }
        public short? Subtipo_Doc_Cxc { get; set; }
        [Column(TypeName = "varchar(3)")]
        public string Tipo_Doc_Comision { get; set; }
        public short? Subtipo_Doc_Comision { get; set; }
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
