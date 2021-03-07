using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main()
        {
            var sentence = Console.ReadLine().ToCharArray();
            for(int i = 0; i < sentence.Length; i++)
            {
                sentence[i] = (char)(sentence[i] + 3);
            }
            Console.WriteLine(sentence);
        }
    }
}