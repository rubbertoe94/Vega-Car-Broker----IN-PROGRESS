namespace vega.Models.ViewModels
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }

       
        public DateTime LastUpdated { get; set; }

        public ICollection<VehicleFeature> Features { get; set; }
    }
}
