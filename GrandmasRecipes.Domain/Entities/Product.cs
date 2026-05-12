using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Продукт що використовується в інгредієнтах рецептів.
    /// </summary>
    public class Product
    {
        [Key]
        public int Id { get; set; }

        /// <summary>Назва продукту.</summary>
        public string Name { get; set; } = string.Empty;
    }
}