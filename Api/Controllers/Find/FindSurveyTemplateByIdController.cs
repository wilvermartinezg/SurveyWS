using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurveyWS.Application.Find;
using SurveyWS.Domain.Presentation;

namespace SurveyWS.Api.Controllers.Find
{
    [ApiController]
    [Route("api/survey-template")]
    public class FindSurveyTemplateByIdController : ControllerBase
    {
        private readonly SurveyTemplateByIdFinder _surveyTemplateByIdFinder;

        public FindSurveyTemplateByIdController(SurveyTemplateByIdFinder surveyTemplateByIdFinder)
        {
            _surveyTemplateByIdFinder = surveyTemplateByIdFinder;
        }

        [HttpGet("id:long"), ActionName("FindById")]
        public async Task<ActionResult<SurveyTemplateSummary>> Index(long id)
        {
            var result = await _surveyTemplateByIdFinder.Find(id);

            return Ok(result);
        }
    }
}