using System;

namespace SurveyWS.Api.Controllers.User.Login
{
    public struct LoginResponseJsonDto
    {
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
    }
}