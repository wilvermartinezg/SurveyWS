using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyWS.Domain.Entities.SurveyTemplate;
using SurveyWS.Domain.Exceptions;
using SurveyWS.Domain.Presentation;
using SurveyWS.Domain.Repository;
using SurveyWS.Infrastructure.EntityFramework;
using SurveyWS.Infrastructure.EntityFramework.Entities;
using SurveyWS.Infrastructure.Mappers;

namespace SurveyWS.Infrastructure.Repository
{
    public class SqlServerSurveyTemplateRepository : ISurveyTemplateRepository
    {
        private readonly SurveyTemplateMapper _mapper = new();
        private readonly ApplicationDbContext _applicationDbContext;

        public SqlServerSurveyTemplateRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<SurveyTemplate?> FindById(SurveyTemplateId id)
        {
            var entity = await _applicationDbContext
                .SurveyTemplateEfs
                .FirstOrDefaultAsync(entity => entity.Id == id.Value);

            return entity == null ? null : _mapper.FromEntityFramework(entity);
        }

        public async Task<List<SurveyTemplateSummary>> FindAllActive()
        {
            var result = await _applicationDbContext
                .SurveyTemplateEfs
                .Where(entity => entity.Active == true)
                .ToListAsync();

            return result
                .Select(_mapper.SummaryFromEntityFramework)
                .ToList();
        }

        public async Task Create(SurveyTemplate surveyTemplate)
        {
            var entity = new SurveyTemplateEf
            {
                Name = surveyTemplate.Name.Value,
                Description = surveyTemplate.Description.Value
            };

            _applicationDbContext.Add(entity);

            await _applicationDbContext.SaveChangesAsync();

            surveyTemplate.Id = SurveyTemplateId.ValueOf(entity.Id);
        }

        public async Task Update(SurveyTemplate surveyTemplate)
        {
            var id = surveyTemplate.Id;

            var entity = await _applicationDbContext
                .SurveyTemplateEfs
                .FirstOrDefaultAsync(entity => entity.Id == id.Value);

            if (entity == null)
            {
                throw new EntityNotFoundException($"Entidad SurveyTemplate {id} no existe");
            }

            entity.Name = surveyTemplate.Name.Value;
            entity.Description = surveyTemplate.Description.Value;
            entity.Active = surveyTemplate.Active;

            _applicationDbContext.SurveyTemplateEfs.Update(entity);

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Delete(SurveyTemplateId id)
        {
            var entity = await _applicationDbContext
                .SurveyTemplateEfs
                .FirstOrDefaultAsync(entity => entity.Id == id.Value);

            if (entity == null)
            {
                throw new EntityNotFoundException($"Entidad SurveyTemplate {id} no existe");
            }

            entity.Active = false;

            _applicationDbContext.SurveyTemplateEfs.Update(entity);

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}