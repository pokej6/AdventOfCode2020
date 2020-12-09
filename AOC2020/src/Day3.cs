using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.src
{
    class Day3
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>();
            StreamReader file = new StreamReader(@"../../resources/Day3.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            long result = 1;
            result *= numTrees(inputs, 1, 1);
            result *= numTrees(inputs, 3, 1);
            result *= numTrees(inputs, 5, 1);
            result *= numTrees(inputs, 7, 1);
            result *= numTrees(inputs, 1, 2);

            Console.WriteLine(result);
        }

        static int numTrees(List<string> inputs, int deltaX, int deltaY)
        {
            int maxCol = inputs[0].Length;
            int col = -deltaX;
            int trees = 0;
            for (int row = 0; row < inputs.Count; row += deltaY)
            {
                col = (col + deltaX) % maxCol;
                if (inputs[row].ToCharArray()[col] == '#')
                {
                    trees++;
                }
            }

            return trees;
        }
    }
}
