using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.src
{
    class Day11
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>();
            StreamReader file = new StreamReader(@"../../resources/Day11.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            string[,] seats = new string[inputs.Count, inputs[0].Count()];
            string[,] orig = new string[inputs.Count, inputs[0].Count()];
            for (int i = 0; i < inputs.Count; i++)
            {
                char[] input = inputs[i].ToCharArray();
                for (int j = 0; j < input.Count(); j++)
                {
                    seats[i, j] = input[j] + "";
                    orig[i, j] = input[j] + "";
                }
            }

            bool changeHappened = true;
            while (changeHappened)
            {
                (seats, changeHappened) = nextSeats(seats, numNeighbors, 4);
            }

            int numOccupied = 0;
            int rowLength = seats.GetLength(0);
            int colLength = seats.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (seats[i, j] == "#")
                    {
                        numOccupied++;
                    }
                }
            }

            Console.WriteLine(string.Format("occupied: {0}", numOccupied));

            seats = orig;

            changeHappened = true;
            while (changeHappened)
            {
                (seats, changeHappened) = nextSeats(seats, numSeen, 5);
                //printThing(seats);
            }

            numOccupied = 0;
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (seats[i, j] == "#")
                    {
                        numOccupied++;
                    }
                }
            }

            Console.WriteLine(string.Format("occupied: {0}", numOccupied));
        }

        static int numNeighbors(string[,] seats, int row, int col)
        {
            int result = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i == row && j == col)
                    {
                        continue;
                    }
                    try
                    {
                        if (seats[i, j] == "#")
                        {
                            result++;
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        // pass
                    }
                }
            }

            return result;
        }

        static int numSeen(string[,] seats, int row, int col)
        {
            int result = 0;
            //printThing(seats);
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    bool done = false;
                    int curX = col;
                    int curY = row;
                    while (!done)
                    {
                        curX += j;
                        curY += i;
                        if (i == 0 && j == 0)
                        {
                            break;
                        }
                        try
                        {
                            if (seats[curY, curX] == "#")
                            {
                                done = true;
                                result++;
                            }
                            else if (seats[curY, curX] == "L")
                            {
                                done = true;
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            done = true;
                        }
                    }
                }
            }

            //Console.WriteLine(string.Format("Found {0} seen for row: {1} col {2}", result, row, col));

            return result;
        }

        static void printThing(string[,] seats)
        {
            int rowLength = seats.GetLength(0);
            int colLength = seats.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0}", seats[i, j]));
                }
                Console.Write(Environment.NewLine);
            }
            Console.Write(Environment.NewLine);
        }

        public delegate int NeighborFunc(string[,] seats, int row, int col);
        static (string[,], bool) nextSeats(string[,] seats, NeighborFunc numNeighbor, int reqNumNeighbors)
        {
            int rowLength = seats.GetLength(0);
            int colLength = seats.GetLength(1);
            string[,] next = new string[rowLength, colLength];
            bool changeHappened = false;
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (seats[i, j] == "L")
                    {
                        if (numNeighbor(seats, i, j) == 0)
                        {
                            next[i, j] = "#";
                            changeHappened = true;
                        }
                        else
                        {
                            next[i, j] = "L";
                        }
                    }
                    else if (seats[i, j] == "#")
                    {
                        if (numNeighbor(seats, i, j) >= reqNumNeighbors)
                        {
                            next[i, j] = "L";
                            changeHappened = true;
                        }
                        else
                        {
                            next[i, j] = "#";
                        }
                    }
                    else
                    {
                        next[i, j] = ".";
                    }
                }
            }

            return (next, changeHappened);
        }
    }
}
