using System;
using System.Collections.Generic;

namespace SurveyWS.Domain.Presentation
{
    public struct SurveySummary
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public long SurveyTemplateId { get; set; }
        public string SurveyTemplateName { get; set; }
        public string SurveyTemplateDescription { get; set; }
        public List<SurveyDetailSummary> Details { get; set; }
    }
}