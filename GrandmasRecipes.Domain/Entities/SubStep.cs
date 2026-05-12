using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Підкрок приготування. Деталізує крок <see cref="Step"/>.
    /// </summary>
    public class SubStep
    {
        [Key]
        public int Id { get; set; }

        /// <summary>Порядковий номер підкроку.</summary>
        public int Number { get; set; }

        /// <summary>Текст підкроку.</summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>Ідентифікатор батьківського кроку.</summary>
        public int StepId { get; set; }
    }
}