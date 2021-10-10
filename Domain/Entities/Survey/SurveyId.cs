using SurveyWS.Domain.Exceptions;

namespace SurveyWS.Domain.Entities.Survey
{
    public class SurveyId
    {
        public long Value { get; }

        public SurveyId(long value)
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

        public static SurveyId Empty() => new(0L);
        public static SurveyId ValueOf(long? value) => new(value ?? 0L);
    }
}