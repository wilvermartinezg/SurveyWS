using System;
using SurveyWS.Domain.Entities.SurveyTemplate;
using SurveyWS.Domain.Presentation;
using SurveyWS.Infrastructure.EntityFramework.Entities;

namespace SurveyWS.Infrastructure.Mappers
{
    public class SurveyTemplateMapper
    {
        public SurveyTemplateSummary SummaryFromEntityFramework(SurveyTemplateEf entity)
        {
            return new SurveyTemplateSummary
            {
                Id = entity.Id,
                Name = entity.Name ?? "",
                Description = entity.Description ?? "",
                CreatedAt = entity.createdAt ?? DateTime.Now,
                CreatedBy = entity.createdBy ?? "",
                ModifiedAt = entity.ModifiedAt ?? DateTime.Now,
                ModifiedBy = entity.ModifiedBy ?? "",
                Active = entity.Active
            };
        }

        public SurveyTemplate FromEntityFramework(SurveyTemplateEf entity)
        {
            return new SurveyTemplate
            {
                Id = SurveyTemplateId.ValueOf(entity.Id),
                Name = SurveyTemplateName.ValueOf(entity.Name),
                Description = SurveyTemplateDescription.ValueOf(entity.Description),
                Active = entity.Active
            };
        }
    }
}