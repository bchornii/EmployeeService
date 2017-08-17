namespace DataAccess.Infrastructure
{
    public interface INorthwindDbContextFactory
    {
        INorthwindContext CreateContext();
    }

    public class NorthwindDbContextFactory : INorthwindDbContextFactory
    {
        public INorthwindContext CreateContext()
        {
            return new NorthwindContext();
        }
    }
}
