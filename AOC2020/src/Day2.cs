using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.src
{
    class Day2
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>();
            StreamReader file = new StreamReader(@"../../resources/Day2.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            int result = 0;
            foreach (string input in inputs)
            {
                string[] parts = input.Split(':');
                string policy = parts[0];
                string password = parts[1].Trim();

                string[] policyParts = policy.Split(' ');
                string[] rangeParts = policyParts[0].Split('-');
                string restriction = policyParts[1];
                int.TryParse(rangeParts[0], out int low);
                int.TryParse(rangeParts[1], out int high);

                int count = 0;
                foreach (char c in password.ToCharArray())
                {
                    if ("" + c == restriction)
                    {
                        count++;
                        if (count > high)
                        {
                            break;
                        }
                    }
                }

                if (count >= low && count <= high)
                {
                    result++;
                }
            }

            Console.WriteLine(result);

            result = 0;
            foreach (string input in inputs)
            {
                string[] parts = input.Split(':');
                string policy = parts[0];
                char[] password = parts[1].Trim().ToCharArray();

                string[] policyParts = policy.Split(' ');
                string[] rangeParts = policyParts[0].Split('-');
                string restriction = policyParts[1];
                int.TryParse(rangeParts[0], out int posA);
                int.TryParse(rangeParts[1], out int posB);

                if (password[posA - 1]+"" == restriction ^ password[posB - 1]+"" == restriction)
                {
                    result++;
                }
            }

            Console.WriteLine(result);
        }
    }
}
