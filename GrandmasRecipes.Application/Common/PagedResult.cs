namespace GrandmasRecipes.Application.Common
{
	public class PagedResult<T>
	{
		public ICollection<T> Items { get; set; } = [];
		public int Page { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public int PageCount => Items.Count;
		public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
	}
}
