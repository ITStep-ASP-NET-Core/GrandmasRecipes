using System.ComponentModel;

namespace GrandmasRecipes.Domain.Enums
{
    /// <summary>
    /// Рівень складності приготування рецепту.
    /// </summary>
    public enum DifficultyLevel
    {
        /// <summary>Простий — підходить для початківців.</summary>
        [Description("Easy")]
        Easy = 0,

        /// <summary>Середній — потребує базових навичок.</summary>
        [Description("Normal")]
        Normal = 1,

        /// <summary>Складний — потребує досвіду.</summary>
        [Description("Difficult")]
        Difficult = 2,

        /// <summary>Рівень Гордона Рамзі — тільки для професіоналів.</summary>
        [Description("Gordon Ramsay")]
        GordonRamsay = 3,
    }
}