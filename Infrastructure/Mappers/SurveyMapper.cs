using System;
using SurveyWS.Domain.Entities.Survey;
using SurveyWS.Domain.Entities.SurveyTemplate;
using SurveyWS.Domain.Presentation;
using SurveyWS.Infrastructure.EntityFramework.Entities;

namespace SurveyWS.Infrastructure.Mappers
{
    public class SurveyMapper
    {
        public Survey FromEntityFramework(SurveyEf entity)
        {
            return new Survey
            {
                Id = SurveyId.ValueOf(entity.Id),
                SurveyTemplateId = SurveyTemplateId.ValueOf(entity.SurveyTemplate?.Id),
                createdAt = entity.CreatedAt ?? DateTime.Now,
                Active = entity.Active
            };
        }

        public SurveySummary SummaryFromEntityFramework(SurveyEf entity)
        {
            return new SurveySummary
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt ?? DateTime.Now,
                SurveyTemplateId = entity.SurveyTemplate?.Id ?? 0L,
                SurveyTemplateName = entity.SurveyTemplate?.Name ?? "",
                SurveyTemplateDescription = entity.SurveyTemplate?.Description ?? ""
            };
        }
    }
}