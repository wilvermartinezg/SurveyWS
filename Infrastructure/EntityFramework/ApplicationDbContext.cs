using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveyWS.Infrastructure.EntityFramework.Entities;

namespace SurveyWS.Infrastructure.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<SurveyTemplateEf> SurveyTemplateEfs { get; set; }
        public DbSet<SurveyTemplateDetailEf> SurveyTemplateDetailEfs { get; set; }
        public DbSet<SurveyEf> SurveyEfs { get; set; }
        public DbSet<SurveyDetailEf> SurveyDetailEfs { get; set; }
    }
}