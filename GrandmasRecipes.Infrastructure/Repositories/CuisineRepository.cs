using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Repositories
{
    public class CuisineRepository : ICuisineRepository
    {
        private readonly ApplicationContext _context;

        public CuisineRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Cuisine> GetAll()
            => _context.Cuisines.AsNoTracking();

        public async Task<Cuisine?> GetByIdAsync(int id)
            => await _context.Cuisines.FindAsync(id);

        public async Task AddAsync(Cuisine entity)
            => await _context.Cuisines.AddAsync(entity);

        public void Update(Cuisine entity)
            => _context.Cuisines.Update(entity);

        public void Delete(Cuisine entity)
            => _context.Cuisines.Remove(entity);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}