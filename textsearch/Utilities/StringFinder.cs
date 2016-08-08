using System;
using System.Collections.Generic;
using System.Linq;

namespace textsearch.Utilities
{
    class StringFinder
    {
        public List<String> FindMatchExactly(List<String> input, List<String> patterns)
        {
            return (from patternLine in patterns from inputLine in input where inputLine.Equals(patternLine) select inputLine).ToList();
        }

        public List<String> FindContainsInTheLine(List<String> input, List<String> patterns)
        {
            return (from patternLine in patterns from inputLine in input where inputLine.Contains(patternLine) select inputLine).ToList();
        }

        public List<String> FindContainsWithEditDistance(List<String> input, List<String> patterns, int distance)
        {
            return (from patternLine in patterns from inputLine in input where FindDistance(inputLine, patternLine) <= distance select inputLine).ToList();
        }

        private int Minimum(int a, int b, int c)
        {
            return Math.Min(Math.Min(a, b), c);
        }

        private int FindDistance(String inputLine, String patternLine)
        {
            int[,] distance = new int[inputLine.Count() + 1, patternLine.Count() + 1];

            for (int i = 0; i <= inputLine.Count(); i++)
                distance[i,0] = i;
            for (int j = 1; j <= patternLine.Count(); j++)
                distance[0,j] = j;

            for (int i = 1; i <= inputLine.Count(); i++)
                for (int j = 1; j <= patternLine.Count(); j++)
                    distance[i,j] = Minimum(
                            distance[i - 1,j] + 1,
                            distance[i,j - 1] + 1,
                            distance[i - 1, j - 1] + ((inputLine.ElementAt(i - 1) == patternLine.ElementAt(j - 1)) ? 0 : 1));

            return distance[inputLine.Count(), patternLine.Count()];
        }
    }
}
