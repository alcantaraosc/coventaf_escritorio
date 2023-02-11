using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{      
    public class Consec_Caja_Pos
    {       
        public Consec_Caja_Pos()
        {
            //TIPO_DOC_DEFAULT = new HashSet<TIPO_DOC_DEFAULT>();
            //DOCUMENTO_POS = new HashSet<DOCUMENTO_POS>();
        }

        //[Key]
        //[Column(Order = 0)]
        [StringLength(10)]
        public string Codigo { get; set; }

        //[Key]
        //[Column(Order = 1)]
        [StringLength(6)]
        public string Caja { get; set; }

        [StringLength(60)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(1)]
        public string Tipo_Documento { get; set; }

        [Required]
        [StringLength(50)]
        public string Mascara { get; set; }

        [Required]
        [StringLength(50)]
        public string Valor { get; set; }

        [Required]
        [StringLength(1)]
        public string Credito_Fiscal { get; set; }

        [StringLength(30)]
        public string Formato_Impresion { get; set; }

        [StringLength(20)]
        public string Consec_Fac_Dev { get; set; }

        [StringLength(20)]
        public string Resolucion { get; set; }

        [Required]
        [StringLength(1)]
        public string Activo { get; set; }

        [StringLength(10)]
        public string Clase_Documente { get; set; }

        [Required]
        [StringLength(1)]
        public string USA_DESPACHOS { get; set; }

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

        //public virtual CAJA_POS CAJA_POS { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TIPO_DOC_DEFAULT> TIPO_DOC_DEFAULT { get; set; }

        //public virtual RESOLUCION_POS RESOLUCION_POS { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DOCUMENTO_POS> DOCUMENTO_POS { get; set; }
    }
}
