package E01_Stacks_and_Queues;

import java.util.ArrayDeque;
import java.util.Arrays;
import java.util.Scanner;

public class _02_Basic_Stack_Operations {
    public static void main(String[] args) {
        final var scanner = new Scanner(System.in);
        final var stack = new ArrayDeque<Integer>();
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

        for (int i = 0; i < n; i++) {
            stack.push(input[i]);
        }

        for (int i = 0; i < s; i++) {
            stack.pop();
        }

        if (stack.contains(x))
            System.out.println(true);
        else if (!stack.isEmpty())
            System.out.println(stack
                    .stream()
                    .min(Integer::compareTo)
                    .get());
        else
            System.out.println(0);
    }
}
