namespace Contracts;

public interface IGenericService<T, U>
{
    public Task<ICollection<T>> GetListAsync();
    public Task<T> GetElementAsync(U id);
    public Task<T> AddElementAsync(T subforum);
    public Task DeleteElementAsync(string id);
    public Task UpdateElementAsync(T subforum);
}