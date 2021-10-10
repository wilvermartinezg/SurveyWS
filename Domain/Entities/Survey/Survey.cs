using System;
using SurveyWS.Domain.Entities.SurveyTemplate;

namespace SurveyWS.Domain.Entities.Survey
{
    public class Survey
    {
        public SurveyId Id { get; set; } = SurveyId.Empty();
        public SurveyTemplateId SurveyTemplateId { get; set; } = SurveyTemplateId.Empty();
        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}