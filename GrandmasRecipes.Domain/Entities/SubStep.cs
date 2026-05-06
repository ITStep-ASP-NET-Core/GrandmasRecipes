using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
    public class SubStep
    {
        [Key]
        public int Id { get; set; }

		public int Number { get; set; }
		public string Description { get; set; } = string.Empty;

		public int StepId { get; set; }
	}
}