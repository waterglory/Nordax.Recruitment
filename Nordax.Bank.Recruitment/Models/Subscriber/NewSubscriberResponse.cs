using System;

namespace Nordax.Bank.Recruitment.Models.Subscriber
{
    public class NewSubscriberResponse
    {
        public NewSubscriberResponse(Guid subscriberId)
        {
            SubscriberId = subscriberId;
        }

        public Guid SubscriberId { get; set; }
    }
}