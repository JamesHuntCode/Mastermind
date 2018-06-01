using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    class Program
    {
        /// <summary>
        /// Code set by Player 1 for Player 2 to crack.
        /// </summary>
        private static int[] playerOnesCode = null;

        /// <summary>
        /// Guesses remaining for Player 2 to correctly crack Player One's code.
        /// </summary>
        private static int remainingGuesses = 10;

        static void Main(string[] args)
        {
            // Start game - Take players one's code.
            playerOnesCode = TakeCodeToCrack();
            Console.WriteLine("\nGreat! Your input has been logged.");

            // Begin taking guesses from player two.

        }

        /// <summary>
        /// Method to take the code to crack from Player 1.
        /// </summary>
        /// /// <param name="offset"></param>
        /// <returns></returns>
        static int[] TakeCodeToCrack(string offset = null)
        {
            // Get player 1's input (4 random non-negative integers smaller than 10).
            Console.WriteLine(offset + "Player 1 - input a sequence of 4 random positive integers smaller than 10, each seperated by a comma:\n");
            string sequenceToCrack = Console.ReadLine();

            // Ensure input string is in the correct format.
            int[] userInput = ParseIntArray(sequenceToCrack);
            
            while (userInput == null)
            {
                Console.WriteLine("\nOops! Please input 4 non-negative integers, each below 10 and seperated by a comma.");
                userInput = TakeCodeToCrack("\n\n");
            }

            return userInput;
        }

        /// <summary>
        /// Method to take a string input and parse it into an integer array.
        /// </summary>
        /// <param name="stringInput"></param>
        /// <returns></returns>
        static int[] ParseIntArray(string stringInput)
        {
            try
            {
                int[] parsedArray = new int[4];

                parsedArray[0] = Convert.ToInt32(stringInput.Split(',')[0]);
                parsedArray[1] = Convert.ToInt32(stringInput.Split(',')[1]);
                parsedArray[2] = Convert.ToInt32(stringInput.Split(',')[2]);
                parsedArray[3] = Convert.ToInt32(stringInput.Split(',')[3]);

                // Validate all element values are less than 10.
                for (int i = 0; i < parsedArray.Length; i++)
                {
                    if (parsedArray[i] >= 10)
                    {
                        return null;
                    }
                }

                // Return valid array.
                return parsedArray;
            }
            catch (Exception)
            {
                // Format of input was not accepted.
                return null;
            }
        }

        /// <summary>
        /// Method to take a guess input from Player 2 and check if correct.
        /// </summary>
        /// <param name="guess"></param>
        /// <returns></returns>
        static bool TakeGuess(int[] guess)
        {
            return false;
        }
    }
}
