using SurveyWS.Domain.Exceptions;

namespace SurveyWS.Domain.Entities.SurveyDetail
{
    public class SurveyDetailFieldResponse
    {
        public string Value { get; }

        public SurveyDetailFieldResponse(string value)
        {
            Value = value.Trim();
        }

        public void validate()
        {
            if (Value == "")
            {
                throw new RequiredValueException("La respuesta de la encuesta es requerida.");
            }
        }

        public static SurveyDetailFieldResponse Empty() => new("");
        public static SurveyDetailFieldResponse ValueOf(string? value) => new(value ?? "");
    }
}