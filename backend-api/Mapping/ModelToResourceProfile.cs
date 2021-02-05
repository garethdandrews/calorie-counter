using AutoMapper;
using backend_api.Controllers.DiaryEntryResources;
using backend_api.Controllers.FoodItemResources;
using backend_api.Controllers.UserResources;
using backend_api.Domain.Models;

namespace backend_api.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>();
            CreateMap<DiaryEntry, DiaryEntryResource>();
            CreateMap<FoodItem, FoodItemResource>();
        }
    }
}