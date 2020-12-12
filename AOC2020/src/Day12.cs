using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.src
{
    class Day12
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>();
            StreamReader file = new StreamReader(@"../../resources/Day12.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            (int x, int y) = (0, 0);
            Direction dir = Direction.EAST;

            foreach (string input in inputs)
            {
                string action = input.Substring(0, 1);
                int.TryParse(input.Substring(1), out int dist);

                switch (action)
                {
                    case "N":
                        y -= dist;
                        break;
                    case "E":
                        x += dist;
                        break;
                    case "S":
                        y += dist;
                        break;
                    case "W":
                        x -= dist;
                        break;
                    case "F":
                        (int deltaX, int deltaY) = getDelta(dir);
                        x += dist * deltaX;
                        y += dist * deltaY;
                        break;
                    case "L":
                        dir = turn(dir, "L", dist);
                        break;
                    case "R":
                        dir = turn(dir, "R", dist);
                        break;
                }

                //Console.WriteLine(string.Format("Ran {0}. Result: ({1}, {2}) facing {3}", input, x, y, dir));
            }

            Console.WriteLine(string.Format("x: {0}, y: {1}, dir: {2}", x, y, dir));
            Console.WriteLine(string.Format("manhattan: {0}", Math.Abs(x) + Math.Abs(y)));
            Console.WriteLine();

            // make north positive at this point...

            (int shipX, int shipY) = (0, 0);
            (int pointX, int pointY) = (10, 1);

            foreach (string input in inputs)
            {
                string action = input.Substring(0, 1);
                int.TryParse(input.Substring(1), out int dist);

                switch (action)
                {
                    case "N":
                        pointY += dist;
                        break;
                    case "E":
                        pointX += dist;
                        break;
                    case "S":
                        pointY -= dist;
                        break;
                    case "W":
                        pointX -= dist;
                        break;
                    case "F":
                        (shipX, shipY) = moveToPoint(shipX, shipY, pointX, pointY, dist);
                        break;
                    case "L":
                        (pointX, pointY) = rotatePoint(pointX, pointY, dist, "L");
                        break;
                    case "R":
                        (pointX, pointY) = rotatePoint(pointX, pointY, dist, "R");
                        break;
                }

                Console.WriteLine(string.Format("Ran {0}. Ship: ({1}, {2}), Point: ({3}, {4})", input, shipX, shipY, pointX, pointY));
            }
            Console.WriteLine(string.Format("Ship: ({0}, {1}), Point: ({2}, {3})", shipX, shipY, pointX, pointY));
            Console.WriteLine(string.Format("manhattan: {0}", Math.Abs(shipX) + Math.Abs(shipY)));
        }

        static (int, int) moveToPoint(int shipX, int shipY, int pointX, int pointY, int dist)
        {
            return (shipX + dist * pointX, shipY + dist * pointY);
        }

        static (int, int) rotatePoint(int deltaX, int deltaY, int dist, string turn)
        {
            int turns = dist / 90;
            int temp;
            int newSignX = 0;
            int newSignY = 0;

            for (int i = 0; i < turns; i++)
            {
                if (turn == "L")
                {
                    if (deltaX == 0)
                    {
                        newSignY = 0;
                    }
                    else
                    {
                        newSignY = deltaX / Math.Abs(deltaX);
                    }

                    if (deltaY == 0)
                    {
                        newSignX = 0;
                    }
                    else
                    {
                        newSignX = -deltaY / Math.Abs(deltaY);
                    }
                }
                else if (turn == "R")
                {
                    if (deltaX == 0)
                    {
                        newSignY = 0;
                    }
                    else
                    {
                        newSignY = -deltaX / Math.Abs(deltaX);
                    }

                    if (deltaY == 0)
                    {
                        newSignX = 0;
                    }
                    else
                    {
                        newSignX = deltaY / Math.Abs(deltaY);
                    }
                }

                temp = deltaX;
                deltaX = deltaY;
                deltaY = temp;

                deltaX = Math.Abs(deltaX) * newSignX;
                deltaY = Math.Abs(deltaY) * newSignY;
            }

            return (deltaX, deltaY);
        }

        static (int, int) getDelta(Direction dir)
        {
            switch (dir)
            {
                case Direction.NORTH:
                    return (0, -1);
                case Direction.EAST:
                    return (1, 0);
                case Direction.WEST:
                    return (-1, 0);
                case Direction.SOUTH:
                    return (0, 1);
            }

            return (0, 0);
        }

        static Direction turn(Direction current, string turn, int dist)
        {
            int numTurns = dist / 90;
            if (turn == "L")
            {
                return (Direction)(((int)current - numTurns + 4) % 4);
            }
            else
            {
                return (Direction)(((int)current + numTurns) % 4);
            }
        }
    }

    enum Direction
    {
        NORTH, EAST, SOUTH, WEST
    }
}
