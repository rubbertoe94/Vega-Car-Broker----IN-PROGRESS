using vega.Core.Models;
using vega.Models;

namespace vega.Core.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}