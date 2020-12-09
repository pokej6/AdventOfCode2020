using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.src
{
    class Day6
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>();
            StreamReader file = new StreamReader(@"../../resources/Day6.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            // Part 1
            List<string> answers = new List<string>();
            string current = "";
            foreach (string input in inputs)
            {
                if (input == "")
                {
                    answers.Add(current);
                    current = "";
                }
                else
                {
                    current += input;
                }
            }
            answers.Add(current);

            int result = 0;
            foreach (string answer in answers)
            {
                Dictionary<char, int> yesCounts = new Dictionary<char, int>();
                foreach (char question in answer.ToCharArray())
                {
                    yesCounts.TryGetValue(question, out int yesCount);
                    yesCount++;
                    yesCounts[question] = yesCount;
                }
                int questionsAnswered = yesCounts.Keys.Count;
                result += questionsAnswered;
            }

            Console.WriteLine(result);

            // Part 2
            Dictionary<char, int> groupCounts = new Dictionary<char, int>();
            int results = 0;
            int numPeople = 0;
            foreach (string answer in inputs)
            {
                if (answer == "")
                {
                    results += groupCounts.Keys.Where(key => groupCounts[key] == numPeople).Count();
                    numPeople = 0;
                    groupCounts.Clear();
                    continue;
                }

                numPeople++;
                foreach (char question in answer.ToCharArray())
                {
                    groupCounts.TryGetValue(question, out int yesCount);
                    yesCount++;
                    groupCounts[question] = yesCount;
                }
            }
            results += groupCounts.Keys.Where(key => groupCounts[key] == numPeople).Count();

            Console.WriteLine(results);
        }
    }
}
