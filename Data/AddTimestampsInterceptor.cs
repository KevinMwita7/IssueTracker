using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MessagePack.Resolvers;

namespace IssueTracker.Data
{
    public class AddTimestampsInterceptor : SaveChangesInterceptor {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            AddTimestamps(eventData.Context);

            return result;
        }


        private static void AddTimestamps(DbContext context)
        {
            context.ChangeTracker.DetectChanges();

            foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added) {
                    entry.Property(e => e.CreatedAt).CurrentValue = DateTime.UtcNow;
                    entry.Property(e => e.UpdatedAt).CurrentValue = DateTime.UtcNow;
                } else if (entry.State == EntityState.Modified) {
                    entry.Property(e => e.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }
        }        
    }
}