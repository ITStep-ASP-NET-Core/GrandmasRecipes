namespace GrandmasRecipes.Application.DTO.Common
{
    /// <summary>
    /// Базовий DTO для довідникових сутностей з повною інформацією.
    /// Використовується для категорій, кухонь, складностей, продуктів.
    /// </summary>
    public class LookupDto
    {
        /// <summary>Ідентифікатор запису.</summary>
        public int Id { get; set; }

        /// <summary>Назва запису.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Посилання на зображення.</summary>
        public string? ImageUrl { get; set; }
    }
}