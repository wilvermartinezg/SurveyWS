using System.Threading.Tasks;
using SurveyWS.Domain.Entities.Survey;
using SurveyWS.Domain.Presentation;
using SurveyWS.Domain.Repository;

namespace SurveyWS.Application.Find
{
    public class SurveyByIdFinder
    {
        private readonly ISurveyRepository _surveyRepository;

        public SurveyByIdFinder(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public async Task<SurveySummary?> Find(long id)
        {
            var entityId = SurveyId.ValueOf(id);
            entityId.Validate();

            return await _surveyRepository.FindSummaryById(entityId);
        }
    }
}