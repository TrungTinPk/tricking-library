using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrickingLibrary.API.BackgroundServices;
using TrickingLibrary.API.BackgroundServices.VideoEditing;
using TrickingLibrary.Data;

namespace TrickingLibrary.API
{
    public class Startup
    {
        private const string AllCors = "All";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Dev"));
            services.AddHostedService<VideoEditingBackgroundService>();
            services.AddSingleton(_ => Channel.CreateUnbounded<EditVideoMessage>());
            services.AddSingleton<VideoManager>();
            
            services.AddCors(options => options.AddPolicy(AllCors,build =>
            {
                build.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(AllCors);
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}