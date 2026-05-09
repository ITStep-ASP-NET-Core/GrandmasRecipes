using GrandmasRecipes.Application.DTO.Category;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Interfaces;

namespace GrandmasRecipes.Application.Implementations
{
    public class CategoryService : IService<CategoryDto>
    {
        private readonly IUnitOfWork _uow;

        public CategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ICollection<CategoryDto>> GetAllAsync()
        {
            var items = await _uow.Categories.GetAllAsync();
            return items.Select(c => new CategoryDto { Id = c.Id, Name = c.Name }).ToList();
        }

        public async Task<CategoryDto?> GetAsync(int id)
        {
            var c = await _uow.Categories.GetByIdAsync(id);
            if (c == null) return null;
            return new CategoryDto { Id = c.Id, Name = c.Name };
        }

        public async Task AddAsync(CategoryDto dto)
        {
            var entity = new Category { Name = dto.Name };
            await _uow.Categories.AddAsync(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task EditAsync(CategoryDto dto)
        {
            var entity = await _uow.Categories.GetByIdAsync(dto.Id);
            if (entity == null) return;
            entity.Name = dto.Name;
            _uow.Categories.Update(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(CategoryDto dto)
        {
            var entity = await _uow.Categories.GetByIdAsync(dto.Id);
            if (entity == null) return;
            _uow.Categories.Delete(entity);
            await _uow.SaveChangesAsync();
        }
    }
}