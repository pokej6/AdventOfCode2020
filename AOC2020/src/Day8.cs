using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.src
{
    class Day8
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>();
            StreamReader file = new StreamReader(@"../../resources/Day8.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            int index = 0;
            int acc = 0;
            HashSet<int> seenInstructions = new HashSet<int>();
            while(index < inputs.Count())
            {
                if (!seenInstructions.Add(index))
                {
                    break;
                }
                string[] opParts = inputs[index].Split(' ');
                switch(opParts[0])
                {
                    case "nop":
                        index++;
                        break;
                    case "acc":
                        int.TryParse(opParts[1], out int accVal);
                        acc += accVal;
                        index++;
                        break;
                    case "jmp":
                        int.TryParse(opParts[1], out int jmpVal);
                        index += jmpVal;
                        break;
                }
            }

            Console.WriteLine("acc: " + acc);

            
            int jmpIndexToChange = 0;
            int currentJmp = 0;
            bool infinite = true;
            while (infinite)
            {
                index = 0;
                acc = 0;
                currentJmp = 0;
                seenInstructions = new HashSet<int>();
                infinite = false;

                while (index < inputs.Count())
                {
                    if (!seenInstructions.Add(index))
                    {
                        infinite = true;
                        break;
                    }
                    string[] opParts = inputs[index].Split(' ');
                    switch (opParts[0])
                    {
                        case "nop":
                            index++;
                            break;
                        case "acc":
                            int.TryParse(opParts[1], out int accVal);
                            acc += accVal;
                            index++;
                            break;
                        case "jmp":
                            if (currentJmp == jmpIndexToChange)
                            {
                                // treat as nop
                                index++;
                            }
                            else
                            {
                                int.TryParse(opParts[1], out int jmpVal);
                                index += jmpVal;
                            }
                            currentJmp++;
                            break;
                    }
                }

                jmpIndexToChange++;
            }

            Console.WriteLine("acc: " + acc);
        }
    }
}
