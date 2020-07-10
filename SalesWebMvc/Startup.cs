using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using SalesWebMvc.Data;
using SalesWebMvc.Services;

namespace SalesWebMvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<SalesWebMvcContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("SalesWebMvcContext"), builder => 
                    builder.MigrationsAssembly("SalesWebMvc"))); // "SalesWebMvcContext" = mesmo nome da classe em "Data"
                                                                 // "SalesWebMvc" = nome do assembly (Projeto)

            services.AddScoped<SeedingService>(); // "services" = argumento do método atual
                                                  // Para registrar o serviço no sistema de injeção de dependência da aplicação
            services.AddScoped<SellerService>();

            services.AddScoped<DepartmentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService)
        { // Acrescido o parâmetro seedingService, que estando a classe registrada no sistema de injeção de dependência da aplicação, 
          // a instância do objeto é automaticamente resolvida (iniciada)
            if (env.IsDevelopment()) // Se estiver no perfil de desenvolvimento faça
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed(); // Chamada do método em "SeedingService" para popular a BD, se não estiver ("if").
            }
            else // Senão, se estiver no perfil de produção faça. Se já foi publicado o aplicativo...
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
