using System;
using System.Threading.Tasks;

namespace BattleCards
{
    using SUS.MvcFramework;

    public static class Program
    {
        public static async Task Main()
        {
            await Host.CreateHostAsync(new Startup(), 1234);
        }
    }
}
