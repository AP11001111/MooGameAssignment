using Microsoft.Extensions.Options;
using MooGameAssignment.ApplicationLayer;
using MooGameAssignment.Infrastructure.Options;
using MooGameAssignment.ServiceLayer;

namespace MooGameAssignment.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private StreamReader? reader;
        private StreamWriter? writer;
        private readonly GameOptions gameOptions;

        public ResultRepository(IOptionsMonitor<GameOptions> gameOptions)
        {
            this.gameOptions = gameOptions.Get(GameWrapper.CurrentGameType!.Name);
        }

        public async Task WriteAsync(string text, bool isAppend)
        {
            using (writer = new StreamWriter(gameOptions.ResultFilePath!, isAppend))
            {
                await writer.WriteLineAsync(text);
            }
        }

        public async Task<string?> ReadAsync()
        {
            using (reader = new StreamReader(gameOptions.ResultFilePath!))
            {
                return await reader.ReadLineAsync();
            }
        }

        public async Task<string> ReadAllAsync()
        {
            using (reader = new StreamReader(gameOptions.ResultFilePath!))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public async Task<IEnumerable<string>> ReadAllAsListOfLineAsync()
        {
            List<string> result = new();
            string? line = string.Empty;

            using (reader = new StreamReader(gameOptions.ResultFilePath!))
            {
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }
    }
}
