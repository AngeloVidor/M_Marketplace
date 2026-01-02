using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M_API.Domain.Entities
{
    public class VendorProfile
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string StoreName { get; private set; }

        protected VendorProfile() { }

        public VendorProfile(Guid userId, string storeName)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            StoreName = storeName;
        }
    }
}