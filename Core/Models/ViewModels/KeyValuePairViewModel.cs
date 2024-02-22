namespace vega.ViewModels
{
    public class KeyValuePairViewModel
    {
        public int Id { get; set; } //Id = VehicleFeature.FeatureId
        public string Name { get; set; } // Name = VehicleFeature.Feature.Name
    }
}



// Make sure that the mapping configuration allows for values to be automatically grabbed
// by the Id and Name properties! (i.e. .ForMember(dvvm => dvvm.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairViewModel { Id = vf.FeatureId, Name = vf.Feature.Name })))