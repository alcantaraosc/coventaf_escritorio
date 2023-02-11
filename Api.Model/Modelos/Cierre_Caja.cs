using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{        
    public class Cierre_Caja
    {       
        public Cierre_Caja()
        {
            //this.CIERRE_INFO_TARJ = new HashSet<CIERRE_INFO_TARJ>();
           //this.Cierre_Pos = new HashSet<Cierre_Pos>();
        }

        //[Key]
        //[Column(Order = 0)]
        [StringLength(20)]
        public string Num_Cierre_Caja { get; set; }

        //[Key]
        //[Column(Order = 1)]
        [Column(TypeName = "varchar(6)")]
        public string Caja { get; set; }
        [Required]
        public DateTime Fecha_Apertura { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string Cajero_Cierre { get; set; }
        public DateTime? Fecha_Cierre { get; set; }
        [Column(TypeName = "varchar(1)")]
        public string Estado { get; set; }
        public DateTime? Fecha_Anulacion { get; set; }
        [Required]
        [Column(TypeName = "varchar(25)")]
        public string Cajero_Apertura { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Doc_Fiscal { get; set; }
        
        public string Documento { get; set; }
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

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CIERRE_INFO_TARJ> CIERRE_INFO_TARJ { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Cierre_Pos> Cierre_Pos { get; set; }
    }
}
