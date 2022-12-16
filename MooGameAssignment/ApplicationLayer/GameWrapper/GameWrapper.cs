using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MooGameAssignment.Infrastructure.Interfaces;

namespace MooGameAssignment.ApplicationLayer
{
    public class GameWrapper : BackgroundService
    {
        private readonly IHost host;
        private IGameController? currentGame;

        public static Type? CurrentGameType { get; set; }

        public GameWrapper(IHost host)
        {
            this.host = host;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await StartChosenGameAsync(GetGameChoice());

                if (currentGame is not null)
                {
                    await currentGame!.RunAsync();
                    currentGame.Cleanup();
                }             
            }
        }

        private char GetGameChoice()
        {
            Console.WriteLine("Select Game:");
            Console.WriteLine("1: MooGame");
            Console.WriteLine("2: CloneGame");
            Console.WriteLine("3: Exit");

            var c = Console.ReadKey(true).KeyChar;
            while (!(c.Equals('1') || c.Equals('2') || c.Equals('3')))
            {
                c = Console.ReadKey().KeyChar;
            }

            Console.Clear();
            return c;
        }

        private async Task StartChosenGameAsync(char userChoice)
        {
            switch (userChoice)
            {
                case '1':
                    CreateGame<IMooGameController>();
                    break;
                case '2':
                    CreateGame<ICloneGameController>();
                    break;
                case '3':
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
