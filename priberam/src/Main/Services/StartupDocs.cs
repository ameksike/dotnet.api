using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace priberam.Services
{

    public static class StartupDocs
    {
        public static string Version = "v1";
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint($"/swagger/{Version}/swagger.json", "API V1");
            });
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSwaggerGen(options => {
                var DocTitle = Configuration.GetValue<string>("Doc:Name");
                var DocVersion = Configuration.GetValue<string>("Doc:Version");
                options.SwaggerDoc(Version, new OpenApiInfo
                {
                    Title = $"API {DocTitle} v{DocVersion}",
                    Version = Version,
                    Description = Configuration.GetValue<string>("Doc:Description"),
                    Contact = new OpenApiContact
                    {
                        Name = Configuration.GetValue<string>("Doc:Contact:Name"),
                        Email = Configuration.GetValue<string>("Doc:Contact:Email"),
                        Url = new Uri(Configuration.GetValue<string>("Doc:Contact:Url")),
                    }
                });
            });
        }
    }
}
