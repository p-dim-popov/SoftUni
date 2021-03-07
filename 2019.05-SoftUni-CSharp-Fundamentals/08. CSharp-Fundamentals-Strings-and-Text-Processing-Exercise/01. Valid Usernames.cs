using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main()
        {
            var usernames = Console.ReadLine()
                .Split(", ");
            
            foreach(var user in usernames)
            {
                if(user.Length >= 3 && user.Length <= 16)
                {
                    bool isValid = true;
                    foreach (char ch in user)
                    {
                        if(!Char.IsLetterOrDigit(ch) && ch != '-' && ch != '_')
                        {
                            isValid = false;
                            break;
                        }
                    }
                    if(isValid)
                    {
                        Console.WriteLine(user);
                    }
                }
            }
        }
    }
}