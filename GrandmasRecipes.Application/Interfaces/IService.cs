
namespace GrandmasRecipes.Application.Interfaces
{
	public interface IService<T> where T : class
    {
		Task AddAsync ( T obj );
		Task EditAsync ( T obj );
		Task DeleteAsync ( T obj );

		Task<T?> GetAsync ( int id );
		Task<ICollection<T>> GetAllAsync ( );
	}
}
