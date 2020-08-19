using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartOHC.Server.Hubs;
using SmartOHC.Server.Persistence;
using SmartOHC.Server.Services;

namespace SmartOHC.Server
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
            services.AddControllers();
            services.AddDbContext<MainDbContext>(contextOptions =>
            {
                // Configuration.GetConnectionString("ConnectionString")
                string connectionString = Configuration.GetValue<string>(key: "ConnectionString");
                contextOptions.UseSqlServer(connectionString);
            });
           //  services.AddAutoMapper(typeof(UserRepository).Assembly);
            services.AddTransient<IBodioService, BodioService>();
            services.AddSignalR();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200");
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            // Cors Configured
             app.UseCors("CorsPolicy");
            // app.UseCors(CorsOptions.AllowAll);


            app.UseAuthorization();


            app.UseSignalR(routes =>
            {
                routes.MapHub<SignalHub>("/signalHub");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
               //  endpoints.MapHub<SignalHub>("/signalHub");
            });

            // signalR service configured
          /* */
        }
    }
}
