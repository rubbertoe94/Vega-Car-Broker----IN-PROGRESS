using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using vega.Core.Models;

namespace vega.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public bool IsRegistered { get; set; }

        [Required]
        [StringLength(100)]
        public string ContactName { get; set; }
        [StringLength(100)]
        public string ContactEmail { get; set; }
        [Required]
        [StringLength(100)]
        public string ContactPhone { get; set; }
        public DateTime LastUpdated { get; set; }
        public ICollection<VehicleFeature> Features { get; set; }
        public ICollection<Photo> Photos { get; set; }


        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
            Photos = new Collection<Photo>();
        }
    }
}
