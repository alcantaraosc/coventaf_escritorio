using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.ViewModels
{
    public class VariableCierreCaja
    {
        public decimal TotalCordoba { get; set; } = 0;
        public decimal TotalDolar { get; set; } = 0;
        public decimal TotalCajaCordoba { get; set; } = 0;
        public decimal TotalCajaDolares { get; set; } = 0;

        public decimal SumaDenomCordoba { get; set; } = 0;
        public decimal SumaDenomDolar { get; set; } = 0;
    }
}
