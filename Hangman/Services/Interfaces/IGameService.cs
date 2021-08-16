using System.Threading;
using System.Threading.Tasks;

namespace Hangman.Services
{
    public interface IGameService
    {
        public Task RunAsync(CancellationToken cancellationToken);
    }
}
