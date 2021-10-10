using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyWS.Domain.Presentation;
using SurveyWS.Domain.Repository;

namespace SurveyWS.Application.Find
{
    public class SurveyTemplateFinder
    {
        private readonly ISurveyTemplateRepository _surveyTemplateRepository;

        public SurveyTemplateFinder(ISurveyTemplateRepository surveyTemplateRepository)
        {
            _surveyTemplateRepository = surveyTemplateRepository;
        }

        public Task<List<SurveyTemplateSummary>> Find()
        {
            return _surveyTemplateRepository.FindAllActive();
        }
    }
}