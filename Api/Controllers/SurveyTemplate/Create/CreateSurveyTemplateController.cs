using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyWS.Application;
using SurveyWS.Application.Create;

namespace SurveyWS.Api.Controllers.SurveyTemplate.Create
{
    [ApiController]
    [Route("api/survey-template", Name = "Crear plantilla para encuestas")]
    [Produces("application/json")]
    public class CreateSurveyTemplateController : ControllerBase
    {
        private readonly SurveyTemplateCreator _surveyTemplateCreator;

        public CreateSurveyTemplateController(SurveyTemplateCreator surveyTemplateCreator)
        {
            _surveyTemplateCreator = surveyTemplateCreator;
        }

        /// <summary>
        /// Crea la plantilla de una encuesta, los datos que necesita son, nombre de la encuesta,
        /// descripcion de la encuesta y su listado de campos, el listado de campos debe incluir:
        /// Nombre del campo, Descripcion del campo, Tipo de campo que puede ser TEXTO, BOOLEAN(SI/NO), FECHA
        /// </summary>
        /// <remarks>
        /// Ejemplo de como se debe enviar la data en el body:
        ///
        ///     POST /api/survey-template
        ///     {
        ///        "name": "Nombre de la encuesta",
        ///        "description": "Descripcion de la encuesta",
        ///        "details": [
        ///             {
        ///                 "fieldName": "",
        ///                 "fieldDescription": "",
        ///                 "fieldType": "",
        ///                 "isRequired": true
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="data"></param>
        /// <returns>Retorna el ID de la encuesta</returns>
        /// <response code="201">Si se ha creado exitosamente</response>
        /// <response code="400">Si alguno de los datos requeridos esta en blanco o nulo</response>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

            return CreatedAtRoute("FindTemplateById", new {id = result}, result);
        }
    }
}