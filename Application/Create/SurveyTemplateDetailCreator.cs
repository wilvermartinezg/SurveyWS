using System.Threading.Tasks;
using SurveyWS.Domain.Entities.SurveyTemplate;
using SurveyWS.Domain.Entities.SurveyTemplateDetail;
using SurveyWS.Domain.Repository;
using SurveyWS.Domain.Validators;

namespace SurveyWS.Application.Create
{
    public class SurveyTemplateDetailCreator
    {
        private readonly ISurveyTemplateDetailRepository _surveyTemplateDetailRepository;

        public SurveyTemplateDetailCreator(ISurveyTemplateDetailRepository surveyTemplateDetailRepository)
        {
            _surveyTemplateDetailRepository = surveyTemplateDetailRepository;
        }

        public async Task<long> Create(SurveyTemplateDetailRequest request)
        {
            var fieldName = SurveyTemplateDetailFieldName.ValueOf(request.FieldName);
            var fieldDescription = SurveyTemplateDetailFieldDescription.ValueOf(request.FieldDescription);
            var fieldType = SurveyTemplateDetailFieldType.ValueOf(request.FieldType);
            var isRequired = request.IsRequired;

            var entityDetail = new SurveyTemplateDetail
            {
                SurveyTemplateId = SurveyTemplateId.ValueOf(request.SurveyTemplateId),
                FieldName = fieldName,
                FieldDescription = fieldDescription,
                FieldType = fieldType,
                IsRequired = isRequired
            };

            new SurveyTemplateDetailValidator(entityDetail).Validate();

            await _surveyTemplateDetailRepository.Create(entityDetail);

            var generatedId = entityDetail.Id;
            generatedId.Validate();

            return generatedId.Value;
        }
    }
}