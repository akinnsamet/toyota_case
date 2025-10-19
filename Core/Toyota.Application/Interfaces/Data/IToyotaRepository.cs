using Toyota.Shared.Entities.Common;

namespace Toyota.Application.Interfaces.Data
{
    public interface IToyotaRepository<T> where T : BaseContextEntity, new()
    {
        IQueryable<T> GetList();
        IQueryable<T> GetListNoTracking();
        IQueryable<T> FromSqlRaw(string queryableText, params object[] values);
        Task<T> InsertT(T entity);
        Task Insert(T entity);
        Task Insert(List<T> entity);
        Task Update(T entity);
        Task Update(List<T> entity);
        Task Delete(T entity);
        Task Delete(List<T> entity);
        Task RemoveBulk(IQueryable<T> entity);
    }
}
