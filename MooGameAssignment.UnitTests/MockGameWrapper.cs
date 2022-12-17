using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MooGameAssignment.ApplicationLayer;
using MooGameAssignment.Infrastructure.Options;
using MooGameAssignment.Repositories;
using MooGameAssignment.ServiceLayer;

namespace MooGameAssignment.UnitTests
{
    public static class MockGameWrapper
    {
        public static IHost GetHost()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory))
                .AddYamlFile("conf/appsettings.yml", optional: false, reloadOnChange: true)
                .Build();

            var host = new HostBuilder()
                .ConfigureServices((_, services) =>
                    services.AddOptions()
                        .Configure<GameOptions>(typeof(IMooGameController).Name, config.GetRequiredSection("GameOptions:MooOptions"))
                        .Configure<GameOptions>(typeof(ICloneGameController).Name, config.GetRequiredSection("GameOptions:CloneOptions"))
                        .Configure<GameWrapperOptions>(config.GetRequiredSection("GameWrapperOptions"))
                        .AddTransient<IResultRepository, ResultRepository>()
                        .AddTransient<IResultService, ResultService>()
                        .AddSingleton<IConsoleIOService, ConsoleIOService>()
                        .AddTransient<IMooGame, ApplicationLayer.MooGame>()
                        .AddTransient<IMooGameController, MooGameController>()
                        .AddTransient<ICloneGame, CloneGame>()
                        .AddTransient<ICloneGameController, CloneGameController>()
                        .AddHostedService<GameWrapper>()
                )
                .Build();

            return host;
        }

        public static GameWrapper GetGameWrapper()
        {
            return new GameWrapper(GetHost());
        }
    }
}
