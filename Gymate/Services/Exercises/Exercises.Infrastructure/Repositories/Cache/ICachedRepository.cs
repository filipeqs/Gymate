namespace Exercises.Infrastructure.Repositories.Cache
{
    public interface ICachedRepository<T> where T : class
    {
        Task<T?> GetCachedValueAsync(string key);
        Task UpdateCachedValueAsync(string key, T value);
        Task DeleteCachedValueAsync(string key);
    }
}
