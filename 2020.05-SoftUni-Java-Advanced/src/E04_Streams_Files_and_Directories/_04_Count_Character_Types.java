package E04_Streams_Files_and_Directories;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.*;
import java.util.Map.Entry;
import java.util.function.Function;
import java.util.function.Supplier;
import java.util.stream.Collectors;

public class _04_Count_Character_Types {
    public static void main(String[] args) {
        var path = Path.of(
                "src",
                _01_Sum_Lines.class.getPackageName(),
                "resources",
                "input.txt");

        final var vowels = Set.of("a", "e", "i", "o", "u");
        final var punctuationMarks = Set.of("!", ",", ".", "?");

        try (var reader = Files.newBufferedReader(path)) {
            reader.lines()
                    .collect((Supplier<HashMap<String, Integer>>) HashMap::new, (acc, cur) -> Arrays
                            .stream(cur
                                    .replaceAll("\\s", "")
                                    .split(""))
                            .forEach(el -> {
                                if (vowels.contains(el))
                                    acc.put("Vowels: ", acc.getOrDefault("Vowels: ", 0) + 1);
                                else if (punctuationMarks.contains(el))
                                    acc.put("Punctuation: ", acc.getOrDefault("Punctuation: ", 0) + 1);
                                else acc.put("Consonants: ", acc.getOrDefault("Consonants: ", 0) + 1);
                            }), (hashMap, hashMap2) -> hashMap.forEach(hashMap2::put))
                    .entrySet()
                    .stream()
                    .sorted((a, b) -> b.getKey().compareTo(a.getKey()))
                    .forEach((kvp) -> System.out.println(kvp.getKey() + kvp.getValue()));
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
