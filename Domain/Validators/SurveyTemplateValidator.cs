using SurveyWS.Domain.Entities.SurveyTemplate;

namespace SurveyWS.Domain.Validators
{
    public class SurveyTemplateValidator
    {
        private readonly SurveyTemplate _entity;

        public SurveyTemplateValidator(SurveyTemplate entity)
        {
            _entity = entity;
        }

        public void Validate()
        {
            _entity.Name.Validate();
            _entity.Description.Validate();
        }
    }
}