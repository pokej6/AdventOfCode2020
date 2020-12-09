using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.src
{
    class Day7
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>();
            StreamReader file = new StreamReader(@"../../resources/Day7.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            Dictionary<string, List<(int, string)>> bagContents = new Dictionary<string, List<(int, string)>>();

            foreach (string bagRule in inputs) 
            {
                string[] outerParts = bagRule.Split(new string[] { " bags contain " }, StringSplitOptions.RemoveEmptyEntries);
                string[] containParts = outerParts[1].Split(',');
                List<(int, string)> innerBags = new List<(int, string)>();

                foreach (string containedBag in containParts)
                {
                    string trimmedContainedBag = containedBag.Trim();
                    int firstSpace = trimmedContainedBag.IndexOf(' ');
                    string contents = trimmedContainedBag.Substring(0, firstSpace);
                    if (contents.StartsWith("no"))
                    {
                    }
                    else
                    {
                        int numBags = int.Parse(contents);
                        string type = trimmedContainedBag.Substring(firstSpace + 1, trimmedContainedBag.LastIndexOf(' ') - firstSpace - 1);
                        innerBags.Add((numBags, type));
                    }
                    
                }

                bagContents[outerParts[0]] = innerBags;
            }

            int result = numToShinyGold(bagContents);
            Console.WriteLine(result);

            Console.WriteLine(numContained(bagContents, "shiny gold"));
        }

        static int numToShinyGold(Dictionary<string, List<(int, string)>> bagContents)
        {
            List<string> bagTypes = bagContents.Keys.ToList();
            HashSet<string> visited = new HashSet<string>();
            Queue<string> toProcess = new Queue<string>();
            int result = 0;
            bool found;
            foreach (string bagType in bagTypes)
            {
                // Console.WriteLine("starting with " + bagType);
                found = false;
                toProcess.Clear();
                visited.Clear();
                toProcess.Enqueue(bagType);
                visited.Add(bagType);

                while (toProcess.Count() > 0)
                {
                    string processing = toProcess.Dequeue();
                    foreach ((int numBags, string bagType) input in bagContents[processing])
                    {
                        if (input.bagType == "shiny gold")
                        {
                            result++;
                            found = true;
                            break;
                        }

                        if (visited.Add(input.bagType))
                        {
                            toProcess.Enqueue(input.bagType);
                        }
                    }

                    if (found)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        static long numContained(Dictionary<string, List<(int, string)>> bagContents, string bagType)
        {
            //Console.WriteLine("Processing " + bagType);
            if (bagContents[bagType].Count == 0)
            {
                return 0;
            }

            long total = 0;
            foreach ((int num, string type) input in bagContents[bagType])
            {
                long contained = numContained(bagContents, input.type);
                total += input.num + input.num * contained;
            }

            return total;
        }
    }
}
