using MooGameAssignment.Model;

namespace MooGameAssignment.ApplicationLayer
{
    public interface IMooGame
    {
        string Goal { get; set; }
        bool IsTest { get; set; }
        string Guess { get; set; }
        PlayerData Player { get; set; }
        bool IsGameCompleted { get; set; }

        string CheckGuess();
        void InitializeGame();
    }
}