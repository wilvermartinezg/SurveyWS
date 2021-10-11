using System;
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
            var resultList = await (
                    from t0 in _applicationDbContext.SurveyEfs
                    join t1 in _applicationDbContext.SurveyTemplateEfs on t0.SurveyTemplate.Id equals t1.Id
                    where t0.Id == id.Value
                    select new SurveySummary
                    {
                        Id = t0.Id,
                        CreatedAt = t0.CreatedAt ?? DateTime.Now,
                        SurveyTemplateId = t1.Id,
                        SurveyTemplateName = t1.Name ?? "",
                        SurveyTemplateDescription = t1.Description ?? ""
                    }
                )
                .ToListAsync();

            if (resultList.Count == 0)
            {
                return null;
            }

            var data = resultList[0];

            // Obtiene los detalles
            data.Details = await (
                    from t0 in _applicationDbContext.SurveyDetailEfs
                    join t1 in _applicationDbContext.SurveyTemplateDetailEfs
                        on t0.SurveyTemplateDetail.Id equals t1.Id
                    where t0.Survey.Id == id.Value
                    select new SurveyDetailSummary
                    {
                        Id = t0.Id,
                        FieldName = t1.FieldName ?? "",
                        FieldDescription = t1.FieldDescription ?? "",
                        Response = t0.Response ?? "",
                        FieldType = t1.FieldType ?? "",
                        IsRequired = t1.IsRequired ?? false
                    }
                )
                .ToListAsync();

            return data;
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