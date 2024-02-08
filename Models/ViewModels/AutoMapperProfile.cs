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
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vvm => vvm.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vvm => vvm.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vvm => vvm.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.MapFrom(vvm => vvm.Features.Select(id => new VehicleFeatureViewModel { FeatureId = id })));

            CreateMap<VehicleFeature, VehicleFeatureViewModel>()
                .ReverseMap();
        }
    }
}
