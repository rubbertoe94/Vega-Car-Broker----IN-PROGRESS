using vega.Core.Models;
using vega.Models;

namespace vega.Core.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        Task<IEnumerable<Vehicle>> GetAllVehicles(Filter filter);
        Task<IEnumerable<Feature>> GetAllFeatures();
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);

    }
}