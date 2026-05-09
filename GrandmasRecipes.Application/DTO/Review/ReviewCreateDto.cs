
namespace GrandmasRecipes.Application.DTO.Review
{
	public class ReviewCreateDto
	{
		public Guid AuthorId { get; set; }
		public string Comment { get; set; } = string.Empty;
	}
}
