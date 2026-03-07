namespace GrandmasRecipes.WebApi.Common
{
	public static class ColorPicker
	{
		public static IEnumerable<string> Colors { get; set; }

		static ColorPicker()
		{
			Colors = new List<string>
			{
				"#6b6b6b",
				"#5d4279",
				"#385a36",
				"#3d5d72",
				"#851c1c",
				"#947b00",
			};

		}

		public static string? GetColor ( int colorNumber )
		{
			return Colors.ElementAtOrDefault(colorNumber);
		}

		public static int? GetColorNumber ( string color )
		{
			return Colors.ToList().IndexOf(color);
		}

		public static int GetRandomColorNumber ()
		{
			var random = new Random();
			return random.Next(0, Colors.Count());

		}
	}
}
