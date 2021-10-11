using System.Threading.Tasks;
using SurveyWS.Domain.Entities.SurveyTemplateDetail;
using SurveyWS.Domain.Exceptions;
using SurveyWS.Domain.Repository;
using SurveyWS.Domain.Validators;

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
            // 1. Validamos el ID de la entidad a actualizar
            var id = SurveyTemplateDetailId.ValueOf(request.Id);
            id.Validate();

            // 2. Buscamos la entidad a actualizar
            var entityToUpdate = await _surveyTemplateDetailRepository.FindById(id)
                                 ?? throw new EntityNotFoundException($"Entidad {id} no existe");

            // 3. Actualizamos los campos
            entityToUpdate.FieldName = SurveyTemplateDetailFieldName.ValueOf(request.FieldName);
            entityToUpdate.FieldDescription = SurveyTemplateDetailFieldDescription.ValueOf(request.FieldDescription);
            entityToUpdate.FieldType = SurveyTemplateDetailFieldType.ValueOf(request.FieldType);
            entityToUpdate.IsRequired = request.IsRequired;

            // 4. Validamos los datos requeridos
            new SurveyTemplateDetailValidator(entityToUpdate).Validate();

            // 5. Guardamos los cambios en la base de datos
            await _surveyTemplateDetailRepository.Update(entityToUpdate);
        }
    }
}