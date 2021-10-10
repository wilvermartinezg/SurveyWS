using System.Threading.Tasks;
using SurveyWS.Domain.Entities.SurveyTemplate;
using SurveyWS.Domain.Repository;

namespace SurveyWS.Application.Delete
{
    public class SurveyTemplateDeleter
    {
        private readonly ISurveyTemplateRepository _surveyTemplateRepository;

        public SurveyTemplateDeleter(ISurveyTemplateRepository surveyTemplateRepository)
        {
            _surveyTemplateRepository = surveyTemplateRepository;
        }

        public async Task Delete(long id)
        {
            // 1. Validamos el ID de la entidad a actualizar
            var entityId = SurveyTemplateId.ValueOf(id);
            entityId.Validate();

            await _surveyTemplateRepository.Delete(entityId);
        }
    }
}