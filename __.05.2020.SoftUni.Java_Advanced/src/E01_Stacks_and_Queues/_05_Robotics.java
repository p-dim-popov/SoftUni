package E01_Stacks_and_Queues;

import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.*;
import java.util.stream.Collectors;

public class _05_Robotics {
    public static void main(String[] args) {
        var scanner = new Scanner(System.in);
        var robots = new HashMap<String, int[]>();

        Arrays.stream(scanner.nextLine()
                .split(";"))
                .map(r -> r.split("-"))
                .forEach(r -> robots.put(
                        r[0],
                        new int[]{
                                Integer.parseInt(r[1]),
                                0
                        })); // name: { processingTime, currentProcess }

        var startingTime = LocalTime.parse(
                scanner.nextLine(),
                DateTimeFormatter.ofPattern("hh:mm:ss")
        );

        String product;
        while (!"End".equals(product = scanner.nextLine())) {
            startingTime = startingTime.plusSeconds(1);

            System.out.println("" + " - " + product + startingTime.format(DateTimeFormatter.ofPattern(" [hh:mm:ss]")));
            //TODO: continue...
        }
    }
}
