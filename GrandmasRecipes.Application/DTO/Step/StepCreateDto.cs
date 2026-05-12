namespace GrandmasRecipes.Application.DTO.Step
{
    /// <summary>Дані для створення кроку при додаванні або редагуванні рецепту.</summary>
    public class StepCreateDto
    {
        /// <summary>Порядковий номер кроку.</summary>
        /// <example>1</example>
        public int Number { get; set; }

        /// <summary>Заголовок кроку.</summary>
        public string? Title { get; set; }

        /// <summary>Детальний опис кроку.</summary>
        public string? Description { get; set; }

        /// <summary>Посилання на зображення кроку.</summary>
        public string? ImageUrl { get; set; }

        /// <summary>Підкроки. Масив текстових описів.</summary>
        public string[]? SubSteps { get; set; }
    }
}