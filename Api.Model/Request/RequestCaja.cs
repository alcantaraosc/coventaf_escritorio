using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Model.Request
{
    public class RequestCaja
    {
        public bool ExisteCajaAperturada { get; set; }
        public string Caja { get; set; }
        public bool ExisteCajeroApertura { get; set; }
        public string Cajero { get; set; }
        public string ConsecutivoCierreCT { get; set; }
    }
}
