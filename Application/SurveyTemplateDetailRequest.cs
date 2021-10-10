namespace SurveyWS.Application
{
    public class SurveyTemplateDetailRequest
    {
        public long Id { get; set; } = 0L;
        public string FieldName { get; set; } = "";
        public string FieldDescription { get; set; } = "";
        public string FieldType { get; set; } = "";
        public bool IsRequired { get; set; }
    }
}