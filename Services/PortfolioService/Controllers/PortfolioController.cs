using Microsoft.AspNetCore.Mvc;
using PortfolioService.Models;
using PortfolioService.Services;

namespace PortfolioService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _service;

        public PortfolioController(IPortfolioService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var portfolio = await _service.GetByIdAsync(id);
            if (portfolio == null) return NotFound();
            return Ok(portfolio);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Portfolio portfolio)
        {
            await _service.AddAsync(portfolio);
            return CreatedAtAction(nameof(Get), new { id = portfolio.Id }, portfolio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Portfolio portfolio)
        {
            if (id != portfolio.Id) return BadRequest();
            await _service.UpdateAsync(portfolio);
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
