using AutoMapper;
using backend_api.Domain.Models;
using backend_api.Resources.FoodItemResources;
using backend_api.Resources.UserResources;

namespace backend_api.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<AddUserResource, User>();
            CreateMap<UpdateUserResource, User>();
            CreateMap<UpdateFoodItemResource, FoodItem>();
        }
    }    
}