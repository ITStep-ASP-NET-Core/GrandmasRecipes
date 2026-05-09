
namespace GrandmasRecipes.Infrastructure.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<ICollection<T>> GetAllAsync ( );
		Task<T?> GetByIdAsync ( int id );

		Task AddAsync ( T obj );
		void Update ( T obj );
		void Delete ( T obj );
	}
}