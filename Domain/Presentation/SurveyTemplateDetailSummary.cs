namespace SurveyWS.Domain.Presentation
{
    public struct SurveyTemplateDetailSummary
    {
        public long Id { get; set; }
        public string FieldName { get; set; }
        public string FieldDescription { get; set; }
        public string FieldType { get; set; }
        public bool IsRequired { get; set; }

        public bool Active { get; set; }
    }
}