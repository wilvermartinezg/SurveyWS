using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyWS.Domain.Entities.SurveyTemplate;
using SurveyWS.Domain.Presentation;

namespace SurveyWS.Domain.Repository
{
    public interface ISurveyTemplateRepository
    {
        public Task<SurveyTemplate?> FindById(SurveyTemplateId id);

        public Task<List<SurveyTemplateSummary>> FindAllActive();

        public Task Create(SurveyTemplate surveyTemplate);

        public Task Update(SurveyTemplate surveyTemplate);

        public Task Delete(SurveyTemplateId id);
    }
}