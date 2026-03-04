using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using Services.DTOs.Event;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _eventService.GetAllAsync();
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var data = await _eventService.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]EventCreateDto dto)
        {
            var createdData = await _eventService.CreateAsync(dto);
            if (createdData == null) BadRequest();
            return CreatedAtAction(nameof(Get), new { id = createdData!.Id }, createdData);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id,[FromBody]EventUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var updatedData = await _eventService.UpdateAsync(dto);
            if (updatedData == null) return NotFound();
            return Ok(updatedData);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteResult = await _eventService.DeleteAsync(id);
            if (deleteResult == false) return NotFound();
            return NoContent();
        }
    }
}
