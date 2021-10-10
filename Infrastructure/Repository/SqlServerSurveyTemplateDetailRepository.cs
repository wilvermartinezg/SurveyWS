using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyWS.Domain.Entities.SurveyTemplateDetail;
using SurveyWS.Domain.Exceptions;
using SurveyWS.Domain.Repository;
using SurveyWS.Infrastructure.EntityFramework;
using SurveyWS.Infrastructure.EntityFramework.Entities;
using SurveyWS.Infrastructure.Mappers;

namespace SurveyWS.Infrastructure.Repository
{
    public class SqlServerSurveyTemplateDetailRepository : ISurveyTemplateDetailRepository
    {
        private readonly SurveyTemplateDetailMapper _mapper = new();
        private readonly ApplicationDbContext _applicationDbContext;

        public SqlServerSurveyTemplateDetailRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<SurveyTemplateDetail?> FindById(SurveyTemplateDetailId id)
        {
            var entity = await _applicationDbContext
                .SurveyTemplateDetailEfs
                .FirstOrDefaultAsync(entity => entity.Id == id.Value);

            return entity == null ? null : _mapper.FromEntityFramework(entity);
        }

        public async Task CreateAll(List<SurveyTemplateDetail> surveyTemplateDetails)
        {
            SurveyTemplateEf? surveyTemplateEntity = null;

            foreach (var detail in surveyTemplateDetails)
            {
                surveyTemplateEntity ??= await _applicationDbContext
                    .SurveyTemplateEfs
                    .FirstOrDefaultAsync(entity => entity.Id == detail.SurveyTemplateId.Value);

                var entity = new SurveyTemplateDetailEf
                {
                    FieldName = detail.FieldName.Value,
                    FieldDescription = detail.FieldDescription.Value,
                    FieldType = detail.FieldType.Value,
                    SurveyTemplate = surveyTemplateEntity
                };

                _applicationDbContext.Add(entity);
            }

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Update(SurveyTemplateDetail surveyTemplateDetail)
        {
            var id = surveyTemplateDetail.Id;

            var entity = await _applicationDbContext
                .SurveyTemplateDetailEfs
                .FirstOrDefaultAsync(entity => entity.Id == id.Value);

            if (entity == null)
            {
                throw new EntityNotFoundException($"Entidad SurveyTemplateDetail {id} no existe");
            }

            entity.FieldName = surveyTemplateDetail.FieldName.Value;
            entity.FieldDescription = surveyTemplateDetail.FieldDescription.Value;
            entity.FieldType = surveyTemplateDetail.FieldType.Value;

            _applicationDbContext.SurveyTemplateDetailEfs.Update(entity);

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Delete(SurveyTemplateDetailId id)
        {
            var entity = await _applicationDbContext
                .SurveyTemplateDetailEfs
                .FirstOrDefaultAsync(entity => entity.Id == id.Value);

            if (entity == null)
            {
                throw new EntityNotFoundException($"Entidad SurveyTemplateDetail {id} no existe");
            }

            entity.Active = false;

            _applicationDbContext.SurveyTemplateDetailEfs.Update(entity);

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}