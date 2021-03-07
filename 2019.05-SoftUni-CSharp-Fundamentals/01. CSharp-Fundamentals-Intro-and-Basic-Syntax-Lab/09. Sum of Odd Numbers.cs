using System;
					
public class Program
{
	public static void Main()
	{
		int n = int.Parse(Console.ReadLine());
		int sum = 0;
		int a = n;
		
      for (int i = 1; i <= n; i++)
      {
			Console.WriteLine(2*i-1);
		  sum += 2*i-1;
		  
      }
		
		Console.WriteLine($"Sum: {sum}");
    }
}