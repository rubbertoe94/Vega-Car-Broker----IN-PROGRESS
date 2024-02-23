using Microsoft.EntityFrameworkCore;
using vega.Core.Interfaces;
using vega.Models;
using vega.Pages.Persistence.Interfaces;
using vega.Persistence;

namespace vega.Pages.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;

        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated) 
            {
                return await context.Vehicles.FindAsync(id);
            }
            return await context.Vehicles
                 .Include(v => v.Features)
                     .ThenInclude(vf => vf.Feature)
                 .Include(v => v.Model)
                     .ThenInclude(m => m.Make)
                 .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehicles() 
        {
            return await context.Vehicles
                .Include(v => v.Features)
                     .ThenInclude(vf => vf.Feature)
                 .Include(v => v.Model)
                     .ThenInclude(m => m.Make)
                     .OrderBy(v => v.Id)
                     .ToListAsync();
        }

        public async Task<IEnumerable<Feature>> GetAllFeatures()
        {
            return await context.Features
                .OrderBy(f => f.Id)
                .ToListAsync();
        }

        public void Add(Vehicle vehicle) 
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle) 
        {
            context.Vehicles.Remove(vehicle);
        }

    }
}
