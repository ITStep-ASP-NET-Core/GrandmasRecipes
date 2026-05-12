namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Адміністратор системи.
    /// Розширює <see cref="User"/> контактними даними.
    /// </summary>
    public class Admin : User
    {
        /// <summary>Адреса адміністратора.</summary>
        public string Adress { get; set; } = string.Empty;

        /// <summary>Номер телефону адміністратора.</summary>
        public string Phone { get; set; } = string.Empty;
    }
}