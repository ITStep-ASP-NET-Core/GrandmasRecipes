namespace GrandmasRecipes.Domain.Entities
{
	public class Admin : User
	{
		public string Adress { get; set; } = null!;

		public string Phone { get; set; } = null!;

	}
}