using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Solution
    {
        //non optimized solution
        public static char? FindFirstNonRepeatingChar(string str)
        {
            Dictionary<char, int> charCount = new();
            foreach (char charecter in str)
            {
                if (charCount.ContainsKey(charecter)) charCount[charecter]++;
                else charCount[charecter] = 1;
            }

            foreach (char charecter in str)
            {
                if (charCount[charecter] == 1) return charecter;
            }
            return null;
        }

        //Followup question:
        private const int CHAR_SIZE = 256;
        public static char? FindFirstNonRepeatingCharOptimizedSolution(string str)
        {
            //edge case analysis:
            //
            if(string.IsNullOrEmpty(str)) return null;
            if(str.Length == 1) return str[0];

            Span<int> charCount = stackalloc int[CHAR_SIZE];

            for (int i = 0; i < str.Length; i++)
            {
                charCount[str[i]]++;

                if (i > CHAR_SIZE && !charCount.Contains(1))
                    return null;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (charCount[str[i]] == 1) return str[i];
            }
            return null;
        }
        // For stream of chars:


    }

    public class StreamCharacterFinder
    {
        private const int CHAR_SIZE = 256;
        private readonly int[] charCount;
        private readonly LinkedList<char> nonRepeatingChars;

        public StreamCharacterFinder()
        {
            charCount = new int[CHAR_SIZE];
            nonRepeatingChars = new LinkedList<char>();
        }

        public void processNextChar(char c)
        {
            charCount[c]++;
            if (charCount[c] == 1) nonRepeatingChars.AddLast(c);
            else nonRepeatingChars.Remove(c);
        }

        public char? GetFirstNonRepeating()
        {
            if (nonRepeatingChars.Count > 0) return nonRepeatingChars.First.Value;
            return null;
        }
        public void Reset()
        {
            Array.Clear(charCount, 0, CHAR_SIZE);
            nonRepeatingChars.Clear();
        }
    }
}

