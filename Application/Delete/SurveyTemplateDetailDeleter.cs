using System.Threading.Tasks;
using SurveyWS.Domain.Entities.SurveyTemplateDetail;
using SurveyWS.Domain.Repository;

namespace SurveyWS.Application.Delete
{
    public class SurveyTemplateDetailDeleter
    {
        private readonly ISurveyTemplateDetailRepository _surveyTemplateDetailRepository;

        public SurveyTemplateDetailDeleter(ISurveyTemplateDetailRepository surveyTemplateDetailRepository)
        {
            _surveyTemplateDetailRepository = surveyTemplateDetailRepository;
        }

        public async Task Delete(long id)
        {
            var entityId = SurveyTemplateDetailId.ValueOf(id);
            entityId.Validate();

            await _surveyTemplateDetailRepository.Delete(entityId);
        }
    }
}