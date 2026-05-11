using System.ComponentModel;

namespace GrandmasRecipes.Domain.Enums
{
	public enum DifficultyLevel
	{
		[Description("Easy")]
		Easy = 0,

		[Description("Normal")]
		Normal = 1,

		[Description("Difficult")]
		Difficult = 2,

		[Description("Gordon Ramsay")]
		GordonRamsay = 3,
	}
}
