using System;

namespace KnightGame
{
    public class KnightGame
    {
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            var matrix = new char[n][];
            for (int row = 0; row < matrix.Length; row++)
            {
                matrix[row] = Console.ReadLine().ToCharArray();
            }

            int mostDangerousKnightRow = -1;
            int mostDangerousKnightCol = -1;
            int removedKnights = 0;

            while (true)
            {
                int maxKnightsInDanger = 0;

                for (int rowIndex = 0; rowIndex < matrix.Length; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < matrix[rowIndex].Length; colIndex++)
                    {
                        int currentKnightsInDanger = 0;
                        if (matrix[rowIndex][colIndex].Equals('K'))
                        {
                            currentKnightsInDanger = CheckDanger(matrix, rowIndex, colIndex);                            
                        }

                        if (currentKnightsInDanger > maxKnightsInDanger)
                        {
                            maxKnightsInDanger = currentKnightsInDanger;
                            mostDangerousKnightRow = rowIndex;
                            mostDangerousKnightCol = colIndex;
                        }
                        currentKnightsInDanger = 0;
                    }
                }
                if (maxKnightsInDanger > 0)
                {
                    matrix[mostDangerousKnightRow][mostDangerousKnightCol] = 'O';
                    removedKnights++;
                }
                else
                {
                    Console.WriteLine(removedKnights);
                    return;
                }
            }
        }

        private static int CheckDanger(char[][] matrix, int rowIndex, int colIndex)
        {
            int currentKnightsInDanger = 0;

            // vertical and left
            if (IsCellInMatrix(rowIndex - 2, colIndex - 1, matrix))
            {
                if (matrix[rowIndex - 2][colIndex - 1].Equals('K'))
                {
                    currentKnightsInDanger++;
                }
            }

            // vertical and right
            if (IsCellInMatrix(rowIndex - 2, colIndex + 1, matrix))
            {
                if (matrix[rowIndex - 2][colIndex + 1].Equals('K'))
                {
                    currentKnightsInDanger++;
                }
            }

            // vertical and left
            if (IsCellInMatrix(rowIndex + 2, colIndex - 1, matrix))
            {
                if (matrix[rowIndex + 2][colIndex - 1].Equals('K'))
                {
                    currentKnightsInDanger++;
                }
            }

            // vertical and right
            if (IsCellInMatrix(rowIndex + 2, colIndex + 1, matrix))
            {
                if (matrix[rowIndex + 2][colIndex + 1].Equals('K'))
                {
                    currentKnightsInDanger++;
                }
            }

            // horizontal up and left
            if (IsCellInMatrix(rowIndex - 1, colIndex - 2, matrix))
            {
                if (matrix[rowIndex - 1][colIndex - 2].Equals('K'))
                {
                    currentKnightsInDanger++;
                }
            }

            // horizontal up and right
            if (IsCellInMatrix(rowIndex - 1, colIndex + 2, matrix))
            {
                if (matrix[rowIndex - 1][colIndex + 2].Equals('K'))
                {
                    currentKnightsInDanger++;
                }
            }

            // horizontal down and left
            if (IsCellInMatrix(rowIndex + 1, colIndex - 2, matrix))
            {
                if (matrix[rowIndex + 1][colIndex - 2].Equals('K'))
                {
                    currentKnightsInDanger++;
                }
            }

            // horizontal down and right
            if (IsCellInMatrix(rowIndex + 1, colIndex + 2, matrix))
            {
                if (matrix[rowIndex + 1][colIndex + 2].Equals('K'))
                {
                    currentKnightsInDanger++;
                }
            }
            return currentKnightsInDanger;
        }

        public static bool IsCellInMatrix(int row, int col, char[][] matrix)
        {
            return (0 <= row && row < matrix.Length && 0 <= col && col < matrix[row].Length);
        }
    }
}
