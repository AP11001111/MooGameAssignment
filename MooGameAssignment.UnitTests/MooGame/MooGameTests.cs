using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MooGameAssignment.Infrastructure.Options;
using System.Reflection;
using Xunit;

namespace MooGameAssignment.UnitTests.MooGame
{
    public class MooGameTests
    {
        [Fact]
        public void ShouldInitializeGame()
        {
            var mooGame = GetMooGame();

            mooGame.InitializeGame();

            mooGame.Guess.Should().BeEmpty();
            mooGame.Goal.Should().HaveLength(4);
            mooGame.IsGameCompleted.Should().BeFalse();
            mooGame.Player.Should().NotBeNull();
        }

        [Fact]
        public void ShouldGenerateGoal()
        {
            var mooGame = GetMooGame();

            MethodInfo? methodInfo = typeof(ApplicationLayer.MooGame).GetMethod("GenerateGoal", BindingFlags.NonPublic | BindingFlags.Instance);
            methodInfo?.Invoke(mooGame, null);

            mooGame.Goal.Should().HaveLength(4);
        }

        [Theory]
        [InlineData("1234", "5678", ",")]
        [InlineData("1234", "1234", "BBBB,")]
        [InlineData("1234", "1256", "BB,")]
        [InlineData("1234", "5612", ",CC")]
        [InlineData("1234", "1562", "B,C")]
        public void ShouldCorrctlyCheckGuess(string guess, string goal, string result)
        {
            var mooGame = GetMooGame();
            mooGame.Guess = guess;
            mooGame.Goal = goal;

            var checkResult = mooGame.CheckGuess();

            checkResult.Should().Be(result);
        }

        private ApplicationLayer.MooGame GetMooGame()
        {
            var host = MockGameWrapper.GetHost();

            return new ApplicationLayer.MooGame(host.Services.GetRequiredService<IOptionsMonitor<GameOptions>>());
        }
    }
}
