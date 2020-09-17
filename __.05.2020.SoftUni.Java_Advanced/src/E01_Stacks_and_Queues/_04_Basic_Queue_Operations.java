package E01_Stacks_and_Queues;

import java.util.ArrayDeque;
import java.util.Arrays;
import java.util.Scanner;

public class _04_Basic_Queue_Operations {
    public static void main(String[] args) {
        final var scanner = new Scanner(System.in);
        final var queue = new ArrayDeque<Integer>();
        var input = Arrays.stream(scanner
                .nextLine()
                .split("\\s+"))
                .mapToInt(Integer::parseInt)
                .toArray();

        final var n = input[0];
        final var s = input[1];
        final var x = input[2];

        input = Arrays.stream(scanner
                .nextLine()
                .split("\\s+"))
                .mapToInt(Integer::parseInt)
                .toArray();

        for (int i = 0; i < n; i++)
            queue.offer(input[i]);


        for (int i = 0; i < s; i++)
            queue.poll();


        if (queue.contains(x))
            System.out.println(true);
        else if (!queue.isEmpty())
            System.out.println(queue
                    .stream()
                    .min(Integer::compareTo)
                    .get());
        else
            System.out.println(0);
    }
}
