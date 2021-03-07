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

            for (int row = 0; row < rows; row++)
            {
                var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = input[col];
                }
            }
            while (true)
            {
                var commandArgs = Console.ReadLine().Split();
                if (commandArgs[0] == "END") break;
                if(commandArgs[0] != "swap" || commandArgs.Length != 5)
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                int row1 = int.Parse(commandArgs[1]);
                int col1 = int.Parse(commandArgs[2]);
                int row2 = int.Parse(commandArgs[3]);
                int col2 = int.Parse(commandArgs[4]);

                if (row1 >= rows || row1 < 0 ||
                    row2 >= rows || row2 < 0 ||
                    col1 < 0 || col1 >= cols ||
                    col2 < 0 || col2 >= cols)
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }
                var swapVar = matrix[row1, col1];
                matrix[row1, col1] = matrix[row2, col2];
                matrix[row2, col2] = swapVar;

                var matrixResult = new System.Text.StringBuilder();
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        matrixResult.Append(matrix[row, col] + " ");
                    }
                    matrixResult.Append(Environment.NewLine);
                }
                Console.Write(matrixResult);
            }
        }
    }
}
