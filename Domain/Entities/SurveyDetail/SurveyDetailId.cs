using SurveyWS.Domain.Exceptions;

namespace SurveyWS.Domain.Entities.SurveyDetail
{
    public class SurveyDetailId
    {
        public long Value { get; }

        public SurveyDetailId(long value)
        {
            Value = value;
        }

        public void validate()
        {
            if (Value <= 0L)
            {
                throw new RequiredValueException("El id del detalle de la encuesta es requerido");
            }
        }

        public static SurveyDetailId Empty() => new(0L);
        public static SurveyDetailId ValueOf(long? value) => new(value ?? 0L);
    }
}