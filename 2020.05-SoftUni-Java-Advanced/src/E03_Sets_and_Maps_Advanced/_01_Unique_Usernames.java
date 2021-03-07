package E03_Sets_and_Maps_Advanced;

import java.util.LinkedHashSet;
import java.util.Scanner;

public class _01_Unique_Usernames {
    public static void main(String[] args) {
        final var scanner = new Scanner(System.in);
        final int n = Integer.parseInt(scanner.nextLine());

        final var set = new LinkedHashSet<String>();

        for (int i = 0; i < n; i++)
            set.add(scanner.nextLine());

        set.forEach(System.out::println);
    }
}
