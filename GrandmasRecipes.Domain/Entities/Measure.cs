using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Одиниця виміру для інгредієнтів. Наприклад: кг, л, штук.
    /// </summary>
    public class Measure
    {
        [Key]
        public int Id { get; set; }

        /// <summary>Назва одиниці виміру.</summary>
        public string Name { get; set; } = null!;
    }
}