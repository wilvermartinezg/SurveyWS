using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SurveyWS.Domain.Entities.Survey;
using SurveyWS.Domain.Entities.SurveyDetail;
using SurveyWS.Domain.Entities.SurveyTemplate;
using SurveyWS.Domain.Entities.SurveyTemplateDetail;
using SurveyWS.Domain.Exceptions;
using SurveyWS.Domain.Repository;

namespace SurveyWS.Application.Create
{
    public class SurveyCreator
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyDetailRepository _surveyDetailRepository;
        private readonly ISurveyTemplateDetailRepository _surveyTemplateDetailRepository;

        public SurveyCreator(
            ISurveyRepository surveyRepository,
            ISurveyDetailRepository surveyDetailRepository,
            ISurveyTemplateDetailRepository surveyTemplateDetailRepository
        )
        {
            _surveyRepository = surveyRepository;
            _surveyDetailRepository = surveyDetailRepository;
            _surveyTemplateDetailRepository = surveyTemplateDetailRepository;
        }

        public async Task<long> Create(SurveyRequest request)
        {
            // 1. Guarda el encabezado de la encuesta
            var entity = new Survey
            {
                SurveyTemplateId = SurveyTemplateId.ValueOf(request.SurveyTemplateId)
            };

            // 2. Validamos que se haya enviado el ID de la encuesta que se esta llenando
            entity.SurveyTemplateId.Validate();

            await _surveyRepository.Create(entity);

            var generatedId = entity.Id;
            generatedId.Validate();

            // 3. Obtenemos los campos del template para validar los campos obligatorios y el tipo de dato de cada campo
            var templateDetail = await _surveyTemplateDetailRepository.FindBySurveyTemplateId(entity.SurveyTemplateId);

            if (templateDetail.Count != request.Details.Count)
            {
                throw new RequiredValueException("No es posible guardar la encuesta, hacen falta campos por llenar");
            }

            var surveyDetails = new List<SurveyDetail>();
            // 4. Guarda las respuestas de la encuesta
            foreach (var detail in request.Details)
            {
                var surveyDetail = new SurveyDetail
                {
                    SurveyId = generatedId,
                    SurveyTemplateDetailId = SurveyTemplateDetailId.ValueOf(detail.SurveyTemplateDetailId),
                    Response = SurveyDetailFieldResponse.ValueOf(detail.Response)
                };

                ValidateDetail(surveyDetail, templateDetail);

                surveyDetails.Add(surveyDetail);
            }

            await _surveyDetailRepository.CreateAll(surveyDetails);

            return generatedId.Value;
        }

        private void ValidateDetail(SurveyDetail detail, List<SurveyTemplateDetail> templateDetails)
        {
            var templateDetailFilterResult = templateDetails
                .Where(it => it.Id.Value == detail.SurveyTemplateDetailId.Value)
                .Select(it => it)
                .ToList();

            if (templateDetailFilterResult.Count == 0)
            {
                throw new RequiredValueException("El campo a llenar no existe");
            }

            var templateDetail = templateDetailFilterResult[0];

            if (templateDetail.IsRequired)
            {
                detail.Response.Validate();
            }

            if (detail.Response.Value == "") return;

            switch (templateDetail.FieldType.Value)
            {
                case "NUMERO":
                    detail.Response.ValidateNumberValue();
                    break;
                case "BOOLEAN":
                    detail.Response.ValidateBooleanValue();
                    break;
                case "FECHA":
                    detail.Response.ValidateDateValue();
                    break;
            }
        }
    }
}