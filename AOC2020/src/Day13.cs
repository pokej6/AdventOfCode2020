using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.src
{
    class Day13
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>();
            StreamReader file = new StreamReader(@"../../resources/Day13.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            int.TryParse(inputs[0], out int earliest);
            List<int> busIds = new List<int>();
            foreach (string id in inputs[1].Split(','))
            {
                if (id != "x")
                {
                    int.TryParse(id, out int busId);
                    busIds.Add(busId);
                }
                else
                {
                    busIds.Add(0);
                }
            }

            int lowestBusId = int.MaxValue;
            int lowestTime = int.MaxValue;
            foreach (int busId in busIds)
            {
                if (busId == 0)
                {
                    continue;
                }
                int timeAfter = busId - (earliest % busId);
                if (timeAfter < lowestTime)
                {
                    lowestTime = timeAfter;
                    lowestBusId = busId;
                }
            }

            Console.WriteLine("Earliest: {0}, Wait Time: {1}, Answer: {2}", lowestBusId, lowestTime, lowestBusId * lowestTime);
            Console.WriteLine("---");

            //Console.WriteLine(chineseRemainder(new List<(int, int)> { (3, 5), (1, 7), (6, 8)}));

            List<(long, long)> formattedBus = new List<(long, long)>();
            for (int i = 0; i < busIds.Count; i++)
            {
                int busId = busIds[i];
                if (busId == 0)
                {
                    continue;
                }

                int baseNum = ((busId - i) % busId + busId) % busId;
                formattedBus.Add((baseNum, busId));
            }

            foreach ((int,int) bus in formattedBus)
            {
                Console.WriteLine(bus);
            }
            Console.WriteLine(chineseRemainder(formattedBus));
        }

        static long chineseRemainder(List<(long, long)> mods)
        {
            long result = 0;
            long N = mods.Aggregate(1L, (total, next) => total * next.Item2);
            foreach ((long, long) mod in mods)
            {
                long bi = mod.Item1;
                long Ni = N / mod.Item2;
                long xi;
                long remainder = Ni % mod.Item2;
                long baseNum = remainder;
                while (baseNum % mod.Item2 != 1)
                {
                    baseNum += remainder;
                }
                xi = baseNum / remainder;
                Console.WriteLine("bi: {0}, Ni: {1}, xi: {2}, N: {3}", bi, Ni, xi, N);
                result += bi * Ni * xi;
            }

            return result % N;
        }
    }
}
