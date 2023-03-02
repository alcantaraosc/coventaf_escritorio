using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Modelos
{
    public class Unidad_Fraccion
    {
        //esta es una consulta de softland para validar si la cantidad tiene punto decimal
        //SELECT FRACCION, * FROM  TIENDA.UNIDAD_FRACCION WHERE UNIDAD_MEDIDA = 'UND'
        [Required]
        [StringLength(6)]
        public string Unidad_Medida { get; set; }
        [Required]
        [StringLength(1)]
        public string Fraccion { get; set; }
        [Required]
        [StringLength(1)]
        public string Balanza_Electronica { get; set; }

    }
}
