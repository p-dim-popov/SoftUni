package E04_Streams_Files_and_Directories;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.stream.Collectors;

public class _03_ALL_CAPITALS {
    public static void main(String[] args) {
        var path = Path.of(
                "src",
                _01_Sum_Lines.class.getPackageName(),
                "resources",
                "input.txt");

        try (var reader = Files.newBufferedReader(path)) {
            System.out.println(reader.lines()
                    .map(String::toUpperCase)
                    .collect(Collectors.joining("\n")));
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
