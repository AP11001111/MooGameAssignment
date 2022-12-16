using MooGameAssignment.Model;

namespace MooGameAssignment.ServiceLayer
{
    public interface IResultService
    {
        Task<List<PlayerData>> GetSortedPlayerDataAsync();
        Task<List<PlayerData>> ReadPlayerDataAsync();
        Task WritePlayerDataAsync(string text, bool append = true);
    }
}