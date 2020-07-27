namespace P01_StudentSystem
{
    using Data;

    class Program
    {
        static void Main(string[] args)
        {
            var context = new StudentSystemContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.SaveChanges();
        }
    }
}
