using SurveyWS.Domain.Entities.SurveyTemplateDetail;

namespace SurveyWS.Domain.Validators
{
    public class SurveyTemplateDetailValidator
    {
        private readonly SurveyTemplateDetail _entity;

        public SurveyTemplateDetailValidator(SurveyTemplateDetail entity)
        {
            _entity = entity;
        }

        public void Validate()
        {
            _entity.FieldName.Validate();
            _entity.FieldDescription.Validate();
            _entity.FieldType.Validate();
        }
    }
}