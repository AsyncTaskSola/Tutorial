using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tutorial_Web.Data;
using Tutorial_Web.Model;
using Tutorial_Web.Services;

namespace Tutorial_Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();
            //var ass = _configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<DataContext>(option =>
            {
          
                option.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddSingleton<IWelcomeService, WelcomeService>();
            //services.AddSingleton<IRepository<Student>, InMemoryRepository>();
            services.AddScoped<IRepository<Student>, EfCoreRepository>();
        }

        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            IConfiguration iConfiguration,
            IWelcomeService welcomeService,
            ILogger<Startup> Logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            #region Check1

            //app.Use(next => {
            //    Logger.LogInformation("app.Use().....");
            //    return async httpcontext =>
            //    {
            //        Logger.LogInformation(".....async httpcontext");
            //        if (httpcontext.Request.Path.StartsWithSegments("/first"))
            //        {
            //            Logger.LogInformation(".....first");
            //            await httpcontext.Response.WriteAsync("first111");
            //        }
            //        else
            //        {
            //            Logger.LogInformation("next(httpcontext)");
            //            await next(httpcontext);
            //        }
            //    };
            //});

            //app.UseWelcomePage(new WelcomePageOptions
            //{
            //    Path = "/Welcome"
            //});

            #endregion

            #region Check2

            //app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseFileServer();

            #endregion

            //app.UseMvcWithDefaultRoute() ;

            app.UseMvc(Builder =>
            {
                Builder.MapRoute("defult", "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(async (context) =>
            {
                //throw new Exception("error");
                var welcome = /*iConfiguration["Welcome"];*/welcomeService.getMessage();
                await context.Response.WriteAsync(welcome);
            });
        }
    }
}
