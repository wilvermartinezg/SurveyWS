using SurveyWS.Domain.Exceptions;

namespace SurveyWS.Domain.Entities.SurveyTemplateDetail
{
    public class SurveyTemplateDetailId
    {
        public long Value { get; }

        public SurveyTemplateDetailId(long value)
        {
            Value = value;
        }

        public void Validate()
        {
            if (Value <= 0L)
            {
                throw new RequiredValueException("El id del detalle de la encuesta es requerido");
            }
        }

        public static SurveyTemplateDetailId Empty() => new(0L);
        public static SurveyTemplateDetailId ValueOf(long? value) => new(value ?? 0L);
    }
}