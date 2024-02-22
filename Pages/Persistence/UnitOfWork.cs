using vega.Persistence;

namespace vega.Pages.Persistence.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext context;

        public UnitOfWork(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
           await  context.SaveChangesAsync();
        }
    }
}
