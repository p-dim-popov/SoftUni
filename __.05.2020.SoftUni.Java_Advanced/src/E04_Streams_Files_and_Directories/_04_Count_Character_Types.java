package E04_Streams_Files_and_Directories;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.*;
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
//            var result = reader.lines()
//                    .collect(ArrayList::new, (acc, cur) -> {
//                        acc.
//                    });
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
