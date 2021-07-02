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
using SalesWebMvc.Data;

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
                    options.UseMySql(Configuration.GetConnectionString("SalesWebMvcContext"), builder => builder.MigrationsAssembly("SalesWebMvc")));

            //aqui estamos adcionando uma injeção na classe SeedingService
            //ele registra o nosso serviço no sistema de injeção de dependencia da aplicação
            services.AddScoped<SeedingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //o metodo configure aceita que vc coloque outros parametros dentro dele, como por exemplo SeedingService seedingService
        //se adicionar um parametro dentro configure e essa classe tiver registrada no sistema de dependencia da aplicação
        //que é justamente o caso acima(services.AddScoped<SeedingService>();) automaticamente vai ser resolvido
        //uma isntancia deste objeto
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService)
        {
            //perfil de desenvolvimento
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                seedingService.seed();//populando a DB caso não estiver populada
            }
            //perfil de produção
            else
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
