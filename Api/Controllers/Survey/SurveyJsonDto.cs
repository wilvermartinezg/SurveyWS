using System;
using System.Collections.Generic;

namespace SurveyWS.Api.Controllers.Survey
{
    public struct SurveyJsonDto
    {
        public long? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? SurveyTemplateId { get; set; }
        public string? SurveyTemplateName { get; set; }
        public string? SurveyTemplateDescription { get; set; }
        public List<SurveyDetailJsonDto>? Details { get; set; }
    }
}