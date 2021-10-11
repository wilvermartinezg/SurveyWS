namespace SurveyWS.Api.Controllers.Survey
{
    public struct SurveyDetailJsonDto
    {
        public long? Id { get; set; }
        public long? SurveyTemplateDetailId { get; set; }
        public string? FieldName { get; set; }
        public string? FieldDescription { get; set; }
        public string? Response { get; set; }
        public string? FieldType { get; set; }
        public bool? IsRequired { get; set; }
    }
}