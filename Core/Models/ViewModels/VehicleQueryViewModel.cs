namespace vega.Core.Models.ViewModels
{
    public class VehicleQueryViewModel
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public string SortBy { get; set; }
        public bool isSortAscending { get; set; }
    }
}
