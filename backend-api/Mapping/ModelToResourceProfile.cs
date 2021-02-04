using AutoMapper;
using backend_api.Domain.Models;
using backend_api.Resources;

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