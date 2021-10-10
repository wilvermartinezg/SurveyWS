using System.Threading.Tasks;
using SurveyWS.Domain.Entities.SurveyTemplate;
using SurveyWS.Domain.Exceptions;
using SurveyWS.Domain.Repository;
using SurveyWS.Domain.Validators;

namespace SurveyWS.Application.Update
{
    public class SurveyTemplateUpdater
    {
        private readonly ISurveyTemplateRepository _surveyTemplateRepository;

        public SurveyTemplateUpdater(ISurveyTemplateRepository surveyTemplateRepository)
        {
            _surveyTemplateRepository = surveyTemplateRepository;
        }

        public async Task Update(SurveyTemplateRequest request)
        {
            // 1. Validamos el ID de la entidad a actualizar
            var id = SurveyTemplateId.ValueOf(request.Id);

            id.Validate();

            // 2. Buscamos la entidad a actualizar
            var entityToUpdate = await _surveyTemplateRepository.FindById(id)
                                 ?? throw new EntityNotFoundException($"Entidad {id} no existe");

            // 3. Actualizamos los campos
            entityToUpdate.Name = SurveyTemplateName.ValueOf(request.Name);
            entityToUpdate.Description = SurveyTemplateDescription.ValueOf(request.Description);

            // 4. Validamos los datos requeridos
            new SurveyTemplateValidator(entityToUpdate).Validate();

            // 5. Guardamos los cambios en la base de datos
            await _surveyTemplateRepository.Update(entityToUpdate);
        }
    }
}