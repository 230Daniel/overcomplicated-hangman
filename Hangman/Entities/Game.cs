using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman.Entities
{
    public class Game
    {
        public GameState GameState { get; private set; }
        public string Word { get; private set; }
        public int Lives { get; private set; }
        public List<char> GuessedLetters { get; }
        public IEnumerable<char> IncorrectGuessedLetters => GuessedLetters.Where(x => !Word.Contains(x));

        public Game()
        {
            GuessedLetters = new List<char>();
            Lives = 8;
        }
        
        public void SetWord(string word)
        {
            Word = word;
            GameState = GameState.Playing;
        }

        public string GetWordDisplay()
        {
            var displayChars = Word.Select(
                letter => GuessedLetters.Contains(letter) || letter == ' '
                    ? letter 
                    : '_')
                .ToList();
            return string.Join(" ", displayChars);
        }

        public void Guess(char letter)
        {
            if (GuessedLetters.Contains(letter))
                throw new InvalidOperationException("That letter has already been guessed.");

            GuessedLetters.Add(letter);
            
            if (Word.Contains(letter))
            {
                if (Word.All(x => GuessedLetters.Contains(x) || x == ' '))
                    GameState = GameState.Won;
            }
            else
            {
                Lives--;
                if (Lives == 0) GameState = GameState.Lost;
            }
        }
    }
    
    public enum GameState
    {
        None,
        Playing,
        Won,
        Lost
    }
}
