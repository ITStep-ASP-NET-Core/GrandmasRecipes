
namespace GrandmasRecipes.Application.Interfaces
{
	public interface IService<T>
	{
		Task AddAsync ( T obj );
		Task EditAsync ( T obj );
		Task DeleteAsync ( T obj );

		Task<T?> GetAsync ( int id );
		Task<ICollection<T>> GetAllAsync ( );
	}
}
