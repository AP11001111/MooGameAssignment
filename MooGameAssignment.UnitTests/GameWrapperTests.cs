using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MooGameAssignment.ApplicationLayer;
using MooGameAssignment.Infrastructure.Options;
using MooGameAssignment.Repositories;
using MooGameAssignment.ServiceLayer;
using System.Reflection;

namespace MooGameAssignment.UnitTests
{
    public class GameWrapperTests
    {
        [Theory]
        [InlineData('1', typeof(IMooGameController))]
        [InlineData('2', typeof(ICloneGameController))]
        [InlineData('3', null)]
        public void ShouldStartCorrectGame(char userChoice, Type result)
        {
            var gameWrapper = MockGameWrapper.GetGameWrapper();

            MethodInfo? methodInfo = typeof(GameWrapper).GetMethod("StartChosenGameAsync", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] args = { userChoice };
            methodInfo?.Invoke(gameWrapper, args);

            GameWrapper.CurrentGameType.Should().Be(result);
        }
    }
}