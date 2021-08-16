using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Hangman.Entities;

namespace Hangman.Services
{
    public class GameService : IGameService
    {
        private readonly IWordService _wordService;

        private Game _game;
        
        public GameService(IWordService wordService)
        {
            _wordService = wordService;
        }
        
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();
            
            while (!cancellationToken.IsCancellationRequested)
            {
                _game = new Game();
                
                var word = _wordService.GetWord();
                _game.SetWord(word);

                while (_game.GameState == GameState.Playing)
                {
                    var input = ' ';
                    while (!Regex.IsMatch(input.ToString(), "[a-z]") || _game.GuessedLetters.Contains(input))
                    {
                        Console.Clear();
                        Console.WriteLine($"Lives: {_game.Lives}");
                        Console.WriteLine($"Guessed: {string.Join(", ", _game.IncorrectGuessedLetters)}");
                        Console.WriteLine(_game.GetWordDisplay());
                        Console.Write("\nGuess a letter... ");
                        input = Console.ReadKey().KeyChar;
                    }
                    
                    _game.Guess(input);
                }

                Console.Clear();
                Console.WriteLine($"You {_game.GameState.ToString()}!");
                Console.WriteLine($"The word was {_game.Word}");

                Console.Write("\nPress any key to play again...");
                Console.ReadKey();
            }
        }
    }
}
