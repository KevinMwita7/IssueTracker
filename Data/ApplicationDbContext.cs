using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Data.Interceptors;

namespace IssueTracker.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{

    private OnAddOrUpdateInterceptor _onAddOrUpdateInterceptor = new OnAddOrUpdateInterceptor();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .AddInterceptors(_onAddOrUpdateInterceptor);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        // builder.Entity<Project>().Property(e => e.CreatedAt).HasDefaultValueSql("now()");
        // builder.Entity<Project>().Property(e => e.UpdatedAt).HasDefaultValueSql("now()");
        // builder.Entity<Swimlane>().Property(e => e.CreatedAt).HasDefaultValueSql("now()");
        // builder.Entity<Swimlane>().Property(e => e.UpdatedAt).HasDefaultValueSql("now()");
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Swimlane>? Swimlane { get; set; }
}
