using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
	public class Measure
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; } = null!;
	}
}
