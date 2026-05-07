using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
    public class Recipe
    {
		[Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
		public string[]? ImageUrls { get; set; }
        public int Calories { get; set; }
        public int Likes { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.Now;


		public Guid AuthorId { get; set; }
		[ForeignKey(nameof(AuthorId))]
		public Account? Author { get; set; }

		public int DificultyId { get; set; }
		[ForeignKey(nameof(DificultyId))]
		public Dificulty? Dificulty { get; set; }

		public int CuisineId { get; set; }
		[ForeignKey(nameof(CuisineId))]
		public Cuisine? Cuisine { get; set; }

        public ICollection<Category> Categories { get; set; } = [];
        public ICollection<Ingredient> Ingredients { get; set; } = [];
		public ICollection<Step> Steps { get; set; } = [];
		public ICollection<Review> Reviews { get; set; } = [];
		public ICollection<Like> Liked { get; set; } = [];
	}
}