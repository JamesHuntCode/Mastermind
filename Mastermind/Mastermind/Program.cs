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

        /// <summary>
        /// Boolean value to indicate if Player 2 has cracked the code.
        /// </summary>
        private static bool codeCracked = false;

        static void Main(string[] args)
        {
            // Take players one's code for player two to break.
            playerOnesCode = TakeCodeToCrack();
            Console.WriteLine("\nGreat! Your input has been logged.");

            // Begin taking guesses from player two.
            while (!codeCracked)
            {
                if (remainingGuesses == 0)
                {
                    string[] correctAnswer = { "BLACK", "BLACK", "BLACK", "BLACK" };
                    string[] userGuess = TakeGuess();

                    codeCracked = (userGuess == correctAnswer);

                    if (codeCracked)
                    {
                        // Player two has cracked the code.
                        break;
                    }
                    else
                    {
                        // Player two's guess was incorrect.
                        Console.WriteLine();
                        remainingGuesses--;
                    }
                }
                else
                {
                    // Player two has run out of lives.
                    PlayerOneWins();
                }
            }

            // Player two has cracked the code.
            PlayerTwoWins();
        }

        /// <summary>
        /// Method to take the code to crack from Player 1.
        /// </summary>
        /// <param name="offset"></param>
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
        /// <returns></returns>
        static string[] TakeGuess()
        {
            // Get guess from Player two.
            Console.WriteLine("\n\nPlayer 2 - What is your guess?\n");
            int[] guess = ParseIntArray(Console.ReadLine());

            // Construct pegs based on guess.
            string[] pegs = new string[5];

            for (int i = 0; i < guess.Length; i++)
            {
                for (int j = 0; j < playerOnesCode.Length; j++)
                {
                    if ((guess[i] == playerOnesCode[i]) && (i == j))
                    {
                        // Number was in the code and in the correct position.
                        pegs[i] = "BLACK";
                    }
                    else if (guess[i] == playerOnesCode[i])
                    {
                        // Number was in the code but not in the correct position.
                        pegs[i] = "WHITE";
                    }
                    else
                    {
                        // Number was not in the code at all.
                        pegs[i] = "BLANK";
                    }
                }
            }

            // Return result.
            return pegs;
        }

        /// <summary>
        /// Method called when the Player 2 has run out of attempts to crack the code.
        /// </summary>
        static void PlayerOneWins()
        {
            Console.WriteLine("\n\nPLAYER ONE WINS!\n\nPLAYER TWO HAS RUN OUT OF GUESSES.");
        }

        /// <summary>
        /// Method called when Player 2 successfully cracked the code.
        /// </summary>
        static void PlayerTwoWins()
        {
            Console.WriteLine("\n\nPLAYER TWO WINS!\n\nPLAYER TWO HAS CRACKED THE CODE.");
        }
    }
}
