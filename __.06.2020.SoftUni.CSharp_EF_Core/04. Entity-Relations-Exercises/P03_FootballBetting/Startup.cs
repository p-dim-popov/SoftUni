namespace P03_FootballBetting
{
    using Data;

    static class Startup
    {
        static void Main(string[] args)
        {
            var context = new FootballBettingContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
