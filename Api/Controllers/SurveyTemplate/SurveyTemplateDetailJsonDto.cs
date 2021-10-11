namespace SurveyWS.Api.Controllers.SurveyTemplate
{
    public struct SurveyTemplateDetailJsonDto
    {
        public long? Id { get; set; }
        public long? SurveyTemplateId { get; set; }
        public string? FieldName { get; set; }
        public string? FieldDescription { get; set; }
        public string? FieldType { get; set; }
        public bool? IsRequired { get; set; }
    }
}