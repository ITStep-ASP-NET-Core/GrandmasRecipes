using System.ComponentModel;

namespace GrandmasRecipes.Domain.Enums
{
    /// <summary>
    /// Одиниці виміру для інгредієнтів рецептів.
    /// </summary>
    public enum MeasureEnum
    {
        /// <summary>Мілілітри.</summary>
        [Description("ml")]
        Milliliters,

        /// <summary>Грами.</summary>
        [Description("g")]
        Grams,

        /// <summary>Літри.</summary>
        [Description("l")]
        Liters,

        /// <summary>Кілограми.</summary>
        [Description("kg")]
        Kilograms,

        /// <summary>Столові ложки.</summary>
        [Description("spoons")]
        Spoons,

        /// <summary>Склянки.</summary>
        [Description("glasses")]
        Glasses,

        /// <summary>Штуки.</summary>
        [Description("pieces")]
        Pieces,
    }
}