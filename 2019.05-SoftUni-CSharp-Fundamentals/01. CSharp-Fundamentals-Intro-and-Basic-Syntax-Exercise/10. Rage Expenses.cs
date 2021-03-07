using System;
class Program
{
    static void Main()
    {
        int losts = int.Parse(Console.ReadLine());
        float headsetP = float.Parse(Console.ReadLine());
        float mouseP = float.Parse(Console.ReadLine());
        float keyboardP = float.Parse(Console.ReadLine());
        float displayP = float.Parse(Console.ReadLine());

        short trashedKeyboard = (short)(losts/6);
        short trashedMouse = (short)(losts/3);
        short trashedHeadset = (short)(losts/2);
        short trashedDisplay = (short)(trashedKeyboard/2);

        /*for (int i = 1; i <= losts; i++)
        {
            if (i % 2 == 0) trashedHeadset++;
            if (i % 3 == 0) trashedMouse++;
            if (i % 6 == 0) trashedKeyboard++;
            if (trashedKeyboard % 2 == 0 && trashedKeyboard > 0) trashedDisplay++;
        }*/

        float sum = trashedDisplay * displayP + trashedHeadset * headsetP + trashedKeyboard * keyboardP + trashedMouse * mouseP;
        Console.WriteLine($"Rage expenses: {sum:f2} lv.");
    }
}