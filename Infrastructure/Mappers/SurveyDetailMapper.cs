using SurveyWS.Domain.Entities.SurveyDetail;
using SurveyWS.Domain.Entities.SurveyTemplateDetail;
using SurveyWS.Domain.Presentation;
using SurveyWS.Infrastructure.EntityFramework.Entities;

namespace SurveyWS.Infrastructure.Mappers
{
    public class SurveyDetailMapper
    {
        public SurveyDetailSummary SummaryFromEntityFramework(SurveyDetailEf entity)
        {
            return new SurveyDetailSummary
            {
                Id = entity.Id,
                FieldName = entity.SurveyTemplateDetail?.FieldName ?? "",
                FieldDescription = entity.SurveyTemplateDetail?.FieldDescription ?? "",
                Response = entity.Response,
                FieldType = entity.SurveyTemplateDetail?.FieldType ?? "",
                IsRequired = entity.SurveyTemplateDetail?.IsRequired ?? false
            };
        }

        public SurveyDetail FromEntityFramework(SurveyDetailEf entity)
        {
            return new SurveyDetail
            {
                Id = SurveyDetailId.ValueOf(entity.Id),
                SurveyTemplateDetailId = SurveyTemplateDetailId.ValueOf(entity.SurveyTemplateDetail?.Id),
                Response = SurveyDetailFieldResponse.ValueOf(entity.Response)
            };
        }
    }
}