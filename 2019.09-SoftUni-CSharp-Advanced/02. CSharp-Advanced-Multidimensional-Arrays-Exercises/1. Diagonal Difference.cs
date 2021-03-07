using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            int sizeOfMatrix = int.Parse(Console.ReadLine());
            var matrix = new int[sizeOfMatrix, sizeOfMatrix];
            for (int row = 0; row < sizeOfMatrix; row++)
            {
                var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (int col = 0; col < sizeOfMatrix; col++)
                {
                    matrix[row, col] = input[col];
                }
            }
            int mainDiagSum = 0;
            int secondDiagSum = 0;

            int r = 0;
            int c = 0;
            while (true)
            {
                if (r >= matrix.GetLength(0) || r < 0 
                    || c >= matrix.GetLength(1) || c < 0)
                {
                    break;
                }
                mainDiagSum += matrix[r, c];
                r++;
                c++;
            }

            r = 0;
            c = matrix.GetLength(1) - 1;

            while (true)
            {
                if (r >= matrix.GetLength(0) || r < 0
                    || c >= matrix.GetLength(1) || c < 0)
                {
                    break;
                }
                secondDiagSum += matrix[r, c];
                r++;
                c--;
            }
            
            Console.WriteLine(Math.Abs(mainDiagSum - secondDiagSum));
        }
    }
}
