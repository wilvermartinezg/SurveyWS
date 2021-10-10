using SurveyWS.Domain.Exceptions;

namespace SurveyWS.Domain.Entities.SurveyTemplateDetail
{
    public class SurveyTemplateDetailFieldType
    {
        public string Value { get; }

        public SurveyTemplateDetailFieldType(string value)
        {
            Value = value.Trim();
        }

        public void validate()
        {
            if (Value == "")
            {
                throw new RequiredValueException("El tipo de dato del campo es requerido.");
            }
        }

        public static SurveyTemplateDetailFieldType Empty() => new("");
        public static SurveyTemplateDetailFieldType ValueOf(string? value) => new(value ?? "");
    }
}