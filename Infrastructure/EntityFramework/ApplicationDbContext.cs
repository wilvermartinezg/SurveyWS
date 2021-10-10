using Microsoft.EntityFrameworkCore;
using SurveyWS.Infrastructure.EntityFramework.Entities;

namespace SurveyWS.Infrastructure.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<SurveyTemplateEf> SurveyTemplateEfs { get; set; }
        public DbSet<SurveyTemplateDetailEf> SurveyTemplateDetailEfs { get; set; }
    }
}