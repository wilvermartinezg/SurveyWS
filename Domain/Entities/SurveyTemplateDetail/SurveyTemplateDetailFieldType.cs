using System.Linq;
using SurveyWS.Domain.Exceptions;

namespace SurveyWS.Domain.Entities.SurveyTemplateDetail
{
    public class SurveyTemplateDetailFieldType
    {
        private readonly string[] _allowedTypes = {"TEXTO", "NUMERO", "BOOLEAN", "FECHA"};

        public string Value { get; }

        public SurveyTemplateDetailFieldType(string value)
        {
            Value = value.Trim();
        }

        public void Validate()
        {
            if (Value == "")
            {
                throw new RequiredValueException("El tipo de dato del campo es requerido.");
            }

            if (!_allowedTypes.Contains(Value))
            {
                var allowedTypes = string.Join(",", _allowedTypes);
                var message = $"El tipo de campo no es valido, los tipos permitidos son {allowedTypes}";
                throw new RequiredValueException(message);
            }
        }

        public static SurveyTemplateDetailFieldType Empty() => new("");
        public static SurveyTemplateDetailFieldType ValueOf(string? value) => new(value ?? "");
    }
}