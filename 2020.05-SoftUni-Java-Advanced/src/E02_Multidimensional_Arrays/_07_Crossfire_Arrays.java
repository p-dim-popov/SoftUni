package E02_Multidimensional_Arrays;

import java.util.Arrays;
import java.util.Scanner;
import java.util.stream.Collectors;

public class _07_Crossfire_Arrays {

    public static void main(String[] args) {
        final var scanner = new Scanner(System.in);
        final var dimensions = Arrays.stream(scanner.nextLine()
                .split("\\s+"))
                .mapToInt(Integer::parseInt)
                .toArray();

        final var rows = dimensions[0];
        final var cols = dimensions[1];

        var matrix = new int[rows][cols];
        int n = 1;
        for (int i = 0; i < matrix.length; i++) {
            for (int j = 0; j < matrix[i].length; j++)
                matrix[i][j] = n++;
        }

        String input;
        while (!"Nuke it from orbit".equals(input = scanner.nextLine())) {
            final var numbers = Arrays.stream(input
                    .split("\\s+"))
                    .mapToInt(Integer::parseInt)
                    .toArray();
            final var row = numbers[0];
            final var col = numbers[1];
            final var radius = numbers[2];

            if (isValid(row, col, matrix))
                matrix[row][col] = 0;

            for (int i = 1; i <= radius; i++) {
                if (isValid(row + i, col, matrix))
                    matrix[row + i][col] = 0;

                if (isValid(row - i, col, matrix))
                    matrix[row - i][col] = 0;

                if (isValid(row, col + i, matrix))
                    matrix[row][col + i] = 0;

                if (isValid(row, col - i, matrix))
                    matrix[row][col - i] = 0;
            }

            for (int i = matrix.length - 1; i > -1; i--) {
                matrix[i] = Arrays.stream(matrix[i])
                        .filter(el -> el != 0)
                        .toArray();

                if (matrix[i].length == 0)
                    matrix = Arrays.stream(matrix)
                            .filter(arr -> arr.length != 0)
                            .toArray(int[][]::new);
            }
        }

        System.out.println(matrixToString(matrix));
    }

    private static String matrixToString(int[][] matrix) {
        return Arrays.stream(matrix)
                .map(line -> Arrays.stream(line)
                        .mapToObj(String::valueOf)
                        .collect(Collectors.joining(" ")))
                .collect(Collectors.joining("\n"));
    }

    private static boolean isValid(int row, int col, int[][] matrix) {
        return (row >= 0 && row < matrix.length) &&
                (col >= 0 && col < matrix[row].length);
    }

}
