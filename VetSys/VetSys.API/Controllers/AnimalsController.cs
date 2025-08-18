using Microsoft.AspNetCore.Mvc;
using VetSys.Application.Dtos;


namespace VetSys.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalService _service;
        public AnimalsController(AnimalService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalDto>>> GetAll() => Ok(await _service.GetAllAnimalsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalDto>> GetById(int id)
        {
            var animal = await _service.GetAnimalByIdAsync(id);
            if (animal == null) return NotFound();
            return Ok(animal);
        }

        [HttpPost]
        public async Task<ActionResult<AnimalDto>> Create(AnimalDto dto)
        {
            var created = await _service.AddAnimalAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AnimalDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _service.UpdateAnimalAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAnimalAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
