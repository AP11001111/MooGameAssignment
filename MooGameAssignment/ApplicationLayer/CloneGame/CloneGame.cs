using Microsoft.Extensions.Options;
using MooGameAssignment.Infrastructure.Options;
using MooGameAssignment.Model;

namespace MooGameAssignment.ApplicationLayer
{
    public class CloneGame : ICloneGame
    {
        private readonly GameOptions options;
        private readonly Random randomGenerator;

        public PlayerData Player { get; set; }
        public string Goal { get; set; }
        public string Guess { get; set; }
        public bool IsTest { get; set; }
        public bool IsGameCompleted { get; set; }


        public CloneGame(IOptionsMonitor<GameOptions> options)
        {
            this.options = options.Get(typeof(ICloneGameController).Name);
            Player = new PlayerData(string.Empty, 0);
            Goal = string.Empty;
            Guess = string.Empty;
            IsTest = this.options.IsTest;
            IsGameCompleted = false;
            randomGenerator = new Random();
        }

        public void InitializeGame()
        {
            IsGameCompleted = false;
            Guess = string.Empty;
            Goal = string.Empty;
            Goal = randomGenerator.Next(options.RandomGeneratorMaxValue).ToString();
            Player.ResetGuessesRequired();
        }

        public string CheckGuess()
        {
            Player.IncreaseGuessesRequired();
            if (Guess.Equals(Goal))
            {
                IsGameCompleted = true;
                return "Win";
            }
            return "Incorrect";
        }
    }
}
