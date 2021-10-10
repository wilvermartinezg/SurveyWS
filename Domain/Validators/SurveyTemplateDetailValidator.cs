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
            _entity.FieldName.validate();
            _entity.FieldDescription.validate();
            _entity.FieldType.validate();
        }
    }
}