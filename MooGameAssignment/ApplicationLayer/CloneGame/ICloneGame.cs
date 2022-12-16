using MooGameAssignment.Model;

namespace MooGameAssignment.ApplicationLayer
{
    public interface ICloneGame
    {
        string Goal { get; set; }
        string Guess { get; set; }
        bool IsGameCompleted { get; set; }
        PlayerData Player { get; set; }
        bool IsTest { get; set; }

        string CheckGuess();
        void InitializeGame();
    }
}