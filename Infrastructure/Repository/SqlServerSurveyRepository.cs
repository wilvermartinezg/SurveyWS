using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyWS.Domain.Entities.Survey;
using SurveyWS.Domain.Presentation;
using SurveyWS.Domain.Repository;
using SurveyWS.Infrastructure.EntityFramework;
using SurveyWS.Infrastructure.EntityFramework.Entities;
using SurveyWS.Infrastructure.Mappers;

namespace SurveyWS.Infrastructure.Repository
{
    public class SqlServerSurveyRepository : ISurveyRepository
    {
        private readonly SurveyMapper _surveyMapper = new();
        private readonly SurveyDetailMapper _surveyDetailMapper = new();
        private readonly ApplicationDbContext _applicationDbContext;

        public SqlServerSurveyRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<SurveySummary>> FindAllActive()
        {
            var result = await _applicationDbContext
                .SurveyEfs
                .Where(entity => entity.Active == true)
                .ToListAsync();

            return result
                .Select(_surveyMapper.SummaryFromEntityFramework)
                .ToList();
        }

        public async Task<SurveySummary?> FindSummaryById(SurveyId id)
        {
            var entity = await _applicationDbContext
                .SurveyEfs
                .FirstOrDefaultAsync(entity => entity.Id == id.Value);

            if (entity == null)
            {
                return null;
            }

            var result = _surveyMapper.SummaryFromEntityFramework(entity);

            var details = await _applicationDbContext
                .SurveyDetailEfs
                .Where(entityDetail =>
                    entityDetail.Survey != null && entityDetail.Survey.Id == id.Value)
                .ToListAsync();

            result.Details = details.Select(_surveyDetailMapper.SummaryFromEntityFramework)
                .ToList();

            return result;
        }

        public async Task<Survey?> FindById(SurveyId id)
        {
            var entity = await _applicationDbContext
                .SurveyEfs
                .FirstOrDefaultAsync(entity => entity.Id == id.Value);

            return entity == null ? null : _surveyMapper.FromEntityFramework(entity);
        }

        public async Task Create(Survey survey)
        {
            var surveyTemplateEntity = await _applicationDbContext
                .SurveyTemplateEfs
                .FirstOrDefaultAsync(entity => entity.Id == survey.SurveyTemplateId.Value);

            var entity = new SurveyEf
            {
                SurveyTemplate = surveyTemplateEntity
            };

            _applicationDbContext.Add(entity);

            await _applicationDbContext.SaveChangesAsync();

            survey.Id = SurveyId.ValueOf(entity.Id);
        }
        
    }
}