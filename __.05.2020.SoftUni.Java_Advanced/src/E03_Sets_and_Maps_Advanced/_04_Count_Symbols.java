package E03_Sets_and_Maps_Advanced;

import java.util.Scanner;
import java.util.TreeMap;

public class _04_Count_Symbols {
    public static void main(String[] args) {
        final var scanner = new Scanner(System.in);
        final var input = scanner
                .nextLine()
                .toCharArray();

        final var map = new TreeMap<Character, Integer>();
        for (var ch :
                input) {
            var val = map.putIfAbsent(ch, 1);
            if (val != null)
                map.replace(ch, ++val);
        }

        map.forEach((k, v)
                -> System.out.println(k + ": " + v + " time/s"));
    }
}
