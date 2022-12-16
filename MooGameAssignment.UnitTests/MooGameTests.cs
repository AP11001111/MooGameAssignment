using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MooGameAssignment.ApplicationLayer;
using MooGameAssignment.Infrastructure.Options;
using Moq;
using System.Reflection;

namespace MooGameAssignment.UnitTests
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

            MethodInfo? methodInfo = typeof(MooGame).GetMethod("GenerateGoal", BindingFlags.NonPublic | BindingFlags.Instance);
            methodInfo?.Invoke(mooGame, null);

            mooGame.Goal.Should().HaveLength(4);
        }

        private MooGame GetMooGame()
        {
            var host = MockGameWrapper.GetHost();

            return new MooGame(host.Services.GetRequiredService<IOptionsMonitor<GameOptions>>());
        }
    }
}
