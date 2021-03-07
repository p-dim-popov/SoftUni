using System;
class Program
{
    static void Main()
    {
        char[] a = new char[3];
        a[0] = char.Parse(Console.ReadLine());
        a[1] = char.Parse(Console.ReadLine());
        a[2] = char.Parse(Console.ReadLine());
        string strinf = $"{a[0]}" + $"{a[1]}" + $"{a[2]}";
        Console.WriteLine(strinf);
    }
}