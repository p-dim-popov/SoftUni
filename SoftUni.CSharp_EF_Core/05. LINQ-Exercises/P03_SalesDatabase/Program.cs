using System;
using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data;

namespace P03_SalesDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new SalesContext();
            context.Database.Migrate();
            
            Console.WriteLine("Hello World!");
        }
    }
}
