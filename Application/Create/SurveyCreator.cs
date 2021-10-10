using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyWS.Domain.Entities.Survey;
using SurveyWS.Domain.Entities.SurveyDetail;
using SurveyWS.Domain.Entities.SurveyTemplate;
using SurveyWS.Domain.Entities.SurveyTemplateDetail;
using SurveyWS.Domain.Repository;

namespace SurveyWS.Application.Create
{
    public class SurveyCreator
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyDetailRepository _surveyDetailRepository;

        public SurveyCreator(
            ISurveyRepository surveyRepository,
            ISurveyDetailRepository surveyDetailRepository
        )
        {
            _surveyRepository = surveyRepository;
            _surveyDetailRepository = surveyDetailRepository;
        }

        public async Task<long> Create(SurveyRequest request)
        {
            // 1. Guarda el encabezado de la encuesta
            var entity = new Survey
            {
                SurveyTemplateId = SurveyTemplateId.ValueOf(request.SurveyTemplateId)
            };

            await _surveyRepository.Create(entity);

            var generatedId = entity.Id;
            generatedId.Validate();


            var surveyDetails = new List<SurveyDetail>();
            // 2. Guarda las respuestas de la encuesta
            foreach (var detail in request.Details)
            {
                var surveyDetail = new SurveyDetail
                {
                    SurveyId = generatedId,
                    SurveyTemplateDetailId = SurveyTemplateDetailId.ValueOf(detail.SurveyTemplateDetailId),
                    Response = SurveyDetailFieldResponse.ValueOf(detail.Response)
                };

                surveyDetails.Add(surveyDetail);
            }

            await _surveyDetailRepository.CreateAll(surveyDetails);

            return generatedId.Value;
        }
    }
}