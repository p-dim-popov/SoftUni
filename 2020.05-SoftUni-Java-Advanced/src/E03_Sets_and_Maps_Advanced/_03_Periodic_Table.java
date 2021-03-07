package E03_Sets_and_Maps_Advanced;

import java.util.Arrays;
import java.util.Scanner;
import java.util.TreeSet;

public class _03_Periodic_Table {
    public static void main(String[] args) {
        final var scanner = new Scanner(System.in);
        final var n = Integer.parseInt(scanner.nextLine());

        final var set = new TreeSet<String>();

        for (int i = 0; i < n; i++)
            set.addAll(Arrays.asList(scanner
                    .nextLine()
                    .split("\\s+")));

        set.forEach(el -> System.out.print(el + " "));
    }
}
