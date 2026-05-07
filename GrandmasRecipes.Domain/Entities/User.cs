
namespace GrandmasRecipes.Domain.Entities
{
    public class User : Account
    {
		public string? ImageUrl { get; set; }
		public int Likes { get; set; } = 0;
		public int Published { get; set; } = 0;
		public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}