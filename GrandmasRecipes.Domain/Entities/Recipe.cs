using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Рецепт користувача.
    /// Містить інгредієнти, кроки приготування, автора, кухню та складність.
    /// </summary>
    public class Recipe
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>Назва рецепту.</summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>Опис рецепту.</summary>
        public string? Description { get; set; }

        /// <summary>Масив посилань на фотографії рецепту.</summary>
        public string[]? ImageUrls { get; set; }

        /// <summary>Кількість калорій на порцію.</summary>
        public int Calories { get; set; }

        /// <summary>Загальна кількість лайків.</summary>
        public int Likes { get; set; }

        /// <summary>Дата публікації рецепту.</summary>
        public DateTime PublishedDate { get; set; } = DateTime.Now;

        /// <summary>Ідентифікатор автора рецепту.</summary>
        public Guid AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Account? Author { get; set; }

        /// <summary>Ідентифікатор складності приготування.</summary>
        public int DifficultyId { get; set; }

        [ForeignKey(nameof(DifficultyId))]
        public Difficulty? Difficulty { get; set; }

        /// <summary>Ідентифікатор кухні.</summary>
        public int CuisineId { get; set; }

        [ForeignKey(nameof(CuisineId))]
        public Cuisine? Cuisine { get; set; }

        /// <summary>Категорії до яких належить рецепт.</summary>
        public ICollection<Category> Categories { get; set; } = [];

        /// <summary>Інгредієнти рецепту.</summary>
        public ICollection<Ingredient> Ingredients { get; set; } = [];

        /// <summary>Кроки приготування у порядку виконання.</summary>
        public ICollection<Step> Steps { get; set; } = [];

        /// <summary>Відгуки користувачів на рецепт.</summary>
        public ICollection<Review> Reviews { get; set; } = [];

        /// <summary>Лайки користувачів на рецепт.</summary>
        public ICollection<Like> Liked { get; set; } = [];
    }
}