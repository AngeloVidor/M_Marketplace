using Domain.ValueObjects;
using M_API.Domain.ValueObjects;

namespace Domain.Entities
{
    public class VendorProfile
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }

        public FullName OwnerName { get; private set; }
        public CompanyName CompanyName { get; private set; }
        public Cnpj Cnpj { get; private set; }
        public Address Address { get; private set; }
        public Phone Phone { get; private set; }

        public string? StripeAccountId { get; private set; }
        public VendorStripeStatus StripeStatus { get; private set; }

        protected VendorProfile() { }

        public VendorProfile(Guid userId, FullName ownerName, CompanyName companyName, Cnpj cnpj, Address address, Phone phone)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            OwnerName = ownerName;
            CompanyName = companyName;
            Cnpj = cnpj;
            Address = address;
            Phone = phone;
        }

        public void UpdateProfile(FullName ownerName, CompanyName companyName, Cnpj cnpj, Address address, Phone phone)
        {
            OwnerName = ownerName;
            CompanyName = companyName;
            Cnpj = cnpj;
            Address = address;
            Phone = phone;
        }

        public void AttachStripeAccount(string stripeAccountId)
        {
            StripeAccountId = stripeAccountId;
            StripeStatus = VendorStripeStatus.Pending;
        }

        public void ActivateStripe()
        {
            StripeStatus = VendorStripeStatus.Active;
        }
    }
}
