using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.Entities;
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
        private readonly ApplicationDbContext _applicationDbContext;

        public SubscriptionRepository(IDbContextFactory dbContextFactory)
        {
            _applicationDbContext = dbContextFactory.Create();
        }

        public async Task<Guid> RegisterSubscriptionAsync(string name, string email)
        {
            if (await _applicationDbContext.Subscriptions.AnyAsync(s => s.Email == email))
                throw new EmailAlreadyRegisteredException();

            var newSubscription = await _applicationDbContext.Subscriptions.AddAsync(new Subscription(name, email));
            await _applicationDbContext.SaveChangesAsync();

            return newSubscription.Entity.Id;
        }

        public async Task<SubscriberModel> GetSubscription(Guid subscriberId)
        {
            var subscriber = await _applicationDbContext.Subscriptions.FirstOrDefaultAsync(s => s.Id == subscriberId);
            if (subscriber == null) throw new UserNotFoundException();
            return subscriber.ToDomainModel();
        }

        public async Task DeleteSubscription(Guid subscriberId)
        {
            var subscriber = await _applicationDbContext.Subscriptions.FirstOrDefaultAsync(s => s.Id == subscriberId);
            if (subscriber == null) throw new UserNotFoundException();
            _applicationDbContext.Subscriptions.Remove(subscriber);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}