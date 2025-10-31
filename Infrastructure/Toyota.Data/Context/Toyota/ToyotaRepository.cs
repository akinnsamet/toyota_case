using Microsoft.EntityFrameworkCore;
using Toyota.Application.Interfaces.Data;
using Toyota.Shared.Entities.Common;

namespace Toyota.Data.Context.Toyota
{
    public class ToyotaRepository<T>(IToyotaDbContext context) : IToyotaRepository<T> where T : BaseContextEntity, new()
    {
        protected readonly IToyotaDbContext ToyotaDbContext = context;
        protected DbSet<T> Table { get => ToyotaDbContext.Set<T>(); }
        public IQueryable<T> GetList() => Table.AsQueryable();
        public IQueryable<T> GetListNoTracking() => Table.AsNoTracking();
        public IQueryable<T> FromSqlRaw(string queryableText,params object[] values)
        {
            return Table.FromSqlRaw(queryableText, values);
        }

        public async Task<T> InsertT(T entity)
        {
            await Table.AddAsync(entity);
            await ToyotaDbContext.SaveChangesAsync();
            return entity;
        }
        public async Task Insert(T entity)
        {
            await Table.AddAsync(entity);
            await ToyotaDbContext.SaveChangesAsync();
        }
        public async Task Insert(List<T> entity)
        {
            await Table.AddRangeAsync(entity);
            await ToyotaDbContext.SaveChangesAsync();
        }
        public async Task Update(T entity)
        {
            Table.Update(entity);
            await ToyotaDbContext.SaveChangesAsync();
        }
        public async Task Update(List<T> entity)
        {
            Table.UpdateRange(entity);
            await ToyotaDbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            Table.Remove(entity);
            await ToyotaDbContext.SaveChangesAsync();
        }
        public async Task Delete(List<T> entity)
        {
            Table.RemoveRange(entity);
            await ToyotaDbContext.SaveChangesAsync();
        }
        public async Task RemoveBulk(IQueryable<T> entity)
        {
            await entity.ExecuteDeleteAsync();
            await ToyotaDbContext.SaveChangesAsync();
        }
    }
}
