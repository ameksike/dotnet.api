using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using priberam.Models.DTO;
using priberam.Models.DAO;

namespace priberam.Services
{
    public static class StartupSecurity
    {
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseAuthentication();
            app.UseAuthorization();
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration Configuration)
        {
            //... set CorsPolicy 
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });

            //... set form options 
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            //... set security options 
            services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            var Secret = Configuration.GetValue<string>("Security:Secret"); 
            var Issuer = Configuration.GetValue<string>("Security:Issuer");    
            var Audience = Configuration.GetValue<string>("Security:Audience");          
            var Expires = int.Parse(Configuration.GetValue<string>("Security:Expires"));

            //... set security 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = Issuer, 
                     ValidAudience = Audience, 
                     IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Secret) 
                     ),
                     ClockSkew = TimeSpan.Zero
                 }); 

            //... add services 
            services.AddScoped<IdentityServiceInterface, IdentityService>();
            services.AddScoped<AccountServiceInterface, AccountService>();
        }
    }
}

/** 
https://docs.microsoft.com/es-es/aspnet/core/migration/22-to-30?view=aspnetcore-5.0&tabs=visual-studio
*/