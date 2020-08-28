using EzGame.Common.ViewModel.Settings;
using EzGame.Data.Context;
using EzGame.Data.Interfaces;
using EzGame.Data.UnitOfWork;
using EzGame.IOC.IdentityConfig;
using EzGame.IOC.NotificationConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EzGame.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.AddControllersWithViews().AddToastWithOptions();
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("connectionString").GetSection("defaultConnection").Value);
            });
            services.AddScoped<DatabaseContext, DatabaseContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddCustomIdentityServices();
            services.AddAuthentication().AddGoogle(options =>
                {
                    options.ClientId = Configuration.GetSection("Google").GetSection("ClientId").Value;
                    options.ClientSecret = Configuration.GetSection("Google").GetSection("ClientSecret").Value;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCustomIdentityServices();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "areas",
                    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                      name: "areas",
                      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
