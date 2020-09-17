package E03_Sets_and_Maps_Advanced;

import java.util.*;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class _08_Hands_of_Cards {
    public static void main(String[] args) {
        final Map<String, Integer> modifiers = Map.of(
                "S", 4,
                "H", 3,
                "D", 2,
                "C", 1
        );

        final var values = Stream.of(new Object[][]{
                        {"1", 1},
                        {"2", 2},
                        {"3", 3},
                        {"4", 4},
                        {"5", 5},
                        {"6", 6},
                        {"7", 7},
                        {"8", 8},
                        {"9", 9},
                        {"10", 10},
                        {"J", 11},
                        {"Q", 12},
                        {"K", 13},
                        {"A", 14}
                }
        )
                .collect(Collectors
                        .toMap(data -> (String) data[0], data -> (Integer) data[1]));

        final var people = new LinkedHashMap<String, Map<String, Integer>>();

        final var scanner = new Scanner(System.in);
        String input;
        while (!"JOKER".equals(input = scanner.nextLine())) {
            final var splittedInput = input.split(":\\s+");
            final var player = splittedInput[0];
            final var cards = splittedInput[1].split(",\\s+");

            var playerDeck = people.putIfAbsent(player, new HashMap<>());
            if (playerDeck == null)
                playerDeck = people.get(player);

            for (var c : cards) {
                final var cardSplit = c.toCharArray();
                if (cardSplit.length != 3) {
                    final var cardValue = Character.toString(cardSplit[0]);
                    final var cardModifier = Character.toString(cardSplit[1]);

                    playerDeck.put(c, values.get(cardValue) * modifiers.get(cardModifier));
                } else playerDeck.put(c, 10 * modifiers.get(Character.toString(cardSplit[2])));
            }
        }

        people.forEach(
                (k, v) ->
                        System.out.println(
                                k + ": " + v.values()
                                        .stream()
                                        .mapToInt(el -> el)
                                        .sum()));
    }
}
