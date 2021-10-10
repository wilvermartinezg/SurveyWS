using SurveyWS.Domain.Exceptions;

namespace SurveyWS.Domain.Entities.SurveyTemplateDetail
{
    public class SurveyTemplateDetailFieldName
    {
        public string Value { get; }

        public SurveyTemplateDetailFieldName(string value)
        {
            Value = value.Trim();
        }

        public void validate()
        {
            if (Value == "")
            {
                throw new RequiredValueException("El nombre del campo es requerido");
            }
        }

        public static SurveyTemplateDetailFieldName Empty() => new("");
        public static SurveyTemplateDetailFieldName ValueOf(string? value) => new(value ?? "");
    }
}