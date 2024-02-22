namespace vega.Pages.Persistence.Interfaces
{

    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
