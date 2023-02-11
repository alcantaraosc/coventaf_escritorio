using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Setting
{
    public static class ConectionContext
    {
        public static string Server { get; set; } = "localhost";// "172.16.20.202";
        public static string User { get; set; } = "sa";
        public static string Password { get; set; } = "sql2017";
        public static string DataBase { get; set; } = "TIENDA";
    }
}
