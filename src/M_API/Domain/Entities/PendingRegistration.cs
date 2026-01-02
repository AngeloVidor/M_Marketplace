using Domain.ValueObjects;
using M_API.Domain.ValueObjects;

namespace Domain.Entities
{
    public class PendingRegistration
    {
        public Guid Id { get; private set; }
        public Email Email { get; private set; }
        public string Username { get; private set; }
        public PasswordHash Password { get; private set; }
        public Role Role { get; private set; }
        public ActivationToken ActivationToken { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected PendingRegistration() { }

        public PendingRegistration(string username, Email email, PasswordHash password, Role role)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            Password = password;
            Role = role;
            CreatedAt = DateTime.UtcNow;
            ActivationToken = new ActivationToken(Id);
        }
    }
}
