﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CartWeb.Data;
using CartWeb.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CartWeb
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CartContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("CartConnectionString"));
            });

            services.AddAutoMapper();

            services.AddTransient<CartSeeder>();

            services.AddScoped<ICartRepository, CartRepository>();

            services.AddTransient<IMailService, NullMailService>();

            services.AddMvc()
                    .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseExceptionHandler("/error");

            app.UseStaticFiles();
            app.UseNodeModules(env);

            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default",
                  "{controller}/{action}/{id?}",
                  new { controller = "App", Action = "Index" });
            });
        }
    }
}
