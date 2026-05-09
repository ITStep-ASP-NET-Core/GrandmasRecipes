
namespace GrandmasRecipes.Application.DTO.Review
{
	public class ReviewDto
	{
		public int Id { get; set; }
		public string Comment { get; set; } = string.Empty;
		public Guid AuthorId { get; set; }
		public string AuthorNickname { get; set; } = string.Empty;
		public string? AuthorImageUrl { get; set; }
	}
}
