using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstract
{
    public interface IEventService
    {
        Task<List<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(Guid id);
        Task<Event> CreateAsync(Event entity);
        Task<Event?> UpdateAsync(Event entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
