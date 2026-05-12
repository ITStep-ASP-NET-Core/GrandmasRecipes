using GrandmasRecipes.Application.DTO.Ingredient;
using GrandmasRecipes.Application.DTO.Step;

namespace GrandmasRecipes.Application.DTO.Recipe
{
    /// <summary>Дані для створення нового рецепту.</summary>
    public class RecipeCreateDto
    {
        /// <summary>Назва рецепту.</summary>
        /// <example>Борщ український</example>
        public string Title { get; set; } = string.Empty;

        /// <summary>Опис рецепту.</summary>
        public string? Description { get; set; }

        /// <summary>Масив посилань на фотографії.</summary>
        public string[]? ImageUrls { get; set; }

        /// <summary>Кількість калорій на порцію.</summary>
        /// <example>350</example>
        public int Calories { get; set; }

        /// <summary>Ідентифікатор автора рецепту.</summary>
        public Guid AuthorId { get; set; }

        /// <summary>Ідентифікатор складності.</summary>
        /// <example>2</example>
        public int DifficultyId { get; set; }

        /// <summary>Ідентифікатор кухні.</summary>
        /// <example>1</example>
        public int CuisineId { get; set; }

        /// <summary>Ідентифікатори категорій рецепту.</summary>
        public IEnumerable<int>? CategoryIds { get; set; }

        /// <summary>Інгредієнти рецепту.</summary>
        public ICollection<IngredientCreateDto>? Ingredients { get; set; }

        /// <summary>Кроки приготування.</summary>
        public ICollection<StepCreateDto>? Steps { get; set; }
    }
}