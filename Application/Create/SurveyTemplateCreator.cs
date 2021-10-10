using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyWS.Domain.Entities.SurveyTemplate;
using SurveyWS.Domain.Entities.SurveyTemplateDetail;
using SurveyWS.Domain.Repository;
using SurveyWS.Domain.Validators;

namespace SurveyWS.Application.Create
{
    public class SurveyTemplateCreator
    {
        private readonly ISurveyTemplateRepository _surveyTemplateRepository;
        private readonly ISurveyTemplateDetailRepository _surveyTemplateDetailRepository;

        public SurveyTemplateCreator(
            ISurveyTemplateRepository surveyTemplateRepository,
            ISurveyTemplateDetailRepository surveyTemplateDetailRepository
        )
        {
            _surveyTemplateRepository = surveyTemplateRepository;
            _surveyTemplateDetailRepository = surveyTemplateDetailRepository;
        }

        public async Task<long> Create(SurveyTemplateRequest request)
        {
            // 1. Crea la entidad Survey
            var name = SurveyTemplateName.ValueOf(request.Name);
            var description = SurveyTemplateDescription.ValueOf(request.Description);

            var surveyTemplate = new SurveyTemplate
            {
                Name = name,
                Description = description
            };

            // 2. Valida los campos requeridos
            new SurveyTemplateValidator(surveyTemplate).Validate();

            // 3. Persiste la entidad en la base de datos
            await _surveyTemplateRepository.Create(surveyTemplate);

            // 4. Crea los detalles
            var surveyTemplateDetails = new List<SurveyTemplateDetail>();

            foreach (var detail in request.Details)
            {
                var fieldName = SurveyTemplateDetailFieldName.ValueOf(detail.FieldName);
                var fieldDescription = SurveyTemplateDetailFieldDescription.ValueOf(detail.FieldDescription);
                var fieldType = SurveyTemplateDetailFieldType.ValueOf(detail.FieldType);
                var isRequired = detail.IsRequired;

                surveyTemplateDetails.Add(new SurveyTemplateDetail
                {
                    SurveyTemplateId = surveyTemplate.Id,
                    FieldName = fieldName,
                    FieldDescription = fieldDescription,
                    FieldType = fieldType,
                    IsRequired = isRequired
                });
            }

            await _surveyTemplateDetailRepository.CreateAll(surveyTemplateDetails);

            // 5. Valida que se haya generado un Id para la entidad 
            var generatedId = surveyTemplate.Id;
            generatedId.Validate();

            // 6. Retorna el Id generado
            return generatedId.Value;
        }
    }
}