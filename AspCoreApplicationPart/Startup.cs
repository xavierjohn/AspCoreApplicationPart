using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace AspCoreApplicationPart
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApplicationPartManager( apm =>
                {
                    List<ApplicationPart> applicationParts = GetModulesOfType(typeof(ModuleType.Happy));
                    foreach(var part in applicationParts)
                    {
                        apm.ApplicationParts.Add(part);
                    }
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        private List<ApplicationPart> GetModulesOfType(Type moduleType)
        {
            var applicationPartsList = new List<ApplicationPart>();
            var dllFiles = Directory.EnumerateFiles($"{AppDomain.CurrentDomain.BaseDirectory}Plugins\\netstandard2.0", "*.dll");
            foreach (var dllFile in dllFiles)
            {
                var dll = Assembly.LoadFile(dllFile);
                if(dll.GetTypes().Any(t => t != moduleType && moduleType.IsAssignableFrom(t)))
                {
                    applicationPartsList.Add(new AssemblyPart(dll));
                }
            }
            return applicationPartsList;
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Plugin Controllers");
            });
        }
    }
}
