using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2020.src
{
    class Day4
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>();
            StreamReader file = new StreamReader(@"../../resources/Day4.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            List<string> passports = new List<string>();
            string current = "";
            bool start = true;
            foreach (string input in inputs)
            {
                if (input == "")
                {
                    passports.Add(current);
                    current = "";
                    start = true;
                }
                else
                {
                    if (start)
                    {
                        start = false;
                    }
                    else
                    {
                        current += " ";
                    }
                    current += input;
                }
            }
            passports.Add(current);

            int invalidPassports = 0;
            string[] fields = new string[] { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:" };
            foreach (string passport in passports)
            {
                if (!validFields(passport))
                {
                    invalidPassports++;
                }
            }

            Console.WriteLine(passports.Count - invalidPassports);

            invalidPassports = 0;
            foreach (string passport in passports)
            {
                if (!validFields(passport))
                {
                    invalidPassports++;
                    continue;
                }

                string[] passportFields = passport.Split(' ');
                bool valid = true;
                foreach (string passportField in passportFields)
                {
                    string[] fieldParts = passportField.Split(':');
                    switch (fieldParts[0])
                    {
                        case "byr":
                            int.TryParse(fieldParts[1], out int byr);
                            valid = byr >= 1920 && byr <= 2002;
                            break;
                        case "iyr":
                            int.TryParse(fieldParts[1], out int iyr);
                            valid = iyr >= 2010 && iyr <= 2020;
                            break;
                        case "eyr":
                            int.TryParse(fieldParts[1], out int eyr);
                            valid = eyr >= 2020 && eyr <= 2030;
                            break;
                        case "hgt":
                            string units = fieldParts[1].Substring(fieldParts[1].Length - 2);
                            int.TryParse(fieldParts[1].Substring(0, fieldParts[1].Length - 2), out int hgt);
                            if (units == "cm")
                            {
                                valid = hgt >= 150 && hgt <= 193;
                            }
                            else if (units == "in")
                            {
                                valid = hgt >= 59 && hgt <= 76;
                            }
                            else
                            {
                                valid = false;
                            }
                            break;
                        case "hcl":
                            valid = Regex.IsMatch(fieldParts[1], @"^#[a-f0-9]{6}$");
                            break;
                        case "ecl":
                            valid = fieldParts[1] == "amb" ||
                                fieldParts[1] == "blu" ||
                                fieldParts[1] == "brn" ||
                                fieldParts[1] == "gry" ||
                                fieldParts[1] == "grn" ||
                                fieldParts[1] == "hzl" ||
                                fieldParts[1] == "oth";
                            break;
                        case "pid":
                            valid = Regex.IsMatch(fieldParts[1], @"^[0-9]{9}$");
                            break;
                    }

                    if (!valid)
                    {
                        invalidPassports++;
                        break;
                    }
                }
            }

            Console.WriteLine(passports.Count - invalidPassports);
        }

        static bool validFields(string passport)
        {
            string[] fields = new string[] { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:" };
            foreach (string field in fields)
            {
                if (!passport.Contains(field))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
