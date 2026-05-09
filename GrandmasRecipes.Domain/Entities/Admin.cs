namespace GrandmasRecipes.Domain.Entities
{
	public class Admin : User
	{
		public string Adress { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
	}
}