using AutoMapper;
using backend_api.Domain.Models;
using backend_api.Resources.FoodItem;

namespace backend_api.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveFoodItemResource, FoodItem>();
        }
    }    
}