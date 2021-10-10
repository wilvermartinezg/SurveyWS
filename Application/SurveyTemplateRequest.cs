using System;
using System.Collections.Generic;

namespace SurveyWS.Application
{
    public class SurveyTemplateRequest
    {
        public long Id { get; set; } = 0L;
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        public List<SurveyTemplateDetailRequest> Details { get; set; } = new();
    }
}