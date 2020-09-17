package E03_Sets_and_Maps_Advanced;

import java.util.HashMap;
import java.util.Scanner;

public class _05_Phonebook {
    public static void main(String[] args) {
        final var scanner = new Scanner(System.in);

        final var map = new HashMap<String, String>();

        String input;
        while (!"search".equals(input = scanner.nextLine())) {
            final var tokens = input.split("-");
            map.put(tokens[0], tokens[1]);
        }

        while (!"stop".equals(input = scanner.nextLine())) {
            if (map.containsKey(input))
                System.out.println(input + " -> " + map.get(input));
            else System.out.println("Contact " + input + " does not exist.");
        }
    }
}
