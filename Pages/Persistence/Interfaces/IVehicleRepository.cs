using vega.Models;

namespace vega.Pages.Persistence.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        Task<IEnumerable<Vehicle>> GetAllVehicles();
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);

    }
}