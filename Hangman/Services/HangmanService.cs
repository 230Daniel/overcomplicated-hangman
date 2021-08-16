using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Hangman.Services
{
    public class HangmanService : IHostedService
    {
        private readonly IGameService _gameService;

        private Task _task;
        private CancellationTokenSource _cancellationTokenSource;
        
        public HangmanService(IGameService gameService)
        {
            _gameService = gameService;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _task = _gameService.RunAsync(_cancellationTokenSource.Token);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource.Cancel();
            return _task;
        }
    }
}
