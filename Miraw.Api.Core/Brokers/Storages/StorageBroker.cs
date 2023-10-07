using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Miraw.Api.Core.Brokers.Storages;

//public partial class StorageBroker : EFxceptionsIdentityContext<User, Role, Guid>, IStorageBroker
public partial class StorageBroker : DbContext, IStorageBroker
{
    readonly IConfiguration configuration;

    public StorageBroker(IConfiguration configuration)
    {
        this.configuration = configuration;
        Database.Migrate();
    }

    async ValueTask<T> InsertAsync<T>(T @object)
    {
        Entry(@object).State = EntityState.Added;
        await SaveChangesAsync();

        return @object;
    }

    IQueryable<T> SelectAll<T>() where T : class => Set<T>();

    async ValueTask<T> SelectAsync<T>(params object[] @objectIds) where T : class =>
        await FindAsync<T>(objectIds);

    async ValueTask<T> UpdateAsync<T>(T @object)
    { 
        Entry(@object).State = EntityState.Modified;
        await SaveChangesAsync();

        return @object;
    }

    async ValueTask<T> DeleteAsync<T>(T @object)
    {
        Entry(@object).State = EntityState.Deleted;
        await SaveChangesAsync();

        return @object;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SetUserReferences(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;
        optionsBuilder.UseSqlServer(connectionString);
    }
}