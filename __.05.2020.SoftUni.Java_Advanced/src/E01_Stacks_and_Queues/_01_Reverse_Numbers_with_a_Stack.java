package E01_Stacks_and_Queues;

import java.util.ArrayDeque;
import java.util.Arrays;
import java.util.Scanner;

public class _01_Reverse_Numbers_with_a_Stack {
    public static void main(String[] args) {
        var scanner = new Scanner(System.in);
        var numbers = Arrays.stream(scanner
                .nextLine()
                .split("\\s+"))
                .mapToInt(Integer::parseInt)
                .toArray();

        var stack = new ArrayDeque<Integer>();

        for (var number : numbers)
            stack.push(number);

        while (!stack.isEmpty())
            System.out.print(stack.pop() + " ");
    }
}
