using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurveyWS.Application.Delete;

namespace SurveyWS.Api.Controllers.Delete
{
    [ApiController]
    [Route("api/survey-template")]
    public class DeleteSurveyTemplateController : ControllerBase
    {
        private readonly SurveyTemplateDeleter _surveyTemplateDeleter;

        public DeleteSurveyTemplateController(SurveyTemplateDeleter surveyTemplateDeleter)
        {
            _surveyTemplateDeleter = surveyTemplateDeleter;
        }

        [HttpDelete("id:long")]
        public async Task<ActionResult> Index(long id)
        {
            await _surveyTemplateDeleter.Delete(id);

            return Ok();
        }
    }
}