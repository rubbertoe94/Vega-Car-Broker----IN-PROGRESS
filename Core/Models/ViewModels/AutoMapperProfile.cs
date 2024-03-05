using AutoMapper;
using vega.Core.Models;
using vega.Core.Models.ViewModels;
using vega.ViewModels;

namespace vega.Models.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            //domain to api resource

            CreateMap<Make, MakeViewModel>()
                .ReverseMap();
            CreateMap<Make, KeyValuePairViewModel>()
                .ReverseMap();

            CreateMap<Model, KeyValuePairViewModel>()
                .ReverseMap();

            CreateMap<Vehicle, VehicleViewModel>()
                .ForMember(vvm => vvm.Contact, opt => opt.MapFrom(v => new ContactViewModel { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vvm => vvm.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairViewModel { Id = vf.FeatureId, Name = vf.Feature.Name })))
                .ForMember(vvm => vvm.Make, opt => opt.MapFrom(v => v.Model.Make));

            CreateMap<Vehicle, SaveVehicleViewModel>()
                .ForMember(svvm => svvm.Contact, opt => opt.MapFrom(v => new ContactViewModel { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(svvm => svvm.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            CreateMap<VehicleFeature, KeyValuePairViewModel>()
                .ReverseMap();




            //api resource to domain

            CreateMap<VehicleQueryViewModel, VehicleQuery>()
                .ReverseMap();

            CreateMap<SaveVehicleViewModel, Vehicle>()
                //.ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vvm => vvm.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vvm => vvm.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vvm => vvm.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vvm, v) => {
                    //remove unselected features
                    var removedFeatures = v.Features.Where(f => !vvm.Features.Contains(f.FeatureId)).ToList();
                    foreach(var f in removedFeatures) 
                    {
                        v.Features.Remove(f);
                    }


                    //add new features
                   var addedFeatures = vvm.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id}).ToList();
                    foreach(var f in addedFeatures) 
                    {
                        v.Features.Add(f);
                    }
                });

            
        }
    }
}
