using System.Collections.Generic;

namespace SurveyWS.Application
{
    public struct SurveyRequest
    {
        public long SurveyId { get; set; }
        public long SurveyTemplateId { get; set; }
        public List<SurveyDetailRequest> Details { get; set; }
    }
}