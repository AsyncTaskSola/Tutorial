using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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

            #region IdentityService
            //配置identity数据库这一块
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(
                    _configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Tutorial Web")));
            //注册服务
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<IdentityDbContext>();


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;//需要数字
                options.Password.RequireLowercase = false;//需要小写字母
                options.Password.RequireNonAlphanumeric = false;//需要非数值类行
                options.Password.RequireUppercase = false;//需要大写
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;

                //Lockout settings.
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });


            #endregion
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

            app.UseAuthentication();
            #region Check2

            //app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/node_modules",
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath,"node_modules"))
                
            });

            //app.UseFileServer();

            #endregion

            //app.UseMvcWithDefaultRoute() ;


            app.UseMvc(Builder =>
            {
                Builder.MapRoute("defult", "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Run(async (context) =>
            //{
            //    //throw new Exception("error");
            //    var welcome = /*iConfiguration["Welcome"];*/welcomeService.getMessage();
            //    await context.Response.WriteAsync(welcome);
            //});
        }
    }
}
