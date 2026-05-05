namespace GrandmasRecipes.Application.DTO
{
    public class RecipeDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int LikesCount { get; set; }
    }
}