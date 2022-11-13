using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;
using Nordax.Bank.Recruitment.DataAccess.Entities.Subscription;
using Nordax.Bank.Recruitment.DataAccess.Exceptions;
using Nordax.Bank.Recruitment.DataAccess.Factories;
using Nordax.Bank.Recruitment.Shared.Models;

namespace Nordax.Bank.Recruitment.DataAccess.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<Guid> RegisterSubscriptionAsync(string firstName, string email);
        Task<SubscriberModel> GetSubscription(Guid subscriberId);
        Task DeleteSubscription(Guid subscriberId);
    }

    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly SubscriptionDbContext _subscriptionDbContext;

        public SubscriptionRepository(ISubscriptionDbContextFactory dbContextFactory)
        {
            _subscriptionDbContext = dbContextFactory.Create();
        }

        public async Task<Guid> RegisterSubscriptionAsync(string name, string email)
        {
            if (await _subscriptionDbContext.Subscriptions.AnyAsync(s => s.Email == email))
                throw new EmailAlreadyRegisteredException();

            var newSubscription = await _subscriptionDbContext.Subscriptions.AddAsync(new Subscription(name, email));
            await _subscriptionDbContext.SaveChangesAsync();

            return newSubscription.Entity.Id;
        }

        public async Task<SubscriberModel> GetSubscription(Guid subscriberId)
        {
            var subscriber = await _subscriptionDbContext.Subscriptions.FirstOrDefaultAsync(s => s.Id == subscriberId);
            if (subscriber == null) throw new UserNotFoundException();
            return subscriber.ToDomainModel();
        }

        public async Task DeleteSubscription(Guid subscriberId)
        {
            var subscriber = await _subscriptionDbContext.Subscriptions.FirstOrDefaultAsync(s => s.Id == subscriberId);
            if (subscriber == null) throw new UserNotFoundException();
            _subscriptionDbContext.Subscriptions.Remove(subscriber);
            await _subscriptionDbContext.SaveChangesAsync();
        }
    }
}