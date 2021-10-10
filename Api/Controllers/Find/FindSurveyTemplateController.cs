using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurveyWS.Application.Find;
using SurveyWS.Domain.Presentation;

namespace SurveyWS.Api.Controllers.Find
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
        public async Task<ActionResult<SurveyTemplateSummary>> Index()
        {
            var result = await _surveyTemplateFinder.Find();

            return Ok(result);
        }
    }
}