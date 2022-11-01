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
            HandleModified(eventData.Context);

            return result;
        }


        private static void HandleModified(DbContext context)
        {
            context.ChangeTracker.DetectChanges();

            foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified) {
                    entry.Property(e => e.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }
        }        
    }
}