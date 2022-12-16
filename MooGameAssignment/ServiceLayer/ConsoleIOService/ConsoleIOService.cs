namespace MooGameAssignment.ServiceLayer
{
    public class ConsoleIOService : IConsoleIOService
    {
        public string? GetString() => Console.ReadLine();

        public void PutString(string s) => Console.WriteLine(s);

        public void Clear() => Console.Clear();
    }
}
