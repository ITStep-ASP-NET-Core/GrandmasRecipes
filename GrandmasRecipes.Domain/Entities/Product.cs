using GrandmasRecipes.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public Measure Measure { get; set; }

	}
}
