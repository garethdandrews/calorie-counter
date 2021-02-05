using System.Linq;
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
            CreateMap<User, UserResource>()
                .ForMember(u => u.Roles, opt => opt.MapFrom(u => u.UserRoles.Select(ur => ur.Role.Name)));
            CreateMap<DiaryEntry, DiaryEntryResource>();
            CreateMap<FoodItem, FoodItemResource>();
        }
    }
}