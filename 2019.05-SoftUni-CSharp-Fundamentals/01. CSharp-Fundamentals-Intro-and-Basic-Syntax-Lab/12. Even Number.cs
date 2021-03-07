using System;
					
public class Program
{
	public static void Main()
	{
		int n = int.Parse(Console.ReadLine());
		
		for (;;)
	  {
		  if (n%2 != 0) 
		  {
			  Console.WriteLine("Please write an even number."); 
			  n = int.Parse(Console.ReadLine());
		  }
			else 
			{
				if (n < 0) n *=(-1);
				Console.WriteLine("The number is: {0}",n);
				break;
			}
		}
    }
}