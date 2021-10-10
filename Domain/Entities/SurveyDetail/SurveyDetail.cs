using SurveyWS.Domain.Entities.Survey;
using SurveyWS.Domain.Entities.SurveyTemplateDetail;

namespace SurveyWS.Domain.Entities.SurveyDetail
{
    public class SurveyDetail
    {
        public SurveyId SurveyId { get; set; } = SurveyId.Empty();
        public SurveyDetailId Id { get; set; } = SurveyDetailId.Empty();
        public SurveyTemplateDetailId SurveyTemplateDetailId { get; set; } = SurveyTemplateDetailId.Empty();
        public SurveyDetailFieldResponse Response { get; set; } = SurveyDetailFieldResponse.Empty();
    }
}