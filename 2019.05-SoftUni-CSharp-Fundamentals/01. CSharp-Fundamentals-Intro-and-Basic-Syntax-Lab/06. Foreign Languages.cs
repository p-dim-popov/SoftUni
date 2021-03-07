using System;
					
public class Program
{
	public static void Main()
	{
		string n = Console.ReadLine();
		
		switch (n)
		{
			case "USA":
			case "England":
				Console.WriteLine("English");
				break;
			case "Spain":
			case "Argentina":
			case "Mexico":
				Console.WriteLine("Spanish");
				break;
			default:
				Console.WriteLine("unknown");
				break;
		}
	}
}