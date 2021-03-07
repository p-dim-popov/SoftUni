using System;
					
public class Program
{
	public static void Main()
	{
		int h = int.Parse(Console.ReadLine());
		int m = int.Parse(Console.ReadLine());
		
		m += 30;
		if (m >= 60) 
		{
			m -= 60;
			h += 1;
		}
		if (h >= 24) h -= 24;
		
		if (m < 10) 
		{
			Console.WriteLine($"{h}:0{m}");
		}
		else 
		{
			Console.WriteLine($"{h}:{m}");
		}
	}
}