using SurveyWS.Domain.Exceptions;

namespace SurveyWS.Domain.Entities.SurveyTemplate
{
    public class SurveyTemplateId
    {
        public long Value { get; }

        private SurveyTemplateId(long value)
        {
            Value = value;
        }

        public void Validate()
        {
            if (Value <= 0L)
            {
                throw new RequiredValueException("El id de la encuesta es requerido.");
            }
        }

        public static SurveyTemplateId Empty() => new(0L);
        public static SurveyTemplateId ValueOf(long? value) => new(value ?? 0L);
    }
}