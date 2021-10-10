using SurveyWS.Domain.Entities.SurveyTemplate;
using SurveyWS.Domain.Entities.SurveyTemplateDetail;
using SurveyWS.Domain.Presentation;
using SurveyWS.Infrastructure.EntityFramework.Entities;

namespace SurveyWS.Infrastructure.Mappers
{
    public class SurveyTemplateDetailMapper
    {
        public SurveyTemplateDetailSummary SummaryFromEntityFramework(SurveyTemplateDetailEf entity)
        {
            return new SurveyTemplateDetailSummary
            {
                Id = entity.Id,
                FieldName = entity.FieldDescription ?? "",
                FieldDescription = entity.FieldDescription ?? "",
                FieldType = entity.FieldType ?? "",
                IsRequired = entity.IsRequired ?? false,
                Active = entity.Active
            };
        }

        public SurveyTemplateDetail FromEntityFramework(SurveyTemplateDetailEf entity)
        {
            return new SurveyTemplateDetail
            {
                Id = SurveyTemplateDetailId.ValueOf(entity.Id),
                SurveyTemplateId = SurveyTemplateId.ValueOf(entity.SurveyTemplate?.Id),
                FieldName = SurveyTemplateDetailFieldName.ValueOf(entity.FieldName),
                FieldDescription = SurveyTemplateDetailFieldDescription.ValueOf(entity.FieldDescription),
                FieldType = SurveyTemplateDetailFieldType.ValueOf(entity.FieldType),
                IsRequired = entity.IsRequired ?? false
            };
        }
    }
}