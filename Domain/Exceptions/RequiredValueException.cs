using System;

namespace SurveyWS.Domain.Exceptions
{
    public class RequiredValueException : Exception
    {
        public RequiredValueException(string message) : base(message)
        {
        }
    }
}