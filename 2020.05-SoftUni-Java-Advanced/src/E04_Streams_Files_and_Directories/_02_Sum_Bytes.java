package E04_Streams_Files_and_Directories;

import java.io.IOException;
import java.math.BigInteger;
import java.nio.file.Files;
import java.nio.file.Path;

public class _02_Sum_Bytes {
    public static void main(String[] args) {
        var path = Path.of(
                "src",
                _01_Sum_Lines.class.getPackageName(),
                "resources",
                "input.txt");

        try (var reader = Files.newBufferedReader(path)) {
            System.out.println(reader.lines()
                    .flatMapToInt(String::chars)
                    .sum());
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
