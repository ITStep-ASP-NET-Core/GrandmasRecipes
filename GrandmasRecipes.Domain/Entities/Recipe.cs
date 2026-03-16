using System;
using System.Collections.Generic;

namespace GrandmasRecipes.Domain.Entities
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public int LikesCount { get; set; } // Правка Сани [cite: 71]

        public Guid AuthorId { get; set; }
        public Account Author { get; set; } = null!;

        public ICollection<Step> Steps { get; set; } = new List<Step>();
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>(); // Это лечит CS1061 
    }
}