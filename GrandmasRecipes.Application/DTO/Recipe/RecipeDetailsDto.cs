using GrandmasRecipes.Application.DTO.Account;
using GrandmasRecipes.Application.DTO.Category;
using GrandmasRecipes.Application.DTO.Cuisine;
using GrandmasRecipes.Application.DTO.Difficulty;
using GrandmasRecipes.Application.DTO.Ingredient;
using GrandmasRecipes.Application.DTO.Step;

namespace GrandmasRecipes.Application.DTO.Recipe
{
    /// <summary>
    /// Детальна інформація про рецепт.
    /// Використовується для відображення повної сторінки рецепту.
    /// </summary>
    public class RecipeDetailsDto
    {
        /// <summary>Ідентифікатор рецепту.</summary>
        public Guid Id { get; set; }

        /// <summary>Назва рецепту.</summary>
        /// <example>Борщ український</example>
        public string Title { get; set; } = string.Empty;

        /// <summary>Опис рецепту.</summary>
        public string? Description { get; set; }

        /// <summary>Масив посилань на фотографії рецепту.</summary>
        public string[]? ImageUrls { get; set; }

        /// <summary>Кількість калорій на порцію.</summary>
        /// <example>350</example>
        public int Calories { get; set; }

        /// <summary>Кількість лайків.</summary>
        /// <example>142</example>
        public int Likes { get; set; }

        /// <summary>Чи лайкнув поточний користувач цей рецепт.</summary>
        /// <example>false</example>
        public bool IsLiked { get; set; }

        /// <summary>Автор рецепту.</summary>
        public AccountSummaryDto? Author { get; set; }

        /// <summary>Складність приготування.</summary>
        public DifficultySummaryDto? Difficulty { get; set; }

        /// <summary>Кухня до якої належить рецепт.</summary>
        public CuisineSummaryDto? Cuisine { get; set; }

        /// <summary>Категорії рецепту.</summary>
        public ICollection<CategorySummaryDto>? Categories { get; set; }

        /// <summary>Список інгредієнтів.</summary>
        public ICollection<IngredientDto>? Ingredients { get; set; }

        /// <summary>Кроки приготування у порядку виконання.</summary>
        public ICollection<StepDto>? Steps { get; set; }
    }
}