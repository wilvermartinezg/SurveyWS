using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SurveyWS.Api.Converters;
using SurveyWS.Application.Create;
using SurveyWS.Application.Find;
using SurveyWS.Domain.Repository;
using SurveyWS.Infrastructure.EntityFramework;
using SurveyWS.Infrastructure.Repository;

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

            // Configura la inyeccion de dependeicas para los casos de uso
            serviceCollection.AddScoped<SurveyTemplateCreator>();
            serviceCollection.AddScoped<SurveyTemplateFinder>();

            serviceCollection.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new() {Title = "Survey rest API", Version = "v1"}));
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
                applicationBuilder.UseSwagger();
                applicationBuilder.UseSwaggerUI(c
                    => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Survey rest API"));
            }

            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseRouting();
            applicationBuilder.UseAuthorization();
            applicationBuilder.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}