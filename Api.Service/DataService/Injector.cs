using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service.DataService
{
    public static class Injector
    {
        static IServiceProvider _proveedor;

        public static void GenerarProveedor(IServiceCollection serviceCollection)
        {
            _proveedor = serviceCollection.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return _proveedor.GetService<T>();
        }
    }
}
