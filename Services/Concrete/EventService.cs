using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Concrete
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }
        public async Task<Event?> GetByIdAsync(Guid id)
        {
            return await _context.Events.FindAsync(id);
        }
        public async Task<Event> CreateAsync(Event entity)
        {
            await _context.Events.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<Event?> UpdateAsync(Event entity)
        {
            var existing = await _context.Events.FindAsync(entity.Id);
            if(existing == null)
            {
                return null;
            }

            _context.Events.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
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
