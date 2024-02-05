using AutoMapper;

namespace vega.Models.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Make, MakeViewModel>();
            CreateMap<MakeViewModel, Make>();
            CreateMap<MakeViewModel, ModelViewModel>();

            CreateMap<Model, ModelViewModel>();
            CreateMap<ModelViewModel, Model>();
            CreateMap<ModelViewModel, MakeViewModel>();
        }
    }
}
