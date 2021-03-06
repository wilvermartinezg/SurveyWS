using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyWS.Application;
using SurveyWS.Application.Update;

namespace SurveyWS.Api.Controllers.SurveyTemplate.Update
{
    [ApiController]
    [Route("api/survey-template-detail")]
    public class UpdateSurveyTemplateDetailController : ControllerBase
    {
        private readonly SurveyTemplateDetailUpdater _surveyTemplateDetailUpdater;

        public UpdateSurveyTemplateDetailController(SurveyTemplateDetailUpdater surveyTemplateDetailUpdater)
        {
            _surveyTemplateDetailUpdater = surveyTemplateDetailUpdater;
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Index([FromBody] SurveyTemplateDetailJsonDto data)
        {
            var request = new SurveyTemplateDetailRequest
            {
                Id = data.Id ?? 0L,
                FieldName = data.FieldName ?? "",
                FieldDescription = data.FieldDescription ?? "",
                FieldType = data.FieldType ?? "",
                IsRequired = data.IsRequired ?? false
            };

            await _surveyTemplateDetailUpdater.Update(request);

            return Ok();
        }
    }
}