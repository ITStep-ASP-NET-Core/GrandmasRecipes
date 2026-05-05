using Microsoft.AspNetCore.Identity;

namespace GrandmasRecipes.Domain.Entities
{
    public class User : IdentityUser<Guid>     // ← ЗМІНИЛИ З Account НА ЦЕ
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public int TotalLikes { get; set; } = 0;

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}