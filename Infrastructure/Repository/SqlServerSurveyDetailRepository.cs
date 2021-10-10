using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyWS.Domain.Entities.SurveyDetail;
using SurveyWS.Domain.Repository;
using SurveyWS.Infrastructure.EntityFramework;
using SurveyWS.Infrastructure.EntityFramework.Entities;

namespace SurveyWS.Infrastructure.Repository
{
    public class SqlServerSurveyDetailRepository : ISurveyDetailRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SqlServerSurveyDetailRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task CreateAll(List<SurveyDetail> surveyDetails)
        {
            SurveyEf? surveyEntity = null;
            SurveyTemplateDetailEf? surveyTemplateDetailEntity = null;

            foreach (var detail in surveyDetails)
            {
                surveyEntity ??= await _applicationDbContext
                    .SurveyEfs
                    .FirstOrDefaultAsync(entity => entity.Id == detail.SurveyId.Value);

                surveyTemplateDetailEntity ??= await _applicationDbContext
                    .SurveyTemplateDetailEfs
                    .FirstOrDefaultAsync(entity => entity.Id == detail.SurveyTemplateDetailId.Value);

                var entity = new SurveyDetailEf
                {
                    Response = detail.Response.Value,
                    SurveyTemplateDetail = surveyTemplateDetailEntity,
                    Survey = surveyEntity
                };

                _applicationDbContext.Add(entity);
            }

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}