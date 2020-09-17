package E02_Multidimensional_Arrays;

import java.util.*;
import java.util.stream.Collectors;

public class _07_Crossfire_Lists {
    public static void main(String[] args) {
        final var scanner = new Scanner(System.in);
        final var dimensions = Arrays.stream(scanner.nextLine()
                .split("\\s+"))
                .mapToInt(Integer::parseInt)
                .toArray();

        final var rows = dimensions[0];
        final var cols = dimensions[1];

        int n = 1;
        var matrix = new ArrayList<List<Integer>>(rows);
        for (int row = 0; row < rows; row++) {
            matrix.add(new ArrayList<>(cols));
            for (int col = 0; col < cols; col++)
                matrix.get(row).add(n++);
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
                matrix.get(row).set(col, 0);

            for (int i = 1; i <= radius; i++) {
                if (isValid(row + i, col, matrix))
                    matrix.get(row + i).set(col, 0);

                if (isValid(row - i, col, matrix))
                    matrix.get(row - i).set(col, 0);

                if (isValid(row, col + i, matrix))
                    matrix.get(row).set(col + i, 0);

                if (isValid(row, col - i, matrix))
                    matrix.get(row).set(col - i, 0);
            }

            for (int i = matrix.size() - 1; i > -1; i--) {
                matrix.get(i).removeIf(el -> el.equals(0));

                if (matrix.get(i).size() == 0)
                    matrix.remove(i);
            }
        }

        System.out.println(matrixToString(matrix));
    }

    private static String matrixToString(List<List<Integer>> matrix) {
        return matrix
                .stream()
                .map(line -> line
                        .stream()
                        .map(String::valueOf)
                        .collect(Collectors.joining(" ")))
                .collect(Collectors.joining("\n"));
    }

    private static boolean isValid(int row, int col, List<List<Integer>> matrix) {
        return (row >= 0 && row < matrix.size()) &&
                (col >= 0 && col < matrix.get(row).size());
    }
}
