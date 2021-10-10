using System.Threading.Tasks;
using SurveyWS.Domain.Repository;

namespace SurveyWS.Application.Update
{
    public class SurveyTemplateDetailUpdater
    {
        private readonly ISurveyTemplateDetailRepository _surveyTemplateDetailRepository;

        public SurveyTemplateDetailUpdater(ISurveyTemplateDetailRepository surveyTemplateDetailRepository)
        {
            _surveyTemplateDetailRepository = surveyTemplateDetailRepository;
        }

        public async Task Update(SurveyTemplateDetailRequest request)
        {
            
        }
    }
}