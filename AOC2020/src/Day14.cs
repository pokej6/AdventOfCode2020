using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.src
{
    class Day14
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>();
            StreamReader file = new StreamReader(@"../../resources/Day14.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            char[] mask = new char[0];
            Dictionary<long, long> memory = new Dictionary<long, long>();

            for (int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i].StartsWith("mask"))
                {
                    string strMask = inputs[i].Split('=')[1].Trim();
                    Console.WriteLine("\nmask:   {0}", strMask);
                    mask = strMask.ToCharArray();
                    continue;
                }
                string[] memParts = inputs[i].Split(new string[] { " = " }, StringSplitOptions.RemoveEmptyEntries);
                long.TryParse(memParts[0].Substring(4, memParts[0].LastIndexOf(']') - 4), out long memLocation);
                Console.WriteLine("memLoc: {0}  (decimal {1})", Convert.ToString(memLocation, 2).PadLeft(mask.Length, '0'), memLocation);
                long.TryParse(memParts[1], out long value);
                Console.WriteLine("value:  {0}  (decimal {1})", Convert.ToString(value, 2).PadLeft(mask.Length, '0'), value);

                // apply mask
                for (int j = 0; j < mask.Length; j++)
                {
                    if (mask[j] == '1' || mask[j] == '0')
                    {
                        int intMask = int.Parse("" + mask[j]);
                        //Console.WriteLine("Setting {0} bit to {1}", (mask.Length - j - 1), intMask);
                        value = setBit(value, mask.Length - j - 1, intMask);
                    }
                }
                Console.WriteLine("result: {0}  (decimal {1})", Convert.ToString(value, 2).PadLeft(mask.Length, '0'), value);
                memory[memLocation] = value;
            }

            long result = memory.Values.Sum();
            Console.WriteLine(result);

            // part 2
            memory.Clear();

            for (int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i].StartsWith("mask"))
                {
                    string strMask = inputs[i].Split('=')[1].Trim();
                    Console.WriteLine("\nmask:   {0}", strMask);
                    mask = strMask.ToCharArray();
                    continue;
                }
                string[] memParts = inputs[i].Split(new string[] { " = " }, StringSplitOptions.RemoveEmptyEntries);
                long.TryParse(memParts[0].Substring(4, memParts[0].LastIndexOf(']') - 4), out long memLocation);
                Console.WriteLine("memLoc: {0}  (decimal {1})", Convert.ToString(memLocation, 2).PadLeft(mask.Length, '0'), memLocation);
                long.TryParse(memParts[1], out long value);
                Console.WriteLine("value:  {0}  (decimal {1})", Convert.ToString(value, 2).PadLeft(mask.Length, '0'), value);

                // apply mask
                List<int> floatingBits = new List<int>();
                for (int j = 0; j < mask.Length; j++)
                {
                    if (mask[j] == '1')
                    {
                        int intMask = int.Parse("" + mask[j]);
                        //Console.WriteLine("Setting {0} bit to {1}", (mask.Length - j - 1), intMask);
                        memLocation = setBit(memLocation, mask.Length - j - 1, intMask);
                    }
                    else if (mask[j] == 'X')
                    {
                        floatingBits.Add(mask.Length - j - 1);
                    }
                }

                // change 1 and 4
                // j: [0, 4]

                long numCombos = 1 << floatingBits.Count;
                for (int j = 0; j < numCombos; j++)
                {
                    //Console.WriteLine("Replacing bits with {0}", Convert.ToString(j, 2).PadLeft(floatingBits.Count, '0'));
                    long floatingMemLocation = memLocation;
                    for (int k = 0; k < floatingBits.Count; k++)
                    {
                        int bit = floatingBits[k];
                        int bitVal = (j & (1 << k)) >> k;
                        //Console.WriteLine("Replacing bit {0} with {1}", bit, bitVal);
                        floatingMemLocation = setBit(floatingMemLocation, bit, bitVal);
                    }
                    memory[floatingMemLocation] = value;
                    //Console.WriteLine("result: {0}  (decimal {1})", Convert.ToString(floatingMemLocation, 2).PadLeft(mask.Length, '0'), floatingMemLocation);
                }
            }

            result = memory.Values.Sum();
            Console.WriteLine(result);
        }

        static long setBit(long num, int bit, int val)
        {
            return num ^ (-val ^ num) & (1L << bit);
        }
    }
}
