using GrandmasRecipes.Application.DTO.Product;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Interfaces;

namespace GrandmasRecipes.Application.Implementations
{
    public class ProductService : IService<ProductDto>
    {
        private readonly IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ICollection<ProductDto>> GetAllAsync()
        {
            var items = await _uow.Products.GetAllAsync();
            return items.Select(p => new ProductDto { Id = p.Id, Name = p.Name }).ToList();
        }

        public async Task<ProductDto?> GetAsync(int id)
        {
            var p = await _uow.Products.GetByIdAsync(id);
            if (p == null) return null;
            return new ProductDto { Id = p.Id, Name = p.Name };
        }

        public async Task AddAsync(ProductDto dto)
        {
            var entity = new Product { Name = dto.Name };
            await _uow.Products.AddAsync(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task EditAsync(ProductDto dto)
        {
            var entity = await _uow.Products.GetByIdAsync(dto.Id);
            if (entity == null) return;
            entity.Name = dto.Name;
            _uow.Products.Update(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductDto dto)
        {
            var entity = await _uow.Products.GetByIdAsync(dto.Id);
            if (entity == null) return;
            _uow.Products.Delete(entity);
            await _uow.SaveChangesAsync();
        }
    }
}