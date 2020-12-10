using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.src
{
    class Day10
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>();
            StreamReader file = new StreamReader(@"../../resources/Day10.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            List<int> adapters = new List<int>();
            foreach (string adapterPower in inputs)
            {
                int.TryParse(adapterPower, out int power);
                adapters.Add(power);
            }

            adapters.Sort();

            int oneDiff = 0;
            int threeDiff = 1;
            int prev = 0;
            foreach (int power in adapters)
            {
                if (power - prev == 1)
                {
                    oneDiff++;
                }
                else if (power - prev == 3)
                {
                    threeDiff++;
                }
                prev = power;
            }

            Console.WriteLine(string.Format("oneDiff: {0}, threeDiff: {1}, output: {2}", oneDiff, threeDiff, oneDiff * threeDiff));

            HashSet<int> adapterSet = new HashSet<int>(adapters);
            adapterSet.Add(adapters[adapters.Count - 1] + 3);
            Dictionary<int, long> combinations = new Dictionary<int, long>();
            combinations[adapters[adapters.Count - 1] + 3] = 1;
            for (int i = adapters.Count - 1; i >=0; i--)
            {
                int pow = adapters[i];
                long possible = 0;
                if (adapterSet.Contains(pow + 1))
                {
                    possible += combinations[pow + 1];
                }
                if (adapterSet.Contains(pow + 2))
                {
                    possible += combinations[pow + 2];
                }
                if (adapterSet.Contains(pow + 3))
                {
                    possible += combinations[pow + 3];
                }

                
                combinations[pow] = possible;
            }

            long possible2 = 0;
            if (adapterSet.Contains(1))
            {
                possible2 += combinations[1];
            }
            if (adapterSet.Contains(2))
            {
                possible2 += combinations[2];
            }
            if (adapterSet.Contains(3))
            {
                possible2 += combinations[3];
            }
            combinations[0] = possible2;

            Console.WriteLine(combinations[0]);
        }
    }
}
