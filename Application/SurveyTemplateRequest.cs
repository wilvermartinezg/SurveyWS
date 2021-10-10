using System;
using System.Collections.Generic;

namespace SurveyWS.Application
{
    public struct SurveyTemplateRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<SurveyTemplateDetailRequest> Details { get; set; }
    }
}