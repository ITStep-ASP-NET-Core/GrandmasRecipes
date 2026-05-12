namespace GrandmasRecipes.Application.DTO.Step
{
    /// <summary>Крок приготування рецепту.</summary>
    public class StepDto
    {
        /// <summary>Порядковий номер кроку.</summary>
        /// <example>1</example>
        public int Number { get; set; }

        /// <summary>Заголовок кроку.</summary>
        /// <example>Підготовка овочів</example>
        public string? Title { get; set; }

        /// <summary>Детальний опис кроку.</summary>
        public string? Description { get; set; }

        /// <summary>Посилання на зображення кроку.</summary>
        public string? ImageUrl { get; set; }

        /// <summary>Підкроки для деталізації. Масив текстових описів.</summary>
        public string[]? SubSteps { get; set; }
    }
}