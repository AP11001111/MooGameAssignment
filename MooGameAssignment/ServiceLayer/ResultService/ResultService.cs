using MooGameAssignment.Model;
using MooGameAssignment.Repositories;

namespace MooGameAssignment.ServiceLayer
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository resultRepository;

        public ResultService(IResultRepository resultRepository)
        {
            this.resultRepository = resultRepository;
        }

        public async Task WritePlayerDataAsync(string text, bool append = true) => await resultRepository.WriteAsync(text, append);

        public async Task<List<PlayerData>> ReadPlayerDataAsync()
        {
            List<PlayerData> results = new();
            IEnumerable<string> lines = await resultRepository.ReadAllAsListOfLineAsync();

            foreach (var line in lines)
            {
                string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);

                string name = nameAndScore[0];
                int guesses = Convert.ToInt32(nameAndScore[1]);

                PlayerData playerData = new PlayerData(name, guesses);

                int index = results.IndexOf(playerData);

                if (index < 0)
                {
                    results.Add(playerData);
                }
                else
                {
                    results[index].AddGuessesFromNewGame(guesses);
                }
            }

            return results;
        }

        public async Task<List<PlayerData>> GetSortedPlayerDataAsync()
        {
            List<PlayerData> results = await ReadPlayerDataAsync();
            if (results.Any())
            {
                results.Sort((p1, p2) => p1.GetAverageGuessesRequired().CompareTo(p2.GetAverageGuessesRequired()));
            }
            return results;
        }
    }
}
