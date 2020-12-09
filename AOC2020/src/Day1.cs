using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2020
{
    class Day1
    {
        static void Main(string[] args)
        {
            List<int> nums = new List<int>();
            StreamReader file = new StreamReader(@"../../resources/Day1.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                int.TryParse(line, out int a);
                nums.Add(a);
            }

            void work()
            {
                for (int i = 0; i < nums.Count - 1; i++)
                {
                    for (int j = i + 1; j < nums.Count; j++)
                    {
                        if (nums[i] + nums[j] == 2020)
                        {
                            Console.WriteLine(string.Format("{0} * {1} = {2}", nums[i], nums[j], nums[i] * nums[j]));
                            return;
                        }
                    }
                }
            }

            work();

            void work2()
            {
                for (int i = 0; i < nums.Count - 2; i++)
                {
                    for (int j = i + 1; j < nums.Count - 1; j++)
                    {
                        for (int k = j + 1; k < nums.Count; k++)
                        {
                            if (nums[i] + nums[j] + nums[k] == 2020)
                            {
                                Console.WriteLine(string.Format("{0} * {1} * {2} = {3}", nums[i], nums[j], nums[k], nums[i] * nums[j] * nums[k]));
                                return;
                            }
                        }
                    }
                }
            }

            work2();
        }
    }
}
