namespace GrandmasRecipes.Application.DTO.Recipe
{
    /// <summary>
    /// Коротка інформація про рецепт.
    /// Використовується у списках та пагінації.
    /// </summary>
    public class RecipePreviewDto
    {
        /// <summary>Ідентифікатор рецепту.</summary>
        public Guid Id { get; set; }

        /// <summary>Назва рецепту.</summary>
        /// <example>Борщ український</example>
        public string Title { get; set; } = string.Empty;

        /// <summary>Посилання на головну фотографію рецепту.</summary>
        public string? ImageUrl { get; set; }

        /// <summary>Кількість лайків.</summary>
        /// <example>142</example>
        public int Likes { get; set; }

        /// <summary>Чи лайкнув поточний користувач цей рецепт.</summary>
        /// <example>false</example>
        public bool IsLiked { get; set; }

        /// <summary>Нікнейм автора рецепту.</summary>
        /// <example>grandma_olga</example>
        public string? AuthorNickname { get; set; }
    }
}