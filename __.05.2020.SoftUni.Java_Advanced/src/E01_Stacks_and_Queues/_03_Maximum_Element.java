package E01_Stacks_and_Queues;

import java.util.ArrayDeque;
import java.util.Scanner;

public class _03_Maximum_Element {
    public static void main(String[] args) {
        final var scanner = new Scanner(System.in);
        final var n = Integer.parseInt(scanner.nextLine());
        final var stack = new ArrayDeque<Integer>();

        for (int i = 0; i < n; i++) {
            final var input = scanner.nextLine().split("\\s+");

            switch (Integer.parseInt(input[0])) {
                case 1:
                    stack.push(Integer.parseInt(input[1]));
                    break;
                case 2:
                    stack.pop();
                    break;
                case 3:
                    if (!stack.isEmpty())
                        System.out.println(stack
                                .stream()
                                .max(Integer::compareTo)
                                .get());
                    break;
                default:
                    break;
            }

        }
    }
}
