using SurveyWS.Domain.Entities.SurveyTemplate;

namespace SurveyWS.Domain.Entities.SurveyTemplateDetail
{
    public class SurveyTemplateDetail
    {
        public SurveyTemplateDetailId Id { get; set; } = SurveyTemplateDetailId.Empty();

        public SurveyTemplateId SurveyTemplateId { get; set; } = SurveyTemplateId.Empty();

        public SurveyTemplateDetailFieldName FieldName { get; set; } = SurveyTemplateDetailFieldName.Empty();

        public SurveyTemplateDetailFieldDescription FieldDescription { get; set; } =
            SurveyTemplateDetailFieldDescription.Empty();

        public SurveyTemplateDetailFieldType FieldType { get; set; } = SurveyTemplateDetailFieldType.Empty();
        public bool IsRequired { get; set; }
    }
}