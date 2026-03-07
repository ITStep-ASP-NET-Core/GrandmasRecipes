using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.WebApi.Annotations
{
	public class AllowedExtensionsAttribute : ValidationAttribute
	{
		private readonly string[] _extensions;
		public AllowedExtensionsAttribute ( string[] extensions )
		{
			_extensions = extensions;
		}

		public override bool IsValid ( object? value )
		{
			if(value is IFormFile file)
			{
				Console.WriteLine("\n\n");
				Console.WriteLine(string.Join(", ", _extensions));
				Console.WriteLine(Path.GetExtension(file.FileName));
				Console.WriteLine("\n\n");
				if(_extensions.Contains(Path.GetExtension(file.FileName).Replace(".", string.Empty).ToLower()))
				{
					return true;
				}
			}
			return false;
		}
		public override string FormatErrorMessage ( string name )
		{
			return base.FormatErrorMessage(string.Join(", ", _extensions).ToString());
		}
	}
}
