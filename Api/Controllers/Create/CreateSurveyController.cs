using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurveyWS.Application;
using SurveyWS.Application.Create;

namespace SurveyWS.Api.Controllers.Create
{
    [ApiController]
    [Route("api/survey")]
    public class CreateSurveyController : ControllerBase
    {
        private readonly SurveyCreator _surveyCreator;

        public CreateSurveyController(SurveyCreator surveyCreator)
        {
            _surveyCreator = surveyCreator;
        }

        [HttpPost]
        public async Task<ActionResult<long>> Index([FromBody] SurveyJsonDto data)
        {
            var request = new SurveyRequest
            {
                SurveyTemplateId = data.SurveyTemplateId ?? 0L,
                Details = (data.Details ?? new List<SurveyDetailJsonDto>())
                    .Select(it => new SurveyDetailRequest
                    {
                        SurveyTemplateDetailId = it.SurveyTemplateDetailId ?? 0L,
                        Response = it.Response ?? ""
                    })
                    .ToList()
            };

            var result = await _surveyCreator.Create(request);

            return CreatedAtRoute("FindSurveyById", result, result);
        }
    }
}