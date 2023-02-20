
using Api.Context;
using Api.Service.DataService;
using Api.Service.Interfaces;
using Api.Setting;
using Controladores;
using COVENTAF.PuntoVenta;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace COVENTAF
{
    static class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //Registramos el DbContext
            #region cadena de conexion
            //declara una variable (conection) que esta llamando a la clase ConectionContext (dicha clase se encuentra en Api.Setting)
           
            string strConection = $"Server={ConectionContext.Server}; Database={ConectionContext.DataBase}; user id={ConectionContext.User}; password={ConectionContext.Password}";                       
            //asignar la cadena de conexion para adonet
            ADONET.strConnect = strConection;
            #endregion



            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           // var services = new ServiceCollection();
           // ConfigureServices(services);

            Application.Run(new frmLogIn2());

           /* using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<frmLogInV>();
                Application.Run(form1);
            }*/
                                
        }

        private static void ConfigureServices(ServiceCollection services)
        {
           /* services.AddSingleton<frmLogInV>()
                    .AddLogging(configure => configure.AddConsole())
                    .AddScoped<IAuthService, AuthService>().AddScoped<LoginController>();
            //.AddScoped<IDataAccessLayer, CDataAccessLayer>();

            services.AddSingleton<ListaUsuarios>()
           .AddLogging(configure => configure.AddConsole())
           .AddScoped<IFactura, FacturaService>().AddScoped<FacturaController>();*/


            //Registramos el DbContext
            #region cadena de conexion
            //declara una variable (conection) que esta llamando a la clase ConectionContext (dicha clase se encuentra en Api.Setting)             
            string strConection = $"Server={ConectionContext.Server}; Database={ConectionContext.DataBase}; user id={ConectionContext.User}; password={ConectionContext.Password}";

            //configurar para la cadena de conexion
            services.AddDbContext<CoreDBContext>(options => options.UseSqlServer(strConection));
            //asignar la cadena de conexion para adonet
            ADONET.strConnect = strConection;
            #endregion
        }

        /* [STAThread]
        static void Main()
        {          
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);
            
            using(var serviceProvider = services.BuildServiceProvider())
            {
                var frmLogin = serviceProvider.GetRequiredService<frmLogInV>();
                var frmListaUsuarios = serviceProvider.GetRequiredService<frmListaUsuarios>();
                Application.Run(frmLogin);
            }



        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>().AddScoped<LoginController>().AddScoped<frmLogInV>();
            services.AddScoped<ISecurityUsuarios, ServiceSecurityUsuario>().AddScoped<SecurityUsuarioController>().AddScoped<frmListaUsuarios>();

            #region cadena de conexion
            //declara una variable (conection) que esta llamando a la clase ConectionContext (dicha clase se encuentra en Api.Setting)
            ConectionContext conection = new ConectionContext();
            string strConection = $"Server={conection.server}; Database={conection.databse}; user id={conection.user}; password={conection.password}";

            //configurar para la cadena de conexion
            services.AddDbContext<CoreDBContext>(options => options.UseSqlServer(strConection));
            //asignar la cadena de conexion para adonet
            ADONET.strConnect = strConection;
            #endregion
        }*/
    }
}
