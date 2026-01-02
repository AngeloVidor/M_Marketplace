using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M_API.Domain.Entities
{
    public class CostumerProfile
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string FullName { get; private set; }

        protected CostumerProfile() { }

        public CostumerProfile(Guid userId, string fullName)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            FullName = fullName;
        }
    }
}
