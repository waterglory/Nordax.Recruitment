using System;
using System.ComponentModel.DataAnnotations;
using Nordax.Bank.Recruitment.Shared.Models;

namespace Nordax.Bank.Recruitment.DataAccess.Entities
{
    public class Subscription
    {
        public Subscription()
        {
        }

        public Subscription(string name, string email)
        {
            Name = name;
            Email = email;
            SignUpDate = DateTime.Now;
        }

        public Guid Id { get; set; }

        [Required] [MaxLength(200)] public string Name { get; set; }

        [Required] [MaxLength(200)] public string Email { get; set; }

        public DateTime SignUpDate { get; set; }

        public SubscriberModel ToDomainModel()
        {
            return new SubscriberModel()
            {
                Id = Id,
                Email = Email,
                Name = Name,
                SignUpDate = SignUpDate
            };
        }
    }
}