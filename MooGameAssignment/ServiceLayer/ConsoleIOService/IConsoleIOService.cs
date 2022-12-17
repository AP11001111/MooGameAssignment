namespace MooGameAssignment.ServiceLayer
{
    public interface IConsoleIOService
    {
        void Clear();
        string GetKeyPressed();
        string? GetString();
        void PutString(string s);
    }
}