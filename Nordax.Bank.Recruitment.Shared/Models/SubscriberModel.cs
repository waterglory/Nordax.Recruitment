using System;

namespace Nordax.Bank.Recruitment.Shared.Models
{
    public class SubscriberModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime SignUpDate { get; set; }
    }
}