using GrandmasRecipes.Application.DTO.Cuisine;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Interfaces;

namespace GrandmasRecipes.Application.Implementations
{
    public class CuisineService : IService<CuisineDto>
    {
        private readonly IUnitOfWork _uow;

        public CuisineService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ICollection<CuisineDto>> GetAllAsync()
        {
            var items = await _uow.Cuisines.GetAllAsync();
            return items.Select(c => new CuisineDto { Id = c.Id, Name = c.Name }).ToList();
        }

        public async Task<CuisineDto?> GetAsync(int id)
        {
            var c = await _uow.Cuisines.GetByIdAsync(id);
            if (c == null) return null;
            return new CuisineDto { Id = c.Id, Name = c.Name };
        }

        public async Task AddAsync(CuisineDto dto)
        {
            var entity = new Cuisine { Name = dto.Name };
            await _uow.Cuisines.AddAsync(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task EditAsync(CuisineDto dto)
        {
            var entity = await _uow.Cuisines.GetByIdAsync(dto.Id);
            if (entity == null) return;
            entity.Name = dto.Name;
            _uow.Cuisines.Update(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(CuisineDto dto)
        {
            var entity = await _uow.Cuisines.GetByIdAsync(dto.Id);
            if (entity == null) return;
            _uow.Cuisines.Delete(entity);
            await _uow.SaveChangesAsync();
        }
    }
}