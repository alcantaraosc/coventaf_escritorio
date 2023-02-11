using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{    
    public class Existencia_Bodega
    {       
        public Existencia_Bodega()
        {
            //PISTA_EXISTEN_DET = new HashSet<PISTA_EXISTEN_DET>();
            //EXISTENCIA_LOTE = new HashSet<EXISTENCIA_LOTE>();
            //EXISTENCIA_RESERVA = new HashSet<EXISTENCIA_RESERVA>();
            //EXISTENCIA_SERIE = new HashSet<EXISTENCIA_SERIE>();
        }

        //[Key]
        //[Column(Order = 0)]
        [StringLength(20)]
        public string Articulo { get; set; }

        //[Key]
        //[Column(Order = 1)]
        [StringLength(4)]
        public string Bodega { get; set; }

        public decimal Existencia_Minima { get; set; }

        public decimal EXISTENCIA_MAXIMA { get; set; }

        public decimal PUNTO_DE_REORDEN { get; set; }

        public decimal CANT_DISPONIBLE { get; set; }

        public decimal CANT_RESERVADA { get; set; }

        public decimal CANT_NO_APROBADA { get; set; }

        public decimal CANT_VENCIDA { get; set; }

        public decimal CANT_TRANSITO { get; set; }

        public decimal CANT_PRODUCCION { get; set; }

        public decimal CANT_PEDIDA { get; set; }

        public decimal CANT_REMITIDA { get; set; }

        [Required]
        [StringLength(1)]
        public string CONGELADO { get; set; }

        public DateTime? FECHA_CONG { get; set; }

        [Required]
        [StringLength(1)]
        public string BLOQUEA_TRANS { get; set; }

        public DateTime? FECHA_DESCONG { get; set; }

        public decimal COSTO_UNT_PROMEDIO_LOC { get; set; }

        public decimal COSTO_UNT_PROMEDIO_DOL { get; set; }

        public decimal COSTO_UNT_ESTANDAR_LOC { get; set; }

        public decimal COSTO_UNT_ESTANDAR_DOL { get; set; }

        public decimal COSTO_PROM_COMPARATIVO_LOC { get; set; }

        public decimal COSTO_PROM_COMPARATIVO_DOLAR { get; set; }

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

        //public virtual ARTICULOS ARTICULOS { get; set; }

        //public virtual BODEGA BODEGA { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PISTA_EXISTEN_DET> PISTA_EXISTEN_DET { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<EXISTENCIA_LOTE> EXISTENCIA_LOTE { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<EXISTENCIA_RESERVA> EXISTENCIA_RESERVA { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<EXISTENCIA_SERIE> EXISTENCIA_SERIE { get; set; }
    }
}

