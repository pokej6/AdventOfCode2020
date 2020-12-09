using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.src
{
    class Day9
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>();
            StreamReader file = new StreamReader(@"../../resources/Day9.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            List<long> allNum = new List<long>();
            List<long> last25 = new List<long>();
            long num = 0;
            int preambleLength = 25;
            foreach (string number in inputs)
            {
                long.TryParse(number, out num);
                allNum.Add(num);
                if (last25.Count >= preambleLength)
                {
                    bool foundSum = false;
                    for (int i = 0; i < last25.Count; i++)
                    {
                        for (int j = i + 1; j < last25.Count; j++)
                        {
                            if (last25[i] + last25[j] == num)
                            {
                                foundSum = true;
                                break;
                            }
                        }

                        if (foundSum)
                        {
                            break;
                        }
                    }
                    if (!foundSum)
                    {
                        break;
                    }
                }

                last25.Add(num);
                if (last25.Count > preambleLength)
                {
                    last25.RemoveAt(0);
                }
            }

            Console.WriteLine(num);

            long lowContend = 0;
            long highContend = 0;
            for (int j = 0; j < allNum.Count; j++)
            {
                long sum = allNum[j];
                int i = j + 1;
                highContend = lowContend = sum;
                while (sum < num)
                {
                    sum += allNum[i];
                    if (sum > num)
                    {
                        break;
                    }
                    if (allNum[i] > highContend)
                    {
                        highContend = allNum[i];
                    }
                    if (allNum[i] < lowContend)
                    {
                        lowContend = allNum[i];
                    }
                    i++;
                }

                if (sum == num)
                {
                    break;
                }
            }
            


            Console.WriteLine(string.Format("high: {0}, low: {1}, weak: {2}", highContend, lowContend, highContend + lowContend));
        }
    }
}
