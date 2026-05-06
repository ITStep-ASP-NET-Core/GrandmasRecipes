
namespace GrandmasRecipes.Domain.Entities
{
    public class User : Account
    {
		public string? ImageUrl { get; set; }
		public int Likes { get; set; } = 0;
		public int Published { get; set; } = 0;
		public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

		public ICollection<Recipe> Recipes { get; set; } = [];

		public ICollection<Recipe> Liked { get; set; } = [];

		public ICollection<Review> Reviews { get; set; } = [];
    }
}