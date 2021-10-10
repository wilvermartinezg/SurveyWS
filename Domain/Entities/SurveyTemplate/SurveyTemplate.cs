namespace SurveyWS.Domain.Entities.SurveyTemplate
{
    public class SurveyTemplate
    {
        public SurveyTemplateId Id { get; set; } = SurveyTemplateId.Empty();
        public SurveyTemplateName Name { get; set; } = SurveyTemplateName.Empty();
        public SurveyTemplateDescription Description { get; set; } = SurveyTemplateDescription.Empty();

        public bool Active { get; set; }
    }
}