using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyWS.Application;
using SurveyWS.Application.Create;

namespace SurveyWS.Api.Controllers.SurveyTemplate.Create
{
    [ApiController]
    [Route("api/survey-template-detail")]
    public class CreateSurveyTemplateDetailController : ControllerBase
    {
        private readonly SurveyTemplateDetailCreator _surveyTemplateDetailCreator;

        public CreateSurveyTemplateDetailController(SurveyTemplateDetailCreator surveyTemplateDetailCreator)
        {
            _surveyTemplateDetailCreator = surveyTemplateDetailCreator;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<long>> Index([FromBody] SurveyTemplateDetailJsonDto data)
        {
            var request = new SurveyTemplateDetailRequest
            {
                SurveyTemplateId = data.SurveyTemplateId ?? 0L,
                FieldName = data.FieldName ?? "",
                FieldDescription = data.FieldDescription ?? "",
                FieldType = data.FieldType ?? "",
                IsRequired = data.IsRequired ?? false
            };

            var result = await _surveyTemplateDetailCreator.Create(request);

            return Created("", result);
        }
    }
}