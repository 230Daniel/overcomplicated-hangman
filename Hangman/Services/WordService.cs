using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Hangman.Services
{
    public class WordService : IWordService
    {
        private readonly IConfiguration _config;
        private readonly Random _random;

        public WordService(IConfiguration config, Random random)
        {
            _config = config;
            _random = random;
        }
        
        public string GetWord()
        {
            var words = _config
                .GetSection("Words")
                .Get<string[]>()
                .Select(x => x.ToLower())
                .ToArray();
            
            var index = _random.Next(0, words.Length);
            return words[index];
        }
    }
}
