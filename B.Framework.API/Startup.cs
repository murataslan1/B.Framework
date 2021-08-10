using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B.Framework.Application.Attribute.Extensions;
using B.Framework.Application.Security.JWT;
using B.Framework.Application.Security.User;
using B.Framework.Domain.Shared.Attribute;
using B.Framework.Domain.Shared.Enums.Claims;
using B.Framework.Domain.Shared.Security;
using B.Framework.Domain.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace B.Framework.API
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
            services.AddSwaggerGen(setup =>
            {
               
                
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = JWTBaseDefinations.OpenApiSecurityScheme.Scheme,
                    BearerFormat = JWTBaseDefinations.OpenApiSecurityScheme.BearerFormat,
                    Name = JWTBaseDefinations.OpenApiSecurityScheme.Name,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = JWTBaseDefinations.OpenApiSecurityScheme.Description,

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

            });
            services.AddScoped<IUserService, UserService>();

            services.AddControllers();
            var jwtConfigurationBase = new JWTConfigurationBase();
            var jwtSection = Configuration.GetSection(JWTBaseDefinations.ConfigurationName);
            jwtSection.Bind(jwtConfigurationBase);
            services.Configure<JWTConfigurationBase>(jwtSection);
            services.AddScoped<BTokenHandler, BTokenHandler>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfigurationBase.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtConfigurationBase.Audience,
                    ValidIssuer = jwtConfigurationBase.Issuer,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "B.Framework.API v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.Use(async (context, func) =>
            {

                var tenantId = context.User.Claims.FirstOrDefault(b => b.Type == BEClaimTypes.TenantId.GetBEnumValue<BAttributeBase>())?.Value;
                await func.Invoke();
            });
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}