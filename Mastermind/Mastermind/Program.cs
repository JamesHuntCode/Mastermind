using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get player 1's input (4 random non-negative integers smaller than 10).
            Console.WriteLine("Player 1 - input a sequence of 4 random positive integers smaller than 10, each seperated by a comma:\n");
            string sequenceToCrack = Console.ReadLine();

            // Parse user input into an array of integers & catch any exceptions.
            int[] crackMe = new int[4];

            try
            {
                crackMe[0] = Convert.ToInt32(sequenceToCrack.Split(',')[0]);
                crackMe[1] = Convert.ToInt32(sequenceToCrack.Split(',')[1]);
                crackMe[2] = Convert.ToInt32(sequenceToCrack.Split(',')[2]);
                crackMe[3] = Convert.ToInt32(sequenceToCrack.Split(',')[3]);
            }
            catch (Exception)
            {
                Console.WriteLine("There is an error in your input. Please input a sequence of 4 non-negative integers smaller than 10, each seperated by a comma.\n\n");
                Main(args);
            }

            
        }
    }
}
