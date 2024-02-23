namespace vega.Core.Interfaces
{

    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
