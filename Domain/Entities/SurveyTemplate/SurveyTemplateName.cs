using SurveyWS.Domain.Exceptions;

namespace SurveyWS.Domain.Entities.SurveyTemplate
{
    public class SurveyTemplateName
    {
        public string Value { get; }

        public SurveyTemplateName(string value)
        {
            this.Value = value.Trim();
        }

        public void Validate()
        {
            if (Value == "")
            {
                throw new RequiredValueException("El nombre de la encuesta es requerido.");
            }
        }

        public static SurveyTemplateName Empty() => new("");
        public static SurveyTemplateName ValueOf(string? value) => new(value ?? "");
    }
}