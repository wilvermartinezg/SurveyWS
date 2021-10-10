using SurveyWS.Domain.Entities.SurveyTemplateDetail;

namespace SurveyWS.Domain.Entities.SurveyDetail
{
    public class SurveyDetail
    {
        public SurveyDetailId Id { get; set; } = SurveyDetailId.Empty();
        public SurveyTemplateDetailId SurveyTemplateDetailId { get; set; } = SurveyTemplateDetailId.Empty();
        public SurveyDetailFieldResponse Response { get; set; } = SurveyDetailFieldResponse.Empty();
    }
}