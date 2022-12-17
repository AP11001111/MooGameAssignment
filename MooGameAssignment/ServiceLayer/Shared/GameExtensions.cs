using MooGameAssignment.Model;

namespace MooGameAssignment.ServiceLayer.Shared
{
    public static class GameExtensions
    {
        public static void DisplayNewGame(IConsoleIOService ui, bool isTest, string? goal)
        {
            ui.PutString("New game:\n");

            if (isTest)
            {
                ui.PutString("For practice, number is: " + goal + "\n");
            }
        }

        public static async Task SaveAndDisplayCurrentResult(PlayerData player, IConsoleIOService ui, IResultService resultService)
        {
            ui.PutString("\nCorrect, it took " + player.TotalNumberOfGuesses + " guesses\n");
            await resultService.WritePlayerDataAsync($"{player.Name}#&#{player.TotalNumberOfGuesses}");
        }

        public static async Task DisplayRankingsAsync(IConsoleIOService ui, IResultService resultService)
        {
            var results = await resultService.GetSortedPlayerDataAsync();
            ui.PutString("Player   games average");
            foreach(PlayerData p in results)
            {
                ui.PutString(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NumberOfGames, p.GetAverageGuessesRequired()));
            }
            ui.PutString("");
        }

        public static bool CheckIfPlayerWillReplay(IConsoleIOService ui)
        {
            ui.PutString("Continue?");
            string? answer = ui.GetString();

            if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
            {
                return false;
            }

            return true;
        }

        public static string GetPlayerName(IConsoleIOService ui)
        {
            ui.PutString("Enter your user name:\n");
            string? name = ui.GetString();

            while (string.IsNullOrWhiteSpace(name))
            {
                ui.PutString("Invalid input. User name must be atleast 1 character long.");
                ui.PutString("Enter your user name:\n");

                name = ui.GetString();
            }

            return name;
        }

        public static int GetGameChoice(List<string> games)
        {
            int digit;

            Console.WriteLine("Select Game:");

            for (int i = 0; i < games.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {games[i]}");
            }

            Console.WriteLine($"{games.Count + 1}: Exit");

            var key = Console.ReadKey(true).KeyChar.ToString();

            while (!int.TryParse(key, out digit) || digit > (games.Count + 1))
            {
                key = Console.ReadKey().KeyChar.ToString();
            }

            Console.Clear();
            return digit;
        }
    }
}
