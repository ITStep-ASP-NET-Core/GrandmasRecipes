using GrandmasRecipes.Application.Implementations;
using Xunit;

namespace GrandmasRecipes.Tests
{
    public class Argon2PasswordHasherTests
    {
        private readonly Argon2PasswordHasher _hasher = new();

        [Fact]
        public void HashPassword_ReturnsNonEmptyString()
        {
            var hash = _hasher.HashPassword("password123");
            Assert.False(string.IsNullOrEmpty(hash));
        }
        
        [Fact]
        public void HashPassword_SamePassword_ReturnsDifferentHashes()
        {
            var hash1 = _hasher.HashPassword("password123");
            var hash2 = _hasher.HashPassword("password123");
            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void VerifyPassword_CorrectPassword_ReturnsTrue()
        {
            var hash = _hasher.HashPassword("password123");
            Assert.True(_hasher.VerifyPassword("password123", hash));
        }

        [Fact]
        public void VerifyPassword_WrongPassword_ReturnsFalse()
        {
            var hash = _hasher.HashPassword("password123");
            Assert.False(_hasher.VerifyPassword("wrongpassword", hash));
        }

        [Fact]
        public void VerifyPassword_EmptyPassword_ReturnsFalse()
        {
            var hash = _hasher.HashPassword("password123");
            Assert.False(_hasher.VerifyPassword("completely_wrong", hash));
        }
    }
}
