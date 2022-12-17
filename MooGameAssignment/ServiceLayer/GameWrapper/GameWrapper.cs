using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MooGameAssignment.ApplicationLayer;
using MooGameAssignment.Infrastructure.Interfaces;
using MooGameAssignment.Infrastructure.Options;
using MooGameAssignment.ServiceLayer.Shared;

namespace MooGameAssignment.ServiceLayer
{
    public class GameWrapper : BackgroundService
    {
        private readonly IHost host;
        private readonly GameWrapperOptions options;
        private IGameController? currentGame;

        public static Type? CurrentGameType { get; set; }

        public GameWrapper(IHost host)
        {
            this.host = host;
            options = host.Services.GetRequiredService<IOptions<GameWrapperOptions>>().Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await StartChosenGameAsync(GameExtensions.GetGameChoice(options.Games ?? new()));

                if (currentGame is not null)
                {
                    await currentGame!.RunAsync();
                    currentGame.Cleanup();
                }             
            }
        }

        private async Task StartChosenGameAsync(int userChoice)
        {
            switch (userChoice)
            {
                case 1:
                    CreateGame<IMooGameController>();
                    break;
                case 2:
                    CreateGame<ICloneGameController>();
                    break;
                case 3:
                    CurrentGameType = null;
                    await host.StopAsync();
                    break;
            }
        }

        private void CreateGame<T>()
            where T : IGameController
        {
            CurrentGameType = typeof(T);
            currentGame = host.Services.GetRequiredService<T>();
        }
    }
}
