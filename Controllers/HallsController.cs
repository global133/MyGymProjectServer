using Microsoft.AspNetCore.Mvc;
using MyGymProject.Server.DTOs.Hall;
using MyGymProject.Server.Services.Interfaces;


namespace MyGymProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallsController : ControllerBase
    {
        private readonly IHallService _hallService;

        public HallsController(IHallService hallService)
        {
            _hallService = hallService;
        }

        // GET: api/Halls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HallDtoResponse>>> GetAll()
        {
            var halls = await _hallService.GetAllHalls();
            return Ok(halls);
        }

        // GET: api/Halls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HallDtoResponse>> GetById(int id)
        {
            var hall = await _hallService.GetHall(id);
            if (hall == null)
                return NotFound();

            return Ok(hall);
        }

        // POST: api/Halls
        [HttpPost]
        public async Task<ActionResult<HallDtoResponse>> Create([FromBody] HallCreateDto dto)
        {
            try
            {
                var created = await _hallService.CreateHall(dto);
                return CreatedAtAction(nameof(GetById), created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Halls/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HallCreateDto dto)
        {
            try
            {
                var updated = await _hallService.UpdateHall(id, dto);
                if (!updated)
                    return NotFound();

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Halls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _hallService.DeleteHall(id);
                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
