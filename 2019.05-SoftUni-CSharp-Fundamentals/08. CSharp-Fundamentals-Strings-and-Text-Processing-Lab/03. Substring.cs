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
            string word = Console.ReadLine().ToLower();
            string secondWord = Console.ReadLine();

            while(true)
            {
                int index = secondWord.IndexOf(word);
                if(index < 0)
                {
                    break;
                }
                secondWord = secondWord.Remove(index, word.Length);
            }
            Console.WriteLine(secondWord);
        }
    }
}