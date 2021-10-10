using SurveyWS.Domain.Exceptions;

namespace SurveyWS.Domain.Entities.SurveyTemplateDetail
{
    public class SurveyTemplateDetailFieldDescription
    {
        public string Value { get; }

        public SurveyTemplateDetailFieldDescription(string value)
        {
            Value = value.Trim();
        }

        public void validate()
        {
            if (Value == "")
            {
                throw new RequiredValueException("La descripcion del campo es requerido");
            }
        }

        public static SurveyTemplateDetailFieldDescription Empty() => new("");
        public static SurveyTemplateDetailFieldDescription ValueOf(string? value) => new(value ?? "");
    }
}