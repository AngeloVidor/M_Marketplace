using Domain.Exceptions;
using BCrypt.Net;

namespace Domain.ValueObjects
{
    public sealed class PasswordHash
    {
        public string Value { get; }

        private PasswordHash() { }

        private PasswordHash(string hash)
        {
            Value = hash;
        }

        public static PasswordHash Create(string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                throw new DomainException("Invalid password.");

            return new PasswordHash(BCrypt.Net.BCrypt.HashPassword(plainPassword));
        }

        public bool Verify(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Value);
        }
    }
}
