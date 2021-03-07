using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            var matrixDimensions = Console.ReadLine().Split().Select(byte.Parse).ToArray();
            byte rows = matrixDimensions[0];
            byte cols = matrixDimensions[1];
            var matrix = new char[rows, cols];
            var snake = new Queue<char>(Console.ReadLine());
            for (int row = 0; row < rows; row++)
            {
                if (row % 2 == 0)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        matrix[row, col] = snake.Peek();
                        snake.Enqueue(snake.Dequeue());
                    }
                }
                else
                {
                    for (int col = cols - 1; col >= 0; col--)
                    {
                        matrix[row, col] = snake.Peek();
                        snake.Enqueue(snake.Dequeue());
                    }
                }
            }
            var result = new System.Text.StringBuilder();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    result.Append(matrix[row, col]);
                }
                result.Append(Environment.NewLine);
            }
            Console.Write(result);
        }
    }
}