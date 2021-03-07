using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            var sizeOfMatrix = Console.ReadLine().Split();
            var rows = int.Parse(sizeOfMatrix[0]);
            var cols = int.Parse(sizeOfMatrix[1]);
            var matrix = new string[rows, cols];
            byte subMatrices = 0;

            for (int row = 0; row < rows; row++)
            {
                var input = Console.ReadLine().Split();
                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = input[col];
                }
            }

            for (int row = 0; row < rows - 1; row++)
            {
                for (int col = 0; col < cols - 1; col++)
                {
                    var currentElement = matrix[row, col];
                    var rightEl = matrix[row, col + 1];
                    var downEl = matrix[row + 1, col];
                    var diagEl = matrix[row + 1, col + 1];
                    if(currentElement == rightEl &&
                       currentElement == downEl &&
                       currentElement == diagEl)
                    {
                        subMatrices++;
                    }
                }
            }
            Console.WriteLine(subMatrices);
        }
    }
}
