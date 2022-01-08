namespace Infra.Infra.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<T> GetOne(string Id);
        Task<IEnumerable<T>> GetAll();

    }
}
