namespace Colegio.WebApp.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetById(string Id, string jwtToken);
        Task<IEnumerable<T>> GetAll(string jwtToken);
        Task<ServiceReturn> Add(T entity, string jwtToken);
        Task<ServiceReturn> Update(T entity, string jwtToken);
        Task<ServiceReturn> Remove(T entity, string jwtToken);
    }
}
