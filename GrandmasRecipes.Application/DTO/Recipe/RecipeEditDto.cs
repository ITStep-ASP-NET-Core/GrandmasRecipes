using GrandmasRecipes.Application.DTO.Ingredient;
using GrandmasRecipes.Application.DTO.Step;

namespace GrandmasRecipes.Application.DTO.Recipe
{
    /// <summary>
    /// Дані для редагування рецепту.
    /// Всі поля окрім RecipeId опціональні — передавай тільки те що змінюєш.
    /// </summary>
    public class RecipeEditDto
    {
        /// <summary>Ідентифікатор рецепту для редагування.</summary>
        public Guid RecipeId { get; set; }

        /// <summary>Нова назва рецепту.</summary>
        public string? Title { get; set; }

        /// <summary>Новий опис рецепту.</summary>
        public string? Description { get; set; }

        /// <summary>Новий масив посилань на фотографії.</summary>
        public string[]? ImageUrls { get; set; }

        /// <summary>Нова кількість калорій.</summary>
        public int? Calories { get; set; }

        /// <summary>Новий ідентифікатор складності.</summary>
        public int? DifficultyId { get; set; }

        /// <summary>Новий ідентифікатор кухні.</summary>
        public int? CuisineId { get; set; }

        /// <summary>Нові ідентифікатори категорій.</summary>
        public IEnumerable<int>? CategoryIds { get; set; }

        /// <summary>Нові інгредієнти рецепту.</summary>
        public ICollection<IngredientCreateDto>? Ingredients { get; set; }

        /// <summary>Нові кроки приготування.</summary>
        public ICollection<StepCreateDto>? Steps { get; set; }
    }
}