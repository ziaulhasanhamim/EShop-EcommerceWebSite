using EShop.DataAccess.Contexts;
using EShop.Infrastructure.Repositories;
using EShop.Infrastructure.RepositoryImplementations;
using EShop.Infrastructure.ServiceImplementations;
using EShop.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.MainApplication
{
    public class Startup
    {
        IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            ConfigureDataServices(services);
            ConfigureAuthenticationServices(services);
            
        }

        public void ConfigureAuthenticationServices(IServiceCollection services)
        {
            services.AddAuthentication("UserAuth")
                .AddCookie("UserAuth", (options) =>
                {
                    options.Cookie.Name = "AuthSession";
                    options.LoginPath = "/account/login";
                    options.LogoutPath = "/account/logout";
                });
            services.AddTransient<IUserManager, UserManager>();
        }

        public void ConfigureDataServices(IServiceCollection services)
        {
            services.AddDbContext<DefaultDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]));
            services.AddScoped<IProductsRepository, ProductsRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
