using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
            //somente para debug
            IdentityModelEventSource.ShowPII = true;

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //services.AddAuthentication(o =>
            //{
            //    o.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            //    o.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            //})
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        //options.Authority = "https://localhost:5001";
            //        //options.RequireHttpsMetadata = false;
            //        //options.ApiSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
            //        //options.ApiName = "MobileClient";
            //        //options.RoleClaimType = JwtClaimTypes.Role;
            //        //options.NameClaimType = JwtClaimTypes.Name;
            //        options.RequireHttpsMetadata = false;
            //        options.Authority = "https://localhost:5001";
            //        options.ApiName = "m2m.client";
            //        options.ApiSecret = "511536EF-F270-4058-80CA-1C89C192F69A";
            //    });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
                // base-address of your identityserver
                options.Authority = "https://localhost:5001";

                // if you are using API resources, you can specify the name here
                options.Audience = "https://localhost:5001/resources";

                // IdentityServer emits a typ header by default, recommended extra check
                //options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
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
