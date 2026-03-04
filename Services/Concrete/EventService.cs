using AutoMapper;
using Entities.Concrete;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Abstract;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Concrete
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context, IMapper mapper, IValidationService validationService)
        {
            _context = context;
            _validationService = validationService;
            _mapper = mapper;
        }
        public async Task<List<EventResponseDto>> GetAllAsync()
        {
            var data = await _context.Events.ToListAsync();
            var responseDto = _mapper.Map<List<EventResponseDto>>(data);
            return responseDto;
        }
        public async Task<EventResponseDto?> GetByIdAsync(Guid id)
        {
            var data =  await _context.Events.FindAsync(id);
            var responseDto = _mapper.Map<EventResponseDto>(data);
            return responseDto;
        }
        public async Task<EventResponseDto> CreateAsync(EventCreateDto dto)
        {
            await _validationService.ValidateAsync(dto); //reflection ile func başında otomatik yaptırabiliriz.
            var entity = _mapper.Map<Event>(dto);
            await _context.Events.AddAsync(entity);
            await _context.SaveChangesAsync();
            var responseDto = _mapper.Map<EventResponseDto>(entity);
            return responseDto;
        }
        public async Task<EventResponseDto?> UpdateAsync(EventUpdateDto dto)
        {
            await _validationService.ValidateAsync(dto);
            var existing = await _context.Events.FindAsync(dto.Id);
            if(existing == null)
            {
                return null;
            }
            _mapper.Map(dto, existing);
            await _context.SaveChangesAsync();
            var responseDto = _mapper.Map<EventResponseDto>(existing);
            return responseDto;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _context.Events.FindAsync(id);
            if (existing == null)
                return false;

            _context.Events.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}
