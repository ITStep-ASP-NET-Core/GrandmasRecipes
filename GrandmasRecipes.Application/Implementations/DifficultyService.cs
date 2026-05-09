using GrandmasRecipes.Application.DTO.Difficulty;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Interfaces;

namespace GrandmasRecipes.Application.Implementations
{
    public class DifficultyService : IService<DifficultyDto>
    {
        private readonly IUnitOfWork _uow;

        public DifficultyService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ICollection<DifficultyDto>> GetAllAsync()
        {
            var items = await _uow.Difficulties.GetAllAsync();
            return items.Select(d => new DifficultyDto { Id = d.Id, Name = d.Name, ImageUrl = d.ImageUrl }).ToList();
        }

        public async Task<DifficultyDto?> GetAsync(int id)
        {
            var d = await _uow.Difficulties.GetByIdAsync(id);
            if (d == null) return null;
            return new DifficultyDto { Id = d.Id, Name = d.Name, ImageUrl = d.ImageUrl };
        }

        public async Task AddAsync(DifficultyDto dto)
        {
            var entity = new Difficulty { Name = dto.Name, ImageUrl = dto.ImageUrl };
            await _uow.Difficulties.AddAsync(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task EditAsync(DifficultyDto dto)
        {
            var entity = await _uow.Difficulties.GetByIdAsync(dto.Id);
            if (entity == null) return;
            entity.Name = dto.Name;
            entity.ImageUrl = dto.ImageUrl;
            _uow.Difficulties.Update(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(DifficultyDto dto)
        {
            var entity = await _uow.Difficulties.GetByIdAsync(dto.Id);
            if (entity == null) return;
            _uow.Difficulties.Delete(entity);
            await _uow.SaveChangesAsync();
        }
    }
}