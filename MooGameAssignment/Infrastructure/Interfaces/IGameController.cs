namespace MooGameAssignment.Infrastructure.Interfaces
{
    public interface IGameController
    {
        Task RunAsync();
        void Cleanup();
    }
}
