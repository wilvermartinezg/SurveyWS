using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyWS.Domain.Entities.SurveyTemplate;
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

        public async Task<List<SurveyTemplateDetail>> FindBySurveyTemplateId(SurveyTemplateId surveyTemplateId)
        {
            var dataList = await _applicationDbContext
                .SurveyTemplateDetailEfs
                .Where(entity => entity.SurveyTemplate != null && entity.SurveyTemplate.Id == surveyTemplateId.Value)
                .ToListAsync();

            return dataList
                .Select(_mapper.FromEntityFramework)
                .ToList();
        }

        public async Task<SurveyTemplateDetail?> FindById(SurveyTemplateDetailId id)
        {
            var entity = await _applicationDbContext
                .SurveyTemplateDetailEfs
                .FirstOrDefaultAsync(entity => entity.Id == id.Value);

            return entity == null ? null : _mapper.FromEntityFramework(entity);
        }

        public async Task Create(SurveyTemplateDetail surveyTemplateDetail)
        {
            var surveyTemplateEntity = await _applicationDbContext
                .SurveyTemplateEfs
                .FirstOrDefaultAsync(entity => entity.Id == surveyTemplateDetail.SurveyTemplateId.Value);

            var entity = new SurveyTemplateDetailEf
            {
                FieldName = surveyTemplateDetail.FieldName.Value,
                FieldDescription = surveyTemplateDetail.FieldDescription.Value,
                FieldType = surveyTemplateDetail.FieldType.Value,
                SurveyTemplate = surveyTemplateEntity
            };

            _applicationDbContext.Add(entity);

            await _applicationDbContext.SaveChangesAsync();

            surveyTemplateDetail.Id = SurveyTemplateDetailId.ValueOf(entity.Id);
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