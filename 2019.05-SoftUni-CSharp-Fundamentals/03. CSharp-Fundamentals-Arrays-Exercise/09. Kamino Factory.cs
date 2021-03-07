using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int length = int.Parse(Console.ReadLine());
        int[] longestSubsequenceOfOnes = new int[length];
        short bestSequenceIndex = 1;
        //int bestSequenceSum = 0;
        short counter = 0;

        short match = 0;
        short maxMatch = 0;
        int wheresTheMatch = 0;

        while (true)
        {
            counter++;
            string sequence = Console.ReadLine();
            if (sequence != "Clone them!")
            {
                int[] dnaSequence = sequence
                    .Split("!", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int i = 1; i < length; i++)
                {
                    int current = dnaSequence[i];
                    int previous = dnaSequence[i - 1];

                    if (current == previous)
                    {
                        match++;
                        if (match > maxMatch)
                        {
                            wheresTheMatch = i - 1;
                            maxMatch = match;
                            longestSubsequenceOfOnes = dnaSequence;
                            bestSequenceIndex = counter;
                        }
                        else if (match == maxMatch)
                        {
                            if (wheresTheMatch > i)
                            {
                                wheresTheMatch = i - 1;
                                maxMatch = match;
                                longestSubsequenceOfOnes = dnaSequence;
                                bestSequenceIndex = counter;
                            }
                        }
                    }
                }
            match = 0;
            }
            else break;
        }
        Console.WriteLine($"Best DNA sample {bestSequenceIndex} with sum: {longestSubsequenceOfOnes.Sum()}.");
        Console.WriteLine(String.Join(" ", longestSubsequenceOfOnes));
    }
}