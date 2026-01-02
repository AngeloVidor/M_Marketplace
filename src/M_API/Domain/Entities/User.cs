using Domain.ValueObjects;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; }
        public string Username { get; private set; }
        public Email Email { get; private set; }
        public PasswordHash Password { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; }

        public User(string username, Email email, PasswordHash password)
        {
            Id = Guid.NewGuid();

            Username = username ?? throw new DomainException("Invalid username.");
            Email = email ?? throw new DomainException("Invalid email.");
            Password = password ?? throw new DomainException("Invalid password.");

            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void ChangeEmail(Email newEmail)
        {
            Email = newEmail ?? throw new DomainException("Invalid email.");
        }

        public void ChangePassword(string newPassword)
        {
            Password = PasswordHash.Create(newPassword);
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }
}
