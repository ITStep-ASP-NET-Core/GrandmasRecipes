using System;
using System.Collections.Generic;

namespace GrandmasRecipes.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Коллекция рецептов автора 
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}