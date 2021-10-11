namespace SurveyWS.Api.Controllers.User.Login
{
    public struct LoginRequestJsonDto
    {
        public string? Email { get; set; }
        public string Password { get; set; }
    }
}