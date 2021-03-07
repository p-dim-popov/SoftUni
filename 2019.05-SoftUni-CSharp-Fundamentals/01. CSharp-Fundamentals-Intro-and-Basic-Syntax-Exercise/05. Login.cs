using System;
class Program
{
    static void Main()
    {
        string username = Console.ReadLine();
        string password = "";
        
        for (int i = username.Length-1; i >= 0; i--)
        {
            password += username[i];
        }
        for (int j = 0; j < 3; j++)
        {
            string inputPassword = Console.ReadLine();
            if (password == inputPassword)
            {
                Console.WriteLine($"User {username} logged in.");
                return;
            }
            else Console.WriteLine("Incorrect password. Try again.");
        }

        string inputLastTry = Console.ReadLine();
        if (password == inputLastTry)
        {
            Console.WriteLine($"User {username} logged in.");
            return;
        }
        else Console.WriteLine($"User {username} blocked!");

    }
}