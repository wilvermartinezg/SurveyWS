using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyWS.Domain.Presentation;
using SurveyWS.Domain.Repository;

namespace SurveyWS.Application.Find
{
    public class SurveyFinder
    {
        private readonly ISurveyRepository _surveyRepository;

        public SurveyFinder(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public async Task<List<SurveySummary>> Find()
        {
            return await _surveyRepository.FindAllActive();
        }
    }
}