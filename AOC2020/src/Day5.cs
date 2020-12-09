using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2020.src
{
    class Day5
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>();
            StreamReader file = new StreamReader(@"../../resources/Day5.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            int maxSid = 0;
            HashSet<int> sids = new HashSet<int>();
            foreach (string input in inputs)
            {
                int low = 0;
                int high = 127;
                for (int i = 0; i < 7; i++)
                {
                    if (input[i] == 'F')
                    {
                        high = (high + low) / 2;
                    }
                    else
                    {
                        low = (high + low) / 2 + 1;
                    }
                }

                int row = low;
                low = 0;
                high = 7;
                for (int i = 7; i < 10; i++)
                {
                    if (input[i] == 'L')
                    {
                        high = (high + low) / 2;
                    }
                    else
                    {
                        low = (high + low) / 2 + 1;
                    }
                }
                int col = low;

                // Console.WriteLine(string.Format("row: {0}, col: {1}", row, col));
                int sid = row * 8 + col;
                if (sid > maxSid)
                {
                    maxSid = sid;
                }
                sids.Add(sid);
            }

            Console.WriteLine(maxSid);

            for (int i = 1; i < maxSid - 1; i++)
            {
                if (!sids.Contains(i) && sids.Contains(i - 1) && sids.Contains(i + 1))
                {
                    Console.WriteLine(string.Format("my seat? {0}", i));
                }
            }
        }
    }
}
