using Entities.Concrete;
using Services.DTOs.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstract
{
    public interface IEventService
    {
        Task<List<EventResponseDto>> GetAllAsync();
        Task<EventResponseDto?> GetByIdAsync(Guid id);
        Task<EventResponseDto> CreateAsync(EventCreateDto dto);
        Task<EventResponseDto?> UpdateAsync(EventUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
