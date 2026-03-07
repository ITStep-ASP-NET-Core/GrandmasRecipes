using System.ComponentModel;

namespace GrandmasRecipes.Domain.Enums
{
	public enum Measure
	{
		None,

		[Description("ml")]
		Milliliters,

		[Description("g")]
		Grams,

		[Description("l")]
		Liters,

		[Description("kg")]
		Kilograms,

		[Description("spoons")]
		Spoons,

		[Description("glasses")]
		Glasses,

		[Description("pieces")]
		Pieces,
	}
}
