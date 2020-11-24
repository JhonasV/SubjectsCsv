using ElectronNET.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SubjectWriterCSV.Data;
using SubjectWriterCSV.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectWriterCSV
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHost )
        {
            Configuration = configuration;
            WebHost = webHost;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHost { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // DI Injection
            string connString = Configuration.GetConnectionString("DefaultConnection");
            if(connString != null)
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connString));
            }
           

            services.AddTransient<ISubjectsRepository, SubjectsRepository>();
            // End DI Injection

            services.AddControllersWithViews();
        }

        private async void CreateWindow()
        {
            var window = await Electron.WindowManager.CreateWindowAsync();
            window.OnClosed += () => {
                Electron.App.Quit();
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Subjects}/{action=Index}/{id?}");
            });

            if (HybridSupport.IsElectronActive)
            {
                CreateWindow();
            }
        }
    }
}
