using AutoMapper;
using backend_api.Controllers.Resources.FoodItemResources;
using backend_api.Controllers.Resources.UserResources;
using backend_api.Domain.Models;

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