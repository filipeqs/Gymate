namespace Exercises.Infrastructure.Repositories.Cache
{
    public interface ICachedRepositoryDecorator<T> where T : class
    {
        Task<T?> GetCachedValueAsync(string key);
        Task UpdateCachedValueAsync(string key, T value);
        Task DeleteCachedValueAsync(string key);
    }
}
