using AutoMapper;
using vega.ViewModels;

namespace vega.Models.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Make, MakeViewModel>()
                .ReverseMap();
            CreateMap<MakeViewModel, ModelViewModel>()
                .ReverseMap();

            CreateMap<Model, ModelViewModel>()
                .ReverseMap();
            CreateMap<ModelViewModel, MakeViewModel>()
                .ReverseMap();

            CreateMap<Vehicle, VehicleViewModel>()
                .ForMember(vvm => vvm.Contact, opt => opt.MapFrom(v => new ContactViewModel { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vvm => vvm.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            CreateMap<VehicleViewModel, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vvm => vvm.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vvm => vvm.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vvm => vvm.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vvm, v) => {
                    //remove unselected features
                    var removedFeatures = new List<VehicleFeature>();
                    foreach(var f in v.Features) {
                        if (!vvm.Features.Contains(f.FeatureId)) {
                            removedFeatures.Add(f);
                        }
                    }
                    foreach(var f in removedFeatures) {
                        v.Features.Remove(f);
                    }

                    //add new features
                    foreach(var id in vvm.Features) {
                        if (!v.Features.Any(f => f.FeatureId == id)) {
                            v.Features.Add(new VehicleFeature {FeatureId = id});
                        }
                    }
                });

            CreateMap<VehicleFeature, VehicleFeatureViewModel>()
                .ReverseMap();
        }
    }
}
