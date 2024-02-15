using vega.Models;

namespace vega.Pages.Persistence.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id);
    }
}