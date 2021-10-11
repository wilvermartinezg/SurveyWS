using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyWS.Application.Find;
using SurveyWS.Domain.Presentation;

namespace SurveyWS.Api.Controllers.SurveyTemplate.Find
{
    [ApiController]
    [Route("api/survey-template")]
    public class FindSurveyTemplateController : ControllerBase
    {
        private readonly SurveyTemplateFinder _surveyTemplateFinder;

        public FindSurveyTemplateController(SurveyTemplateFinder surveyTemplateFinder)
        {
            _surveyTemplateFinder = surveyTemplateFinder;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<SurveyTemplateSummary>> Index()
        {
            var result = await _surveyTemplateFinder.Find();

            return Ok(result);
        }
    }
}