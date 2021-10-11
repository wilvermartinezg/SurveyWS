using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SurveyWS.Api.Exceptions;
using SurveyWS.Application.Create;
using SurveyWS.Application.Delete;
using SurveyWS.Application.Find;
using SurveyWS.Application.Update;
using SurveyWS.Domain.Repository;
using SurveyWS.Infrastructure.EntityFramework;
using SurveyWS.Infrastructure.Repository;
using DateTimeConverter = SurveyWS.Api.Converters.DateTimeConverter;

namespace SurveyWS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter()));

            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configura la inyeccion de dependencias de las implementaciones de los repositorios de dominio
            serviceCollection.AddScoped<ISurveyTemplateRepository, SqlServerSurveyTemplateRepository>();
            serviceCollection.AddScoped<ISurveyTemplateDetailRepository, SqlServerSurveyTemplateDetailRepository>();
            serviceCollection.AddScoped<ISurveyRepository, SqlServerSurveyRepository>();
            serviceCollection.AddScoped<ISurveyDetailRepository, SqlServerSurveyDetailRepository>();

            // Configura la inyeccion de dependeicas para los casos de uso
            serviceCollection.AddScoped<SurveyTemplateCreator>();
            serviceCollection.AddScoped<SurveyTemplateFinder>();
            serviceCollection.AddScoped<SurveyTemplateUpdater>();
            serviceCollection.AddScoped<SurveyTemplateDeleter>();
            serviceCollection.AddScoped<SurveyTemplateByIdFinder>();
            serviceCollection.AddScoped<SurveyTemplateDetailUpdater>();
            serviceCollection.AddScoped<SurveyTemplateDetailDeleter>();
            serviceCollection.AddScoped<SurveyTemplateDetailCreator>();
            serviceCollection.AddScoped<SurveyCreator>();
            serviceCollection.AddScoped<SurveyByIdFinder>();
            serviceCollection.AddScoped<SurveyFinder>();

            serviceCollection
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"]))
                    });

            serviceCollection.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            serviceCollection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Title = "Api rest para sistema de encuestas",
                    Description = "Api rest para sistema de encuestas desarrollado sobre ASP.NET Core 6.0",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Wilver Martinez",
                        Email = "wilver.martinezg@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/wilvermartinezg/")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
            }

            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(c
                => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Survey rest API"));

            applicationBuilder.ConfigureCustomExceptionMiddleware();

            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseRouting();
            applicationBuilder.UseAuthorization();
            applicationBuilder.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}