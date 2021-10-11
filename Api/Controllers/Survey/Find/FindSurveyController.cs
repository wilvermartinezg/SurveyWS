using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurveyWS.Application.Find;
using SurveyWS.Domain.Presentation;

namespace SurveyWS.Api.Controllers.Survey.Find
{
    [ApiController]
    [Route("api/survey")]
    public class FindSurveyController : ControllerBase
    {
        private readonly SurveyFinder _surveyFinder;

        public FindSurveyController(SurveyFinder surveyFinder)
        {
            _surveyFinder = surveyFinder;
        }

        [HttpGet]
        public async Task<ActionResult<SurveySummary>> Index()
        {
            var result = await _surveyFinder.Find();

            return Ok(result);
        }
    }
}