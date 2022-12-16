using Microsoft.Extensions.Options;
using MooGameAssignment.Infrastructure.Options;
using MooGameAssignment.Model;

namespace MooGameAssignment.ApplicationLayer
{
    public class MooGame : IMooGame
    {
        public PlayerData Player { get; set; }
        public string Goal { get; set; }
        public string Guess { get; set; }
        public bool IsTest { get; set; }
        public bool IsGameCompleted { get; set; }

        private readonly Random randomGenerator;
        private readonly GameOptions options;
        private readonly string correctGuessResult;


        public MooGame(IOptionsMonitor<GameOptions> options)
        {
            this.options = options.Get(typeof(IMooGameController).Name);
            Player = new PlayerData(string.Empty, 0);
            Goal = string.Empty;
            Guess = string.Empty;
            IsTest = this.options.IsTest;
            correctGuessResult = this.options.CorrectGuessResult!;
            IsGameCompleted = false;
            randomGenerator = new Random();
        }

        public void InitializeGame()
        {
            IsGameCompleted = false;
            Guess = string.Empty;
            Goal = string.Empty;
            GenerateGoal();
            Player.ResetGuessesRequired();
        }

        private void GenerateGoal()
        {
            for (int i = 0; i < 4; i++)
            {
                string randomDigit = GetRandomNumberAsString();

                while (Goal.Contains(randomDigit))
                {
                    randomDigit = GetRandomNumberAsString();
                }
                Goal += randomDigit;
            }
        }

        public string CheckGuess()
        {
            int cows = 0;
            int bulls = 0;
            string result = string.Empty;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Goal[i] == Guess[j])
                    {
                        if (i == j)
                        {
                            bulls++;
                        }
                        else
                        {
                            cows++;
                        }
                    }
                }
            }
            result = "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);

            Player.IncreaseGuessesRequired();
            CheckGameCompletion(result);

            return result;
        }

        private void CheckGameCompletion(string result)
        {
            if (result.Equals(correctGuessResult))
            {
                IsGameCompleted = true;
            }
        }

        private string GetRandomNumberAsString() => randomGenerator.Next(options.RandomGeneratorMaxValue).ToString();
    }
}
