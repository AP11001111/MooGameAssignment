namespace MooGameAssignment.Model
{
    public class PlayerData
    {
        public string Name { get; set; }
        public int NumberOfGames { get; private set; }
        public int TotalNumberOfGuesses { get; private set; }


        public PlayerData(string name, int guesses)
        {
            this.Name = name;
            NumberOfGames = 1;
            TotalNumberOfGuesses = guesses;
        }

        public void ResetGuessesRequired() => TotalNumberOfGuesses = 0;

        public void IncreaseGuessesRequired() => TotalNumberOfGuesses++;

        public void AddGuessesFromNewGame(int guesses)
        {
            TotalNumberOfGuesses += guesses;
            NumberOfGames++;
        }

        public double GetAverageGuessesRequired()
        {
            return (double)TotalNumberOfGuesses / NumberOfGames;
        }

        public override bool Equals(Object? p)
        {
            return Name.Equals(((PlayerData)p!)!.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
