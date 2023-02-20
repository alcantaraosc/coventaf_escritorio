﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{
    
    public class Factura_Linea
    {       
        public Factura_Linea()
        {
            //this.DESPACHO_DETALLE = new HashSet<DESPACHO_DETALLE>();
            //this.FACTURA_DOC_CC = new HashSet<FACTURA_DOC_CC>();
        }

        //[Key]
        //[Column(Order = 0)]
        
        [Column(TypeName = "varchar(50)")]
        public string Factura { get; set; }
        //[Key]
        //[Column(Order = 1)]        
        [Column(TypeName = "varchar(1)")]
        public string Tipo_Documento { get; set; }
        public short Linea { get; set; }
        [Required]        
        [Column(TypeName = "varchar(4)")]
        public string Bodega { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Costo_Total_Dolar { get; set; }        
        [Column(TypeName = "varchar(50)")]
        public string Pedido { get; set; }
        [Required]       
        [Column(TypeName = "varchar(20)")]
        public string Articulo { get; set; }        
        [Column(TypeName = "varchar(8)")]
        public string Localizacion { get; set; }        
        [Column(TypeName = "varchar(15)")]
        public string Lote { get; set; }
        [Required]       
        [Column(TypeName = "varchar(1)")]
        public string Anulada { get; set; }
        [Required]
        public DateTime Fecha_Factura { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Cantidad { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Precio_Unitario { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Total_Impuesto1 { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Total_Impuesto2 { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Desc_Tot_Linea { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Desc_Tot_General { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Costo_Total { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Precio_Total { get; set; }
                
        [Column(TypeName = "text")]
        public string Descripcion { get; set; }        
        [Column(TypeName = "varchar(100)")]
        public string Comentario { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Cantidad_Devuelt { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Descuento_Volumen { get; set; }
        [Required]        
        [Column(TypeName = "varchar(1)")]
        public string Tipo_Linea { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Cantidad_Aceptada { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Cant_No_Entregada { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Costo_Total_Local { get; set; }
        public short? Pedido_Linea { get; set; }
        [Required]
        public short Multiplicador_Ev { get; set; }
        public int? Serie_Cadena { get; set; }
        public int? Serie_Cad_No_Acept { get; set; }
        public int? Serie_Cad_Aceptada { get; set; }        
        [Column(TypeName = "varchar(50)")]
        public string Documento_Origen { get; set; }
        public short? Linea_Origen { get; set; }        
        [Column(TypeName = "varchar(1)")]
        public string Tipo_Origen { get; set; }        
        [Column(TypeName = "varchar(6)")]
        public string Unidad_Distribucio { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Cant_Despachada { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Costo_Estim_Local { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Costo_Estim_Dolar { get; set; }
        [Column(TypeName = "decimal(28, 8)")]
        public decimal? Cant_Anul_Pordespa { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Monto_Retencion { get; set; }
        [Column(TypeName = "decimal(28, 8)")]
        public decimal? Base_Impuesto1 { get; set; }
        [Column(TypeName = "decimal(28, 8)")]
        public decimal? Base_Impuesto2 { get; set; }        
        [Column(TypeName = "varchar(25)")]
        public string Proyecto { get; set; }        
        [Column(TypeName = "varchar(25)")]
        public string Fase { get; set; }        
        [Column(TypeName = "varchar(25)")]
        public string Centro_Costo { get; set; }        
        [Column(TypeName = "varchar(25)")]
        public string Cuenta_Contable { get; set; }       
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Costo_Total_Comp { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Costo_Total_Comp_Local { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Costo_Total_Comp_Dolar { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Costo_Estim_Comp_Local { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Costo_Estim_Comp_Dolar { get; set; }
        [Required]
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Cant_Dev_Proceso { get; set; }
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
        
        [Column(TypeName = "varchar(4)")]
        public string Tipo_Impuesto1 { get; set; }        
        [Column(TypeName = "varchar(2)")]
        public string Tipo_Tarifa1 { get; set; }        
        [Column(TypeName = "varchar(4)")]
        public string Tipo_Impuesto2 { get; set; }        
        [Column(TypeName = "varchar(2)")]
        public string Tipo_Tarifa2 { get; set; }
        [Column(TypeName = "decimal(28, 8)")]        
        public decimal? Porc_Exoneracion { get; set; }
        [Column(TypeName = "decimal(28, 8)")]
        public decimal? Monto_Exoneracion { get; set; }        
        [Column(TypeName = "varchar(1)")]
        public string Es_Otro_Cargo { get; set; }        
        [Column(TypeName = "varchar(1)")]
        public string Es_Canasta_Basica { get; set; }        
        [Column(TypeName = "varchar(1)")]
        public string Es_Servicio_Medico { get; set; }
        [Column(TypeName = "decimal(28, 8)")]        
        public decimal? Monto_Devuelto_Iva { get; set; }
        [Column(TypeName = "decimal(28, 8)")]
        public decimal? Porc_Exoneracion2 { get; set; }
        [Column(TypeName = "decimal(28, 8)")]
        public decimal? Monto_Exoneracion2 { get; set; }        
        [Column(TypeName = "varchar(2)")]
        public string Tipo_Descuento_Linea { get; set; }
        [Column(TypeName = "decimal(28, 8)")]
        public decimal Porc_Desc_Linea { get; set; }

        //public virtual ARTICULOS ARTICULOS { get; set; }
        //public virtual BODEGAS BODEGAS { get; set; }

        //public virtual CENTRO_COSTO CENTRO_COSTO1 { get; set; }
        //public virtual CUENTA_CONTABLE CUENTA_CONTABLE1 { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        ////public virtual ICollection<DESPACHO_DETALLE> DESPACHO_DETALLE { get; set; }
        //public virtual FACTURAS FACTURAS { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<FACTURA_DOC_CC> FACTURA_DOC_CC { get; set; }
        //public virtual UNIDAD_DE_MEDIDA UNIDAD_DE_MEDIDA { get; set; }
        //public virtual LOCALIZACION LOCALIZACION1 { get; set; }
        //public virtual SERIE_CADENA SERIE_CADENA1 { get; set; }
        //public virtual SERIE_CADENA SERIE_CADENA2 { get; set; }
        //public virtual SERIE_CADENA SERIE_CADENA3 { get; set; }
        //public virtual LOTE LOTE1 { get; set; }
        //public virtual TIPO_DESCUENTO TIPO_DESCUENTO { get; set; }
    }
}
