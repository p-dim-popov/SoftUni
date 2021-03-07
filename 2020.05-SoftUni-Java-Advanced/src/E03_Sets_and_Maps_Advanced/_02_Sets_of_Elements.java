package E03_Sets_and_Maps_Advanced;

import java.util.Arrays;
import java.util.LinkedHashSet;
import java.util.Scanner;

public class _02_Sets_of_Elements {
    public static void main(String[] args) {
        final var scanner = new Scanner(System.in);
        final var setsLength = Arrays.stream(scanner
                .nextLine()
                .split("\\s+"))
                .mapToInt(Integer::parseInt)
                .toArray();
        final var firstSet = new LinkedHashSet<Integer>();
        final var secondSet = new LinkedHashSet<Integer>();

        for (int i = 0; i < setsLength[0]; i++)
            firstSet.add(Integer.parseInt(scanner.nextLine()));

        for (int i = 0; i < setsLength[1]; i++)
            secondSet.add(Integer.parseInt(scanner.nextLine()));

        final var intersect = new LinkedHashSet<Integer>(firstSet);
        intersect.retainAll(secondSet);

        intersect.forEach(el -> System.out.print(el + " "));
    }
}
