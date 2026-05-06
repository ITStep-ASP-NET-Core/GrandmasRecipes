using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
    public class Step
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
		public string? ImageUrl { get; set; }
		public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid RecipeId { get; set; }
		
        public ICollection<SubStep>? SubSteps { get; set; }

    }
}