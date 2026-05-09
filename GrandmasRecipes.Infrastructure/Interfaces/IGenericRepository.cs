using System;
using System.Collections.Generic;
using System.Text;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    void UpdateAsync(T entity);
    void DeleteAsync(T entity);
    Task SaveAsync();
}
