using System;
using System.Linq;
using Albums.Controllers;
using Api.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace StoredProcHel
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"}); });

            services.AddScoped<AlbumsController, AlbumsController>();
            services.AddScoped<PlaylistsController, PlaylistsController>();

            RegisterControllersFromEveryProjects(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            
            app.UseMvc();
        }

        private void RegisterControllersFromEveryProjects(IServiceCollection services)
        {
            var Controllers =
                from a in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
                from t in a.GetTypes()
                let attributes = t.GetCustomAttributes(typeof(ControllerAttribute), true)
                where attributes?.Length > 0
                select new { Type = t };

            var ControllersList = Controllers.ToList();
            System.Diagnostics.Debug.WriteLine($"Found {ControllersList.Count} controllers");
            //Logging.Info($"Found {ControllersList.Count} controllers");

            // register them
            foreach (var Controller in ControllersList)
            {
                //Logging.Info($"[Controller] Registering {Controller.Type.Name}");
                System.Diagnostics.Debug.WriteLine($"[Controller] Registering {Controller.Type.Name}");
                services.AddMvc()
                    .AddJsonOptions(Options => Options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                    .AddApplicationPart(Controller.Type.Assembly);
            }
        }
    }
}