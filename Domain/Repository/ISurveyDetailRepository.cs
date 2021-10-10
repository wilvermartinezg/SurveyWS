using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyWS.Domain.Entities.SurveyDetail;

namespace SurveyWS.Domain.Repository
{
    public interface ISurveyDetailRepository
    {
        public Task CreateAll(List<SurveyDetail> surveyDetails);
    }
}