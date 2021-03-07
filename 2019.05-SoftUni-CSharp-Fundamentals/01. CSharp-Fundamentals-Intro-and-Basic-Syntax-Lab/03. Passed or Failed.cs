using System;
					
public class Program
{
	public static void Main()
	{
		double grade = double.Parse(Console.ReadLine());
		
		if (grade >= 3.0) Console.WriteLine("Passed!");
		else Console.WriteLine("Failed!");
		
	}
}