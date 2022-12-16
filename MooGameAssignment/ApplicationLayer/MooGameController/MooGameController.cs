using MooGameAssignment.ServiceLayer;
using MooGameAssignment.ServiceLayer.Shared;

namespace MooGameAssignment.ApplicationLayer
{
    public class MooGameController : IMooGameController
    {
        private bool replayGame;
        private readonly IMooGame mooGame;
        private readonly IResultService resultService;
        private readonly IConsoleIOService ui;

        public MooGameController(IMooGame mooGame, IResultService resultService, IConsoleIOService ui)
        {
            replayGame = true;
            this.mooGame = mooGame;
            this.resultService = resultService;
            this.ui = ui;
        }

        public async Task RunAsync()
        {
            mooGame.Player.Name = GameExtensions.GetPlayerName(ui);

            while (replayGame)
            {
                mooGame.InitializeGame();
                GameExtensions.DisplayNewGame(ui, mooGame.IsTest, mooGame.Goal);

                PlayRound();

                while (!mooGame.IsGameCompleted)
                {
                    PlayRound();
                }

                await GameExtensions.SaveAndDisplayCurrentResult(mooGame.Player, ui,resultService);
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

        private void DisplayGuessCheckResult()
        {
            string result = mooGame.CheckGuess();
            ui.PutString(result + "\n");
        }

        private void GetGuessFromUser()
        {
            string? guess = ui.GetString();

            while (
                (string.IsNullOrWhiteSpace(guess))
                || !(guess.Length is 4)
                || !(int.TryParse(guess, out _)))
            {
                ui.PutString("Invalid input. Guess must 4 digit long.");
                ui.PutString("Try again:\n");

                guess = ui.GetString();
            }

            mooGame.Guess = guess!;
        }
    }
}
