using Microsoft.AspNetCore.Mvc;
using PriceService.Models;
using PriceService.Services;

namespace PriceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly IPriceService _service;

        public PriceController(IPriceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var price = await _service.GetByIdAsync(id);
            if (price == null) return NotFound();
            return Ok(price);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Price price)
        {
            await _service.AddAsync(price);
            return CreatedAtAction(nameof(Get), new { id = price.Id }, price);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Price price)
        {
            if (id != price.Id) return BadRequest();
            await _service.UpdateAsync(price);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
