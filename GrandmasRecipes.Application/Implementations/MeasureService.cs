using GrandmasRecipes.Application.DTO.Measure;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Interfaces;

namespace GrandmasRecipes.Application.Implementations
{
    public class MeasureService : IService<MeasureDto>
    {
        private readonly IUnitOfWork _uow;

        public MeasureService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ICollection<MeasureDto>> GetAllAsync()
        {
            var items = await _uow.Measures.GetAllAsync();
            return items.Select(d => new MeasureDto { Id = d.Id, Name = d.Name }).ToList();
        }

        public async Task<MeasureDto?> GetAsync(int id)
        {
            var d = await _uow.Measures.GetByIdAsync(id);
            if (d == null) return null;
            return new MeasureDto { Id = d.Id, Name = d.Name };
        }

        public async Task AddAsync(MeasureDto dto)
        {
            var entity = new Measure { Name = dto.Name };
            await _uow.Measures.AddAsync(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task EditAsync(MeasureDto dto)
        {
            var entity = await _uow.Measures.GetByIdAsync(dto.Id);
            if (entity == null) return;
            entity.Name = dto.Name;
            _uow.Measures.Update(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(MeasureDto dto)
        {
            var entity = await _uow.Measures.GetByIdAsync(dto.Id);
            if (entity == null) return;
            _uow.Measures.Delete(entity);
            await _uow.SaveChangesAsync();
        }
    }
}