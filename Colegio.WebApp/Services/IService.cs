namespace Colegio.WebApp.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetById(string Id, string jwtToken);
        Task<IEnumerable<T>> GetAll(string jwtToken);
        Task<bool> Add(T entity, string jwtToken);
        Task<bool> Update(T entity, string jwtToken);
        Task<bool> Remove(T entity, string jwtToken);
    }
}
