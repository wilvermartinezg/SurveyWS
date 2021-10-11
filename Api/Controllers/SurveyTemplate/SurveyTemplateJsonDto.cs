using System;
using System.Collections.Generic;

namespace SurveyWS.Api.Controllers.SurveyTemplate
{
    public struct SurveyTemplateJsonDto
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? Active { get; set; }

        public List<SurveyTemplateDetailJsonDto>? Details { get; set; }
    }
}