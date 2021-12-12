using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using AutoMapper;

using DutchTreat.Services;
using DutchTreat.Data;
using DutchTreat.Data.Entities;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration _config;
        
        // Constructor
        public Startup(IConfiguration config)
        {
            this._config = config;
        }        
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // User Identity
            services.AddIdentity<StoreUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequiredUniqueChars = 0;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<DutchContext>();

            // Authentication Method
            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _config["Tokens:Issuer"],
                        ValidAudience = _config["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
                    };
                });

            // Database
            services.AddDbContext<DutchContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("DutchConnectionString"));
            });

            // Activate Service
            services.AddTransient<DutchSeeder>();
            services.AddTransient<StoreUser>();
            services.AddTransient<IMailService, NullMailService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IDutchRepository, DutchRepository>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            //app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseNodeModules(env);

            app.UseAuthentication();
            //app.UseAuthorization(); // For ASP.NET Core >= 3.

            //app.UseRouting(); // For ASP.NET Core >= 3.

            app.UseMvc(cfg =>
            {
                cfg.MapRoute(
                    "Fallback",
                    "{controller}/{action}/{id?}",
                    new { controller = "app", Action = "index" }
                );
            });
        }
    }
}
