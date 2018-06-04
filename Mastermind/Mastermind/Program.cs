using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    /// <summary>
    /// Dynamically linked list data structure.
    /// </summary>
    class LinkedList
    {
        /// <summary>
        /// List head.
        /// </summary>
        public Node ListHead = new Node();

        /// <summary>
        /// Determine if list head has been set or remains unused.
        /// </summary>
        public bool Initialized { get; set; } = false;

        /// <summary>
        /// Method to add a new node to the end of the linked list.
        /// </summary>
        /// <param name="newNode"></param>
        public void AddNode(Node newNode)
        {
            Node currentNode = ListHead;

            if (currentNode.NextNode == null)
            {
                currentNode.NextNode = newNode;
                newNode.NextNode = null;
            }
            else
            {
                while (currentNode.NextNode != null)
                {
                    currentNode = currentNode.NextNode;

                    if (currentNode.NextNode == null)
                    {
                        currentNode.NextNode = newNode;
                        newNode.NextNode = null;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Object used for storage inside the linked list data structure defined above.
    /// </summary>
    class Node
    {
        /// <summary>
        /// Integer array used to hold value of player two guesses.
        /// </summary>
        public int[] Data { get; set; }

        /// <summary>
        /// The next Node value in the linked list.
        /// </summary>
        public Node NextNode = null;
    }

    /// <summary>
    /// Main program space.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Code set by Player 1 for Player 2 to crack.
        /// </summary>
        private static int[] playerOnesCode;

        /// <summary>
        /// Guesses remaining for Player 2 to correctly crack Player One's code.
        /// </summary>
        private static int remainingGuesses = 10;

        /// <summary>
        /// Boolean value to indicate if Player 2 has cracked the code.
        /// </summary>
        private static bool codeCracked = false;

        /// <summary>
        /// Variable to store the most recent guess to crack the code submitted by Player 2.
        /// </summary>
        private static int[] playerTwosMostRecentGuess;

        static void Main(string[] args)
        {
            // Take players one's code for player two to break.
            playerOnesCode = TakeCodeToCrack();
            Console.WriteLine("\nGreat! Your input has been logged.");

            // Configure Linked List.
            LinkedList previousGuesses = new LinkedList();

            // Begin taking guesses from player two.
            while ((!codeCracked) && (remainingGuesses >= 0))
            {
                // Start recording player twos's guesses in linked list.
                if (!previousGuesses.Initialized)
                {
                    previousGuesses.ListHead.Data = playerTwosMostRecentGuess;
                    previousGuesses.ListHead.NextNode = null;
                    previousGuesses.Initialized = true;
                }

                // Get guess from player two.
                string[] userGuess = TakeGuess();

                codeCracked = CompareArrays(playerTwosMostRecentGuess, playerOnesCode);

                if (codeCracked)
                {
                    // Player two has cracked the code.
                    Console.WriteLine("\n" + String.Join(" ", userGuess) + "\n");
                    codeCracked = true;
                }
                else
                {
                    // Player two's guess was incorrect.
                    remainingGuesses--;
                    Console.WriteLine("\nYour guess was incorrect. You have " + remainingGuesses.ToString() + " lives remaining. Try again.\n");
                    Console.WriteLine("\n" + String.Join(" ", userGuess) + "\n\n");
                }

                // Add most recent guess to linked list of guesses.
                Node newNode = new Node();
                newNode.Data = playerTwosMostRecentGuess;
                previousGuesses.AddNode(newNode);
            }

            if (codeCracked)
            {
                // Player two has cracked the code.
                PlayerTwoWins(previousGuesses);
            }
            else
            {
                // Player two has failed to crack the code.
                PlayerOneWins(previousGuesses);
            }
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
                Console.WriteLine("\nOops! Please input 4 non-negative integers, each below 10 and seperated by a comma:\n");
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
                    if ((parsedArray[i] >= 10) || (parsedArray[i] < 0))
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

            while (guess == null)
            {
                Console.WriteLine("\nOops! Please input 4 non-negative integers, each below 10 and seperated by a comma:\n");
                guess = ParseIntArray(Console.ReadLine());
            }

            playerTwosMostRecentGuess = guess;
           
            // Construct pegs based on guess.
            string[] pegs = new string[4];

            for (int i = 0; i < guess.Length; i++)
            {
                // Check for a black peg.
                if (guess[i] == playerOnesCode[i])
                {
                    pegs[i] = "BLACK";                  
                }
                else
                {
                    // Check for a white peg.
                    bool occurs = false;
                    int timesOccured = 0;
      
                    // Check times the number has occured in player two's guess.
                    for (int j = 0; j < playerOnesCode.Length; j++)
                    {
                        if ((guess[i] == playerOnesCode[j]))
                        {
                            timesOccured++;
                            if (!occurs) occurs = true;
                        }
                    }

                    // Check how many times the number specified above appears in player one's code.
                    int limit = 0;

                    for (int k = 0; k < playerOnesCode.Length; k++)
                    {
                        if ((guess[i] == playerOnesCode[k]))
                        {
                            limit++;
                        }
                    }

                    // Assign peg based on result.
                    if (occurs && (limit >= timesOccured))
                    {
                        pegs[i] = "WHITE";
                    }
                    else
                    {
                        pegs[i] = "BLANK";
                    }
                }
            }

            // Return result.
            return pegs;
        }

        /// <summary>
        /// Method to take two integer arrays and compare each element against each other to derive a match.
        /// </summary>
        /// <param name="firstArray"></param>
        /// <param name="secondArray"></param>
        /// <returns></returns>
        static bool CompareArrays(int[] firstArray, int[] secondArray)
        {
            int arrayLengths = firstArray.Length;

            for (int i = 0; i < arrayLengths; i++)
            {
                // If any single element is not the same between the two arrays, return false.
                if (firstArray[i] != secondArray[i])
                {
                    // No match.
                    return false;
                }
            }

            // All elements match up perfectly - there is a match.
            return true;
        }

        /// <summary>
        /// Method called when the Player 2 has run out of attempts to crack the code.
        /// </summary>
        static void PlayerOneWins(LinkedList allGuesses)
        {
            Console.WriteLine("\n\nPLAYER ONE WINS!\n\nPLAYER TWO HAS RUN OUT OF GUESSES.\n\n");
            Console.ReadKey();

            // Write out all guesses made by player two from the linked list.
            Console.WriteLine("Here are the guesses you made throughout the game:\n\n");
            Console.WriteLine(FormatGuesses(ObtainGuesses(allGuesses)));
            Console.ReadKey();
        }

        /// <summary>
        /// Method called when Player 2 successfully cracked the code.
        /// </summary>
        static void PlayerTwoWins(LinkedList allGuesses)
        {
            Console.WriteLine("\n\nPLAYER TWO WINS!\n\nPLAYER TWO HAS CRACKED THE CODE.\n\n");
            Console.ReadKey();

            // Write out all guesses made by player two from the linked list.
            Console.WriteLine("Here are the guesses you made throughout the game:\n\n");
            Console.WriteLine(FormatGuesses(ObtainGuesses(allGuesses)));
            Console.ReadKey();
        }

        /// <summary>
        /// Method to traverse linked list and return out all of the guesses made by Player 2.
        /// </summary>
        /// <returns></returns>
        static List<int[]> ObtainGuesses(LinkedList ListStructure)
        {
            List<int[]> data = new List<int[]>();

            Node currentNode = ListStructure.ListHead;

            while (currentNode.NextNode != null)
            {
                data.Add(currentNode.Data);

                currentNode = currentNode.NextNode;
            }

            return data;
        }

        /// <summary>
        /// Method to format the integer arrays into string displayable within the console.
        /// </summary>
        /// <param name="guesses"></param>
        /// <returns></returns>
        static string FormatGuesses(List<int[]> guesses)
        {
            return guesses.Count.ToString();
        }
    }
}
