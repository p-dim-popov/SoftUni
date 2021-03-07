using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BattleCards
{
    using Data;
    using Services;

    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUserService, UserService>();
            serviceCollection.Add<ICardService, CardService>();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}
