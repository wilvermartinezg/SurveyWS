using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyWS.Application;
using SurveyWS.Application.Update;

namespace SurveyWS.Api.Controllers.SurveyTemplate.Update
{
    [ApiController]
    [Route("api/survey-template")]
    public class UpdateSurveyTemplateController : ControllerBase
    {
        private readonly SurveyTemplateUpdater _surveyTemplateUpdater;

        public UpdateSurveyTemplateController(SurveyTemplateUpdater surveyTemplateUpdater)
        {
            _surveyTemplateUpdater = surveyTemplateUpdater;
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Index([FromBody] SurveyTemplateJsonDto data)
        {
            var request = new SurveyTemplateRequest
            {
                Id = data.Id ?? 0L,
                Name = data.Name ?? "",
                Description = data.Description ?? ""
            };

            await _surveyTemplateUpdater.Update(request);

            return Ok();
        }
    }
}