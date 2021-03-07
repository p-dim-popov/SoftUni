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

            var matrix = new int[rows, cols];
            int maximumSum = int.MinValue;
            int[,] maxSumMatrix = new int[3, 3];

            for (int row = 0; row < rows; row++)
            {
                var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = input[col];
                }
            }

            for (int row = 0; row < rows - 2; row++)
            {
                for (int col = 0; col < cols - 2; col++)
                {
                    var a00 = matrix[row, col];
                    var a01 = matrix[row, col + 1];
                    var a02 = matrix[row, col + 2];

                    var a10 = matrix[row + 1, col];
                    var a11 = matrix[row + 1, col + 1];
                    var a12 = matrix[row + 1, col + 2];

                    var a20 = matrix[row + 2, col];
                    var a21 = matrix[row + 2, col + 1];
                    var a22 = matrix[row + 2, col + 2];

                    int currentSum = a00 + a01 + a02 + a10 + a11 + a12 + a20 + a21 + a22;
                    if (currentSum > maximumSum)
                    {
                        maximumSum = currentSum;
                        maxSumMatrix = new int[3, 3]
                        {
                            {a00, a01, a02 },
                            {a10, a11, a12 },
                            {a20, a21, a22 }
                        };
                    }
                }
            }
            var result = new System.Text.StringBuilder();

            result.Append("Sum = " + ((rows == 0 && cols == 0) ? 0 : maximumSum) + Environment.NewLine);
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    result.Append(maxSumMatrix[row, col] + " ");
                }
                result.Append(Environment.NewLine);
            }

            Console.WriteLine(result);
        }
    }
}
