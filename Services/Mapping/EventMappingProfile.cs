using AutoMapper;
using Entities.Concrete;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mapping
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Event, EventResponseDto>();
            CreateMap<EventCreateDto, Event>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.AvailableSeats, opt => opt.MapFrom(src => src.Capacity));

            CreateMap<EventUpdateDto, Event>();
        }
    }
}
