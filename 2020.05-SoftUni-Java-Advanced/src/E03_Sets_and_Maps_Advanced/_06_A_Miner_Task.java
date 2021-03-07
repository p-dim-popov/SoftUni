package E03_Sets_and_Maps_Advanced;

import java.util.LinkedHashMap;
import java.util.Scanner;

public class _06_A_Miner_Task {
    public static void main(String[] args) {
        final var scanner = new Scanner(System.in);
        final var map = new LinkedHashMap<String, Integer>();

        String input;
        while (!"stop".equals(input = scanner.nextLine())){
            final var quantity = Integer.parseInt(scanner.nextLine());
            var el = map.putIfAbsent(input, quantity);
            if (el != null)
                map.replace(input, el + quantity);
        }

        map.forEach((k, v) -> System.out.println(k + " -> " + v));
    }
}
