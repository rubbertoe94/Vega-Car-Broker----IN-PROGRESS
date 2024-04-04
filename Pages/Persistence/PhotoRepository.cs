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
    public class PhotoRepository : IPhotoRepository
    {
        private readonly VegaDbContext context;

        public PhotoRepository(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
           return await context.Photos
                .Where(p => p.VehicleId == vehicleId)
                .ToListAsync();
        }


    }
}