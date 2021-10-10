using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurveyWS.Application;
using SurveyWS.Application.Create;

namespace SurveyWS.Api.Controllers.Create
{
    [ApiController]
    [Route("api/survey-template")]
    public class CreateSurveyTemplateController : ControllerBase
    {
        private readonly SurveyTemplateCreator _surveyTemplateCreator;

        public CreateSurveyTemplateController(SurveyTemplateCreator surveyTemplateCreator)
        {
            _surveyTemplateCreator = surveyTemplateCreator;
        }

        [HttpPost]
        public async Task<ActionResult<long>> Index([FromBody] SurveyTemplateJsonDto data)
        {
            var request = new SurveyTemplateRequest
            {
                Name = data.Name ?? "",
                Description = data.Description ?? "",
                Details = (data.Details ?? new List<SurveyTemplateDetailJsonDto>())
                    .Select(it => new SurveyTemplateDetailRequest
                    {
                        FieldName = it.FieldName ?? "",
                        FieldDescription = it.FieldDescription ?? "",
                        FieldType = it.FieldType ?? "",
                        IsRequired = it.IsRequired ?? false
                    })
                    .ToList()
            };

            var result = await _surveyTemplateCreator.Create(request);

            return Created("FindById", result);
        }
    }
}