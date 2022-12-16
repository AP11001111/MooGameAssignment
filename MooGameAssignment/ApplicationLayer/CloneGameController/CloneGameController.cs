using MooGameAssignment.ServiceLayer;
using MooGameAssignment.ServiceLayer.Shared;

namespace MooGameAssignment.ApplicationLayer
{
    public class CloneGameController : ICloneGameController
    {
        private bool replayGame;
        private readonly ICloneGame cloneGame;
        private readonly IResultService resultService;
        private readonly IConsoleIOService ui;

        public CloneGameController(ICloneGame cloneGame, IResultService resultService, IConsoleIOService ui)
        {
            replayGame = true;
            this.cloneGame = cloneGame;
            this.resultService = resultService;
            this.ui = ui;
        }

        public async Task RunAsync()
        {
            cloneGame.Player.Name = GameExtensions.GetPlayerName(ui);

            while (replayGame)
            {
                cloneGame.InitializeGame();
                GameExtensions.DisplayNewGame(ui, cloneGame.IsTest, cloneGame.Goal);

                PlayRound();

                while (!cloneGame.IsGameCompleted)
                {
                    PlayRound();
                }

                await GameExtensions.SaveAndDisplayCurrentResult(cloneGame.Player, ui, resultService);
                await GameExtensions.DisplayRankingsAsync(ui, resultService);

                replayGame = GameExtensions.CheckIfPlayerWillReplay(ui);
            }
        }

        public void Cleanup() => ui.Clear();

        private void PlayRound()
        {
            GetGuessFromUser();

            DisplayGuessCheckResult();
        }

        private void GetGuessFromUser()
        {
            string? guess = ui.GetString();

            while (
                (string.IsNullOrWhiteSpace(guess))
                || !(int.TryParse(guess, out _)))
            {
                ui.PutString("Invalid input. Guess must be a number.");
                ui.PutString("Try again:\n");

                guess = ui.GetString();
            }

            cloneGame.Guess = guess!;
        }

        private void DisplayGuessCheckResult()
        {
            string result = cloneGame.CheckGuess();
            ui.PutString(result + "\n");
        }
    }
}
