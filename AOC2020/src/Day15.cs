using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.src
{
    class Day15
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>();
            StreamReader file = new StreamReader(@"../../resources/Day15.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            List<int> nums = inputs[0].Split(',').Select(x => int.Parse(x)).ToList();
            Dictionary<int, List<int>> spokenTime = new Dictionary<int, List<int>>();

            int lastNum = -1;
            for (int i = 1; i <= 2020; i++)
            {
                if (i <= nums.Count)
                {
                    Console.WriteLine("Turn {0}: This is a starting turn. Speaking {1}", i, nums[i - 1]);
                    spokenTime[nums[i - 1]] = new List<int> { i };
                    lastNum = nums[i - 1];
                    continue;
                }

                spokenTime.TryGetValue(lastNum, out List<int> lastSpokenTime);
                if (lastSpokenTime == null)
                {
                    lastSpokenTime = new List<int>();
                    spokenTime[lastNum] = lastSpokenTime;
                }

                // this number has never been spoken before last turn
                if (lastSpokenTime.Count == 0 || lastSpokenTime.Count == 1 && lastSpokenTime[0] == i - 1)
                {
                    Console.WriteLine("Turn {0}: Never spoken {1}. Speaking 0", i, lastNum);
                    // add the current time
                    lastNum = 0;
                    spokenTime[lastNum].Add(i);
                }
                else
                {
                    int newNum = i - 1 - lastSpokenTime[lastSpokenTime.Count - 2];
                    Console.WriteLine("Turn {0}: Last spoke {1} on Turn {2}. Speaking {3}", i, lastNum, lastSpokenTime[lastSpokenTime.Count - 2], newNum);
                    spokenTime.TryGetValue(newNum, out List<int> newNumLastSpokenTime);
                    if (newNumLastSpokenTime == null)
                    {
                        newNumLastSpokenTime = new List<int>();
                        spokenTime[newNum] = newNumLastSpokenTime;
                    }
                    newNumLastSpokenTime.Add(i);
                    lastNum = newNum;
                }
            }

            Console.WriteLine(lastNum);

            spokenTime.Clear();

            lastNum = -1;
            for (int i = 1; i <= 30000000; i++)
            {
                if (i <= nums.Count)
                {
                    Console.WriteLine("Turn {0}: This is a starting turn. Speaking {1}", i, nums[i - 1]);
                    spokenTime[nums[i - 1]] = new List<int> { i };
                    lastNum = nums[i - 1];
                    continue;
                }

                spokenTime.TryGetValue(lastNum, out List<int> lastSpokenTime);
                if (lastSpokenTime == null)
                {
                    lastSpokenTime = new List<int>();
                    spokenTime[lastNum] = lastSpokenTime;
                }

                // this number has never been spoken before last turn
                if (lastSpokenTime.Count == 0 || lastSpokenTime.Count == 1 && lastSpokenTime[0] == i - 1)
                {
                    //Console.WriteLine("Turn {0}: Never spoken {1}. Speaking 0", i, lastNum);
                    // add the current time
                    lastNum = 0;
                    spokenTime[lastNum].Add(i);
                }
                else
                {
                    int newNum = i - 1 - lastSpokenTime[lastSpokenTime.Count - 2];
                    //Console.WriteLine("Turn {0}: Last spoke {1} on Turn {2}. Speaking {3}", i, lastNum, lastSpokenTime[lastSpokenTime.Count - 2], newNum);
                    spokenTime.TryGetValue(newNum, out List<int> newNumLastSpokenTime);
                    if (newNumLastSpokenTime == null)
                    {
                        newNumLastSpokenTime = new List<int>();
                        spokenTime[newNum] = newNumLastSpokenTime;
                    }
                    newNumLastSpokenTime.Add(i);
                    lastNum = newNum;
                }
            }

            Console.WriteLine(lastNum);
        }
    }
}
