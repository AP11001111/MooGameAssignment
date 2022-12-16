namespace MooGameAssignment.Infrastructure.Options
{
    public class GameOptions
    {
        public string? ResultFilePath { get; set; }
        public string? CorrectGuessResult { get; set; }
        public bool IsTest { get; set; }
        public int RandomGeneratorMaxValue { get; set; }
    }
}
