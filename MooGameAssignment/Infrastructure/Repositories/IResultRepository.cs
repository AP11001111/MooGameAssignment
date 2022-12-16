namespace MooGameAssignment.Repositories
{
    public interface IResultRepository
    {
        Task<IEnumerable<string>> ReadAllAsListOfLineAsync();
        Task<string> ReadAllAsync();
        Task<string?> ReadAsync();
        Task WriteAsync(string text, bool isAppend);
    }
}