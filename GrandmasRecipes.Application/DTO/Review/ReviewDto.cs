namespace GrandmasRecipes.Application.DTO.Review
{
    /// <summary>Відгук на рецепт.</summary>
    public class ReviewDto
    {
        /// <summary>Ідентифікатор відгуку.</summary>
        public int Id { get; set; }

        /// <summary>Текст відгуку.</summary>
        /// <example>Дуже смачний рецепт!</example>
        public string Comment { get; set; } = string.Empty;

        /// <summary>Ідентифікатор автора відгуку.</summary>
        public Guid AuthorId { get; set; }

        /// <summary>Нікнейм автора відгуку.</summary>
        /// <example>grandma_olga</example>
        public string AuthorNickname { get; set; } = string.Empty;

        /// <summary>Посилання на фото профілю автора.</summary>
        public string? AuthorImageUrl { get; set; }
    }
}