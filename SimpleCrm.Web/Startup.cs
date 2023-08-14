using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleCrm.SqlDbServices;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SimpleCrm.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        //private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionStr = Configuration.GetConnectionString("SimpleCrmConnection");

            services.AddMvc();
            services.AddSingleton<IGreeter, ConfigurationGreeter>();
            services.AddScoped<ICustomerData, SqlCustomerData>();

            services.AddDbContext<SimpleCrmDbContext>(options =>
            {
                options.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr));
            });
            services.AddDbContext<CrmIdentityDbContext>(options =>
            {
                options.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr));
            });
            services.AddIdentity<CrmUser, IdentityRole>()
                .AddEntityFrameworkStores<CrmIdentityDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IGreeter greeter)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("Oops!")
                });
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=index}/{id?}"
                );

                //endpoints.MapControllerRoute(
                //    name: "about",
                //    pattern: "About/{phone}",
                //    constraints: new { phone = "^\\d{3}-\\d{3}-\\d{4}$", },
                //    defaults: new { controller = "About", action = "phone", }
                //);

                //endpoints.MapControllerRoute(
                //    name: "about",
                //    pattern: "About/{address}",
                //    defaults: new { controller = "About", action = "address", }
                //);
            });

        // e.g https://localhost:7059/index
            app.Run(ctx => ctx.Response.WriteAsync("Not found!!!"));
        }
    }
}