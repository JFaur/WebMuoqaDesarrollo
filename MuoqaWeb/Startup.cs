using Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;                             // Para clases generales como Exception y TimeSpan
using MuoqaBD;
using MuoqaBackend.ToBD;

namespace MuoqaWeb
{
    public class Startup
    { 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services) {
            try
            {
                services.AddSingleton<IConfiguration>(Configuration);//Configuracion por ejemplo de appsetings.json

                var connectionString2 = Environment.GetEnvironmentVariable("MuoqaBD");
                var getCookieUrl = Configuration["CookiesUrl"];
                services.AddDbContext<MuoqaBDConf>(options =>
                options.UseMySql(connectionString2, ServerVersion.AutoDetect(connectionString2)));
                services.AddScoped<ValidateSession>();
                services.AddScoped<UpdatePrice>();
                services.AddMemoryCache();
                services.AddControllersWithViews();
                //services.AddSession(options =>
                //{
                //    options.IdleTimeout = TimeSpan.FromMinutes(30); //Tiempo de inactividad antes de que caduque la sesión
                //    options.Cookie.IsEssential = true;
                //    options.Cookie.HttpOnly = true; //La cookie solo es accesible a través del servidor (no accesible mediante JavaScript)
                //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; //La cookie solo se enviará a través de conexiones HTTPS
                //    options.Cookie.SameSite = SameSiteMode.Strict; //Restringe la cookie para evitar ataques CSRF
                //});
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error{ex.Message}");
                Log.Error($"Error{ex.Message}");
            }
        }
    }
}
