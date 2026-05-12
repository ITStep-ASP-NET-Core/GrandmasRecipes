using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Refresh токен для оновлення JWT без повторного входу.
    /// </summary>
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        /// <summary>Значення токену.</summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>Дата закінчення дії токену.</summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>Чи відкликаний токен. Відкликані токени не приймаються.</summary>
        public bool IsRevoked { get; set; } = false;

        /// <summary>Ідентифікатор акаунту якому належить токен.</summary>
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }
    }
}