using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MessagePack.Resolvers;

namespace IssueTracker.Data.Interceptors
{
    public class OnAddOrUpdateInterceptor : SaveChangesInterceptor {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            await AddTimestamps(eventData.Context);

            return result;
        }


        private static Task AddTimestamps(DbContext context)
        {
            context.ChangeTracker.DetectChanges();

            foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
            {
                string s = entry.Properties.Aggregate(
                $"Inserting {entry.Metadata.DisplayName()} with ",
                (auditString, property) => auditString + $"{property.Metadata.Name}: '{property.CurrentValue}' ");

                Console.WriteLine(s);

                if (entry.State == EntityState.Added) {
                    entry.Property(e => e.CreatedAt).CurrentValue = DateTime.UtcNow;
                    entry.Property(e => e.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    ++entry.Property(e => e.Version).CurrentValue;
                } else if (entry.State == EntityState.Modified) {
                    entry.Property(e => e.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    ++entry.Property(e => e.Version).CurrentValue;
                }
            }

            return Task.CompletedTask;
        }        
    }
}