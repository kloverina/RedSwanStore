using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using RedSwanStore.Data;
using RedSwanStore.Data.Interfaces;
using RedSwanStore.Data.Repositories;

namespace RedSwanStore
{
    public class Startup
    {
        private IConfigurationRoot dbConfig;


        public Startup(IWebHostEnvironment hostEnv)
        {
            dbConfig = new ConfigurationBuilder()
                .SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json")
                .Build();
        }
        
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RedSwanStoreDBContent>(
                options => options.UseSqlServer(dbConfig.GetConnectionString("DefaultConnection"))
            );
            
            services.AddTransient<IPriceCategoryRepo, PriceCategoryRepo>();
            services.AddTransient<IFeatureRepo, FeatureRepo>();
            services.AddTransient<IGenreRepo, GenreRepo>();
            services.AddTransient<IGameRepo, GameRepo>();
            services.AddTransient<IUserRepo, UserRepo>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
                options.LoginPath = new PathString("/login");
            });
            
            services.AddControllersWithViews();
        }

        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                RedSwanStoreDBContent dbContent = scope.ServiceProvider.GetRequiredService<RedSwanStoreDBContent>();
                DBInitializer.Initialize(dbContent);
            }
        }
    }
}