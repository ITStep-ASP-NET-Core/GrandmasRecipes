namespace GrandmasRecipes.Domain.Entities
{
	public class User : Account
	{
		public string UserName { get; set; } = null!;

		public bool IsBlocked { get; set; }

		public IEnumerable<Recipe> Recipes { get; set; } = [];

		public IEnumerable<Review> Reviews { get; set; } = [];
	}
}