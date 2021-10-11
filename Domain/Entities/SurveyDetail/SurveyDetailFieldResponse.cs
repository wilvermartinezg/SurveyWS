using System.Linq;
using System.Text.RegularExpressions;
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

        public void Validate()
        {
            if (Value == "")
            {
                throw new RequiredValueException("La respuesta de la encuesta es requerida.");
            }
        }

        public void ValidateNumberValue()
        {
            var regex = new Regex(@"^[0-9]+(\.[0-9]{1,2})?$");

            if (!regex.IsMatch(Value))
            {
                throw new RequiredValueException("El tipo de dato del campo no es permitodo, solo se permiten numeros");
            }
        }

        public void ValidateBooleanValue()
        {
            var value = Value.ToUpper();
            string[] allowed = {"SI", "NO"};

            if (!allowed.Contains(value))
            {
                throw new RequiredValueException("Tipo de dato no permitido, valores permitidos: SI/NO");
            }
        }

        public void ValidateDateValue()
        {
            // Valida la fecha en formato dd/MM/yyyy
            var regex = new Regex(
                @"(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            );

            if (!regex.IsMatch(Value))
            {
                var message = "El campo no tiene el formato correcto, la fecha debe estar en formato dd/MM/yyyy";
                throw new RequiredValueException(message);
            }
        }

        public static SurveyDetailFieldResponse Empty() => new("");
        public static SurveyDetailFieldResponse ValueOf(string? value) => new(value ?? "");
    }
}