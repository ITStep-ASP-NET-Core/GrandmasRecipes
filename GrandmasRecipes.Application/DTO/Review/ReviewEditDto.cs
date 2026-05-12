namespace GrandmasRecipes.Application.DTO.Review
{
    /// <summary>Дані для редагування відгуку.</summary>
    public class ReviewEditDto
    {
        /// <summary>Ідентифікатор відгуку для редагування.</summary>
        public int Id { get; set; }

        /// <summary>Новий текст відгуку.</summary>
        public string? Comment { get; set; }
    }
}