using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}