using MaquinasUbicacion.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace XUnitTestProject3
{
    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddDbContext<maqUbiContext>(options =>
            options.UseInMemoryDatabase("prueba"));



            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        { 
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var context = app.ApplicationServices.GetService<maqUbiContext>();
            AddTestData(context);

            app.UseMvc();
        }

        private static void AddTestData(maqUbiContext context)
        {
            context.Cliente.Add(new Cliente { id = 1, nombre = "Cliente Test 1" });
            context.Cliente.Add(new Cliente { id = 2, nombre = "Cliente Test 2" });
            context.Cliente.Add(new Cliente { id = 3, nombre = "Cliente Test 3" });
            context.Cliente.Add(new Cliente { id = 4, nombre = "Cliente Test 4" });

            context.Ubicacion.Add(new Ubicacion { id = 1, ubicacion = "Ubicacion Test 1", idCliente = 1 });
            context.Ubicacion.Add(new Ubicacion { id = 2, ubicacion = "Ubicacion Test 2", idCliente = 2 });
            context.Ubicacion.Add(new Ubicacion { id = 3, ubicacion = "Ubicacion Test 3", idCliente = 3 });
            context.Ubicacion.Add(new Ubicacion { id = 4, ubicacion = "Ubicacion Test 4", idCliente = 4 });

            context.Maquina.Add(new Maquina { id = 1, modelo = "Modelo 1", nSerie = "123456", tipo = "Tipo 1", idCliente = 1, idUbicacion = 1 });
            context.Maquina.Add(new Maquina { id = 2, modelo = "Modelo 2", nSerie = "98745z", tipo = "Tipo 2", idCliente = 2, idUbicacion = 2 });
            context.Maquina.Add(new Maquina { id = 3, modelo = "Modelo 3", nSerie = "79813w", tipo = "Tipo 3", idCliente = 3, idUbicacion = 3 });
            context.Maquina.Add(new Maquina { id = 4, modelo = "Modelo 4", nSerie = "36985t", tipo = "Tipo 4", idCliente = 4, idUbicacion = 4 });

            context.SaveChanges();
        }
    }
}
