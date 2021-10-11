using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurveyWS.Application.Find;
using SurveyWS.Domain.Presentation;

namespace SurveyWS.Api.Controllers.Survey.Find
{
    [ApiController]
    [Route("api/survey")]
    public class FindSurveyByIdController : ControllerBase
    {
        private readonly SurveyByIdFinder _surveyByIdFinder;

        public FindSurveyByIdController(SurveyByIdFinder surveyByIdFinder)
        {
            _surveyByIdFinder = surveyByIdFinder;
        }

        [HttpGet("{id:long}", Name = "FindSurveyById")]
        public async Task<ActionResult<SurveySummary?>> Index(long id)
        {
            var result = await _surveyByIdFinder.Find(id);

            return Ok(result);
        }
    }
}