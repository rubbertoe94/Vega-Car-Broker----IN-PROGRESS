using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using vega.Core.Interfaces;
using vega.Core.Models;
using vega.Extensions;
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



        public async Task<(IEnumerable<Vehicle>, int totalCount)> GetAllVehicles(VehicleQuery queryObj) 
        {
            var query = context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .OrderBy(v => v.Id)
                .AsQueryable();

            if (queryObj.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);
            if (queryObj.ModelId.HasValue)
                query = query.Where(v => v.ModelId == queryObj.ModelId.Value);

            int totalCount = await query.CountAsync();
            
            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
                ["id"] = v => v.Id
            };  

            query = query.ApplyOrdering(queryObj, columnsMap);

            query = query.ApplyPaging(queryObj);


            var vehicles = await query.ToListAsync();
            return (vehicles, totalCount);
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

        public void Update(Vehicle vehicle)
        {
            context.Attach(vehicle);
            context.Entry(vehicle).State = EntityState.Modified;
        }

    }
}
