namespace MooGameAssignment.ServiceLayer
{
    public interface IConsoleIOService
    {
        void Clear();
        string? GetString();
        void PutString(string s);
    }
}