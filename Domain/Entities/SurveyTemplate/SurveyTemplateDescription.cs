using SurveyWS.Domain.Exceptions;

namespace SurveyWS.Domain.Entities.SurveyTemplate
{
    public class SurveyTemplateDescription
    {
        public string Value { get; }

        public SurveyTemplateDescription(string value)
        {
            Value = value.Trim();
        }

        public void Validate()
        {
            if (Value == "")
            {
                throw new RequiredValueException("La descripcion de la encuesta es requerida.");
            }
        }

        public static SurveyTemplateDescription Empty() => new("");
        public static SurveyTemplateDescription ValueOf(string? value) => new(value ?? "");
    }
}