using System;

namespace GuessTheNumber
{
        private static void RunGame()
        {
            int randomNumber = GenerateRandomNumber(1, 100);
            int numberOfGuesses = 0;
            bool isGuessCorrect = false;

            Console.WriteLine("Guess a number between 1 and 100:");

            while (!isGuessCorrect)
            {
                try
                {
                    int userGuess = ReadValidGuess();
                    numberOfGuesses++;

                    isGuessCorrect = CheckGuess(userGuess, randomNumber, numberOfGuesses);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static int GenerateRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max + 1);
        }

        private static int ReadValidGuess()
        {
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int guess))
            {
                throw new ArgumentException("Invalid input. Please enter a number.");
            }

            if (guess < 1 || guess > 100)
            {
                throw new ArgumentException("Number must be between 1 and 100.");
            }

            return guess;
        }

        private static bool CheckGuess(int guess, int randomNumber, int guessCount)
        {
            if (guess < randomNumber)
            {
                Console.WriteLine("Too low. Guess again:");
                return false;
            }

            if (guess > randomNumber)
            {
                Console.WriteLine("Too high. Guess again:");
                return false;
            }

            Console.WriteLine($"You guessed it in {guessCount} guesses!");
            return true;
        }
}
