using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Event entity)
        {
            var createdData = await _eventService.CreateAsync(entity);
            if (createdData == null) BadRequest();
            return CreatedAtAction(nameof(Get), new { id = createdData.Id }, createdData);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id,[FromBody]Event entity)
        {
            if (id != entity.Id) return BadRequest();
            var updatedData = await _eventService.UpdateAsync(entity);
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
