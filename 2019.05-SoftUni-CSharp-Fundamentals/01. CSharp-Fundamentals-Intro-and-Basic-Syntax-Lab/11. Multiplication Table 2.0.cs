using System;
					
public class Program
{
	public static void Main()
	{
		int m = int.Parse(Console.ReadLine());
		int n = int.Parse(Console.ReadLine());
		
      for (; n  < 10; n++)
      {
		Console.WriteLine($"{m} X {n} = {n*m}");	
      }
		if (n >= 10) Console.WriteLine($"{m} X {n} = {n*m}");
    }
}