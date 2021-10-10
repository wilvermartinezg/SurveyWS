using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyWS.Domain.Entities.SurveyTemplateDetail;

namespace SurveyWS.Domain.Repository
{
    public interface ISurveyTemplateDetailRepository
    {
        public Task<SurveyTemplateDetail?> FindById(SurveyTemplateDetailId id);
        public Task CreateAll(List<SurveyTemplateDetail> surveyTemplateDetails);
        public Task Update(SurveyTemplateDetail surveyTemplateDetail);
        public Task Delete(SurveyTemplateDetailId id);
    }
}