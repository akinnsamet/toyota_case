using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Toyota.Shared.Entities.Common;

namespace Toyota.Data.Context.Toyota
{
    public interface IToyotaDbContext : IAsyncDisposable
    {
        public Task EnsureCreated();
        DbSet<T> Set<T>() where T : BaseContextEntity;
        Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolation = IsolationLevel.Unspecified, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
