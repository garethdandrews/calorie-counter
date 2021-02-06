using System.Linq;
using AutoMapper;
using backend_api.Controllers.Resources.DiaryEntryResources;
using backend_api.Controllers.Resources.FoodItemResources;
using backend_api.Controllers.Resources.TokenResources;
using backend_api.Controllers.Resources.UserResources;
using backend_api.Domain.Models;
using backend_api.Domain.Security.Tokens;

namespace backend_api.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>();
            CreateMap<DiaryEntry, DiaryEntryResource>();
            CreateMap<FoodItem, FoodItemResource>();

            CreateMap<AccessToken, AccessTokenResource>()
                .ForMember(a => a.AccessToken, opt => opt.MapFrom(a => a.Token))
                .ForMember(a => a.RefreshToken, opt => opt.MapFrom(a => a.RefreshToken.Token))
                .ForMember(a => a.Expiration, opt => opt.MapFrom(a => a.Expiration));
        }
    }
}