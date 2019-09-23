using AutoMapper;
using ClashRoyaleApi.Core.Entities;
using ClashRoyaleApi.Infrastructure.Models;

namespace ClashRoyaleApi.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ArenaEntity, Arena>();
            CreateMap<ChestEntity, Chest>();
            CreateMap<CardStatisticsEntity, CardStatistics>();
            CreateMap<CardEntity, Card>()
            //.ForMember(dest => dest.Arena, opt => opt.MapFrom(src => src.Arena));
            //.ForAllOtherMembers(opt => opt.IgnoreSourceWhenDefault());
            .ForMember(dest => dest.CardStatistics, opt => opt.MapFrom(src => src.CardStatistics))
            .ForMember(dest => dest.Arena, opt => opt.MapFrom(src => src.Arena));
            //.ForMember(dest => dest.Arena, opt => opt.Ignore());
        }
    }
}
