namespace SurveyWS.Domain.Presentation
{
    public struct SurveyDetailSummary
    {
        public long Id { get; set; }
        public string FieldName { get; set; }
        public string FieldDescription { get; set; }
        public string Response { get; set; }
        public string FieldType { get; set;}
        public bool IsRequired { get; set; }
    }
}