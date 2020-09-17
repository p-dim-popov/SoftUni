package E03_Sets_and_Maps_Advanced;

import java.util.*;

public class _07_Fix_Emails {
    public static void main(String[] args) {
        final var scanner = new Scanner(System.in);
        final var map = new LinkedHashMap<String, String>();

        String input;
        while (!"stop".equals(input = scanner.nextLine())){
            final var email = scanner.nextLine();
            var el = map.putIfAbsent(input, email);
            if (el != null)
                map.replace(input, email);
        }

        map.entrySet()
                .stream()
                .filter(x -> !Arrays.asList("us", "uk", "com")
                        .contains(x.getValue()
                                .substring(x.getValue().lastIndexOf(".") + 1)
                                .toLowerCase()))
                .forEach(x -> System.out.println(x.getKey() + " -> " + x.getValue()));

    }
}
