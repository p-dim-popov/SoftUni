using System;
					
public class Program
{
	public static void Main()
	{
		string n = Console.ReadLine();
		int age = int.Parse(Console.ReadLine());
		int a = 0;
		bool error = false;
		
		switch (n)
		{
			case "Weekday":
				if (age >= 0 && age <= 18)
				{
					a = 12;
				}
				else if (age > 18 && age <= 64)
				{
					a = 18;
				}
				else if (age > 64 && age <= 122)
				{
					a = 12;
				}
				else
				{
					error = true;
				}
				break;
			case "Weekend":
				if (age >= 0 && age <= 18)
				{
					a = 15;
				}
				else if (age > 18 && age <= 64)
				{
					a = 20;
				}
				else if (age > 64 && age <= 122)
				{
					a = 15;
				}
				else
				{
					error = true;
				}
				break;
			case "Holiday":
				if (age >= 0 && age <= 18)
				{
					a = 5;
				}
				else if (age > 18 && age <= 64)
				{
					a = 12;
				}
				else if (age > 64 && age <= 122)
				{
					a = 10;
				}
				else
				{
					error = true;
				}
				break;
		}
		
		if (error)
		{
			Console.WriteLine("Error!");
		}
		else 
		{
			Console.WriteLine($"{a}$");
		}
	}
}