using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyWS.Domain.Entities.Survey;
using SurveyWS.Domain.Presentation;

namespace SurveyWS.Domain.Repository
{
    public interface ISurveyRepository
    {
        public Task<List<SurveySummary>> FindAllActive();
        public Task<SurveySummary?> FindSummaryById(SurveyId id);
        public Task<Survey?> FindById(SurveyId id);
        public Task Create(Survey survey);
    }
}