namespace Kaizen.Domain.Data
{
    public interface IContextFactory
    {
        ApplicationDbContext Create();
    }
}
