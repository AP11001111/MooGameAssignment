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

        //public static void DisplayGuessCheckResult(string result, IConsoleIOService ui)
        //{
        //    ui.PutString(result + "\n");
        //}

        //public static void GetGuessFromUser(IConsoleIOService ui)
        //{
        //    string? guess = ui.GetString();

        //    while (
        //        (string.IsNullOrWhiteSpace(guess))
        //        || !(guess.Length is 4)
        //        || !(int.TryParse(guess, out _)))
        //    {
        //        ui.PutString("Invalid input. Guess must 4 digit long.");
        //        ui.PutString("Try again:\n");

        //        guess = ui.GetString();
        //    }

        //    mooGame.Guess = guess!;
        //}
    }
}
