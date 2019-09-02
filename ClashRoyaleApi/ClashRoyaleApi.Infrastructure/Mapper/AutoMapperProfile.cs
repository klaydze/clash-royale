using AutoMapper;
using ClashRoyaleApi.Core.Entities;
using ClashRoyaleApi.Infrastructure.Extensions;
using ClashRoyaleApi.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ArenaEntity, Arena>();
            CreateMap<ChestEntity, Chest>();
            CreateMap<CardEntity, Card>()
                // .ForMember(dest => dest.Arena, opt => opt.MapFrom(src => src.Arena));
            // .ForMember(dest => dest.Arena, opt => opt.MapFrom(src => src.Arena))
            //.ForMember(dest => dest.Arena, opt => opt.PreCondition(src => src.Arena != null))
            //.ForMember(dest => dest.Arena, opt => opt.MapFrom(src => src.Arena));
            //.ForAllOtherMembers(opt => opt.IgnoreSourceWhenDefault());
            .ForMember(dest => dest.Arena, opt => opt.Ignore());
        }
    }
}
