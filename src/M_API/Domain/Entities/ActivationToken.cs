namespace Domain.Entities
{
    public class ActivationToken
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime ExpiresAt { get; private set; }

        protected ActivationToken() { }

        public ActivationToken(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Token = Guid.NewGuid().ToString("N");
            ExpiresAt = DateTime.UtcNow.AddHours(24);
        }

        public bool IsExpired() => DateTime.UtcNow > ExpiresAt;
    }
}
