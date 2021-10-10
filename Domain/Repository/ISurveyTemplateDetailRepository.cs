using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyWS.Domain.Entities.SurveyTemplateDetail;

namespace SurveyWS.Domain.Repository
{
    public interface ISurveyTemplateDetailRepository
    {
        public Task CreateAll(List<SurveyTemplateDetail> surveyTemplateDetails);
        
    }
}