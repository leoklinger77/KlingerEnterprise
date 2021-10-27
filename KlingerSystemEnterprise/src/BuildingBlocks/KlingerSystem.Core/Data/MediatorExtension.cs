using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Core.Mediatr;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KlingerSystem.Core.Data
{
    public static class MediatorExtension
    {
        public static async Task SendEvent<T>(this IMediatrHandler mediatr, T context) where T : DbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notification != null && x.Entity.Notification.Any());

            var domainEvents = domainEntities.SelectMany(x => x.Entity.Notification).ToList();

            domainEntities.ToList().ForEach(x => x.Entity.DisposeEvent());

            var tasks = domainEvents.Select(async (domainEvents) =>
            {
                await mediatr.PublishEvent(domainEvents);
            });

            await Task.WhenAll(tasks);
        }
    }
}
