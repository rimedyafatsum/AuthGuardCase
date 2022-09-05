using AuthGuardCase.Services.Employee.Services;
using AuthGuardCase.Services.Employee.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthGuardCase.Services.Employee
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                opts.Authority = "http://localhost:5001";
                opts.Audience = "Employee_Service";
                opts.RequireHttpsMetadata = false;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllOrigins",
                builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowAnyOrigin();
                });
            });

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("Read", policy =>
                {
                    policy.RequireClaim("scope", "Employee_Service.Read");
                });

                opts.AddPolicy("Write", policy =>
                {
                    policy.RequireClaim("scope", "Employee_Service.Write");
                });
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthGuardCase.Services.Employee", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthGuardCase.Services.Employee v1"));
            }

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
