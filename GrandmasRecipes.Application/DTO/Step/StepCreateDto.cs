
namespace GrandmasRecipes.Application.DTO.Step
{
	public class StepCreateDto
	{
		public int Number { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? ImageUrl { get; set; }
		public string[]? SubSteps { get; set; }
	}
}
