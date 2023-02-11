using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{
    /*aqui estan el nombre de la tienda, TIENDA OUTLET supermercado Es la sucursales*/
    public class Grupos
    {
        [Required]
        [Column(TypeName = "varchar(6)")]
        public string Grupo { get; set; }
        
        [Column(TypeName = "varchar(40)")]
        public string Descripcion {get; set;} 
        [Required]
        [Column(TypeName = "varchar(1)")] 
        public string Sucursal { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Abono_Aprt_Vencido { get; set; }
        [Required]
        [Column(TypeName = "varchar(12)")]
        public string Nivel_Precio { get; set; }
        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Moneda_Nivel { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Cadena { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string Consecutivo_Ci { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Consec_Cierre { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Ncf_Unico { get; set; }       
        [Required]
        public byte NoteExistsFlag { get; set; }
        [Required]
        public DateTime RecordDate { get; set; }
        [Required]
        public Guid RowPointer { get; set; }
        [Required]
        [StringLength(30)]
        public string CreatedBy { get; set; }
        [Required]
        [StringLength(30)]
        public string UpdatedBy { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Direccion { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Telefono { get; set; }

    }
}
