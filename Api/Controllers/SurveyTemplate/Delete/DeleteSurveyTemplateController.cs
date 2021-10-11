using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyWS.Application.Delete;

namespace SurveyWS.Api.Controllers.SurveyTemplate.Delete
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

        [HttpDelete("{id:long}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Index(long id)
        {
            await _surveyTemplateDeleter.Delete(id);

            return Ok();
        }
    }
}