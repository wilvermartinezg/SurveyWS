using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyWS.Domain.Entities.SurveyTemplateDetail;
using SurveyWS.Domain.Repository;
using SurveyWS.Infrastructure.EntityFramework;
using SurveyWS.Infrastructure.EntityFramework.Entities;

namespace SurveyWS.Infrastructure.Repository
{
    public class SqlServerSurveyTemplateDetailRepository : ISurveyTemplateDetailRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SqlServerSurveyTemplateDetailRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task CreateAll(List<SurveyTemplateDetail> surveyTemplateDetails)
        {
            SurveyTemplateEf? surveyTemplateEntity = null;

            foreach (var detail in surveyTemplateDetails)
            {
                surveyTemplateEntity ??= await _applicationDbContext
                    .SurveyTemplateEfs
                    .FirstOrDefaultAsync(entity => entity.Id == detail.SurveyTemplateId.Value);

                var entity = new SurveyTemplateDetailEf
                {
                    FieldName = detail.FieldName.Value,
                    FieldDescription = detail.FieldDescription.Value,
                    FieldType = detail.FieldType.Value,
                    SurveyTemplate = surveyTemplateEntity
                };

                _applicationDbContext.Add(entity);
            }

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}