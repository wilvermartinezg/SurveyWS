using System.Threading.Tasks;
using SurveyWS.Domain.Entities.SurveyTemplate;
using SurveyWS.Domain.Presentation;
using SurveyWS.Domain.Repository;

namespace SurveyWS.Application.Find
{
    public class SurveyTemplateByIdFinder
    {
        private readonly ISurveyTemplateRepository _surveyTemplateRepository;

        public SurveyTemplateByIdFinder(ISurveyTemplateRepository surveyTemplateRepository)
        {
            _surveyTemplateRepository = surveyTemplateRepository;
        }

        public async Task<SurveyTemplateSummary?> Find(long id)
        {
            var entityId = SurveyTemplateId.ValueOf(id);
            entityId.Validate();

            return await _surveyTemplateRepository.FindSummaryById(entityId);
        }
    }
}