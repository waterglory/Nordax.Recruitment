using System;
using Nordax.Bank.Recruitment.Shared.Models;

namespace Nordax.Bank.Recruitment.Models.Subscriber
{
    public class SubscriberResponse
    {
        public SubscriberResponse(SubscriberModel subscriberModel)
        {
            Name = subscriberModel.Name;
            UserId = subscriberModel.Id;
        }

        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}