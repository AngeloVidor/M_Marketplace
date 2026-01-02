using Domain.ValueObjects;

namespace Domain.Entities
{
    public class CustomerProfile
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }

        public FullName FullName { get; private set; }
        public Address Address { get; private set; }
        public Phone Phone { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        protected CustomerProfile() { }

        public CustomerProfile(Guid userId, FullName fullName, Address address, Phone phone)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            FullName = fullName;
            Address = address;
            Phone = phone;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateProfile(FullName fullName, Address address, Phone phone)
        {
            FullName = fullName;
            Address = address;
            Phone = phone;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
