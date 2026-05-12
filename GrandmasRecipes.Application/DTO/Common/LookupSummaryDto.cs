namespace GrandmasRecipes.Application.DTO.Common
{
    /// <summary>
    /// Скорочений DTO для довідникових сутностей.
    /// Використовується у вкладених об'єктах (наприклад, у RecipeDetailsDto).
    /// </summary>
    public class LookupSummaryDto
    {
        /// <summary>Ідентифікатор запису.</summary>
        public int Id { get; set; }

        /// <summary>Назва запису.</summary>
        public string Name { get; set; } = string.Empty;
    }
}