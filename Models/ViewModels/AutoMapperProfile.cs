using AutoMapper;

namespace vega.Models.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Make, MakeViewModel>();
            CreateMap<Model, ModelViewModel>();
        }
    }
}
