using System.Threading.Tasks;
using SUS.MvcFramework;

namespace SULS
{

    public class Program
    {
        public static async Task Main()
        {
            await Host.CreateHostAsync(new StartUp(), 1234);
        }
    }
}