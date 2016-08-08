using System;
using System.Collections.Generic;
using System.IO;
using textsearch.Utilities;

namespace textsearch
{
    class Program
    {
        private List<String> _input;
        private List<String> _patterns;
 
        static void Main(string[] args)
        {
            try
            {
                Program prog = new Program();
                prog.Start(args[0], args[1], args[2]);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message +
                                  Environment.NewLine +
                                  "Run  textsearch -inputTextFilePath -patternsTextFilePath -mode" +
                                  Environment.NewLine +
                                  "Parameter -mode can be: " +
                                  Environment.NewLine +
                                  "1 - Output all the lines from input.txt that match exactly any pattern in patterns.txt" +
                                  Environment.NewLine +
                                  "2 - Output all the lines from input.txt that contain a match from patterns.txt somewhere in the line" +
                                  Environment.NewLine +
                                  "3 - Output all the lines from input.txt that contain a match with edit distance <= 1 patterns.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Start(String inputPath, String patternsPath, String mode)
        {
            _input = ReadFromTextFile(inputPath);
            _patterns = ReadFromTextFile(patternsPath);

            StringFinder stringFinder = new StringFinder();

            switch (mode)
            {
                case "1":
                    PrintLines(stringFinder.FindMatchExactly(_input, _patterns));
                    break;
                case "2":
                    PrintLines(stringFinder.FindContainsInTheLine(_input, _patterns));
                    break;
                case "3":
                    PrintLines(stringFinder.FindContainsWithEditDistance(_input, _patterns, 1));
                    break;
                default :
                    throw new IndexOutOfRangeException();
            }
        }

        private void PrintLines(List<String> data)
        {
            if (data.Count == 0)
                Console.WriteLine("No match data");
            else
                foreach (var line in data)
                {
                    Console.WriteLine(line);
                }
            Console.WriteLine();
        }

        private List<String> ReadFromTextFile(String fullPath)
        {
            List<String> linesList = new List<string>();
            string line;
            StreamReader file = new StreamReader(fullPath);
            while ((line = file.ReadLine()) != null)
            {
                linesList.Add(line);
            }
            file.Close();
            return linesList;
        }
    }
}
