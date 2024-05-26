using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactApp.Server.Models.Request;
using ReactApp.Server.Services;

namespace ReactApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersoninfoController : ControllerBase
    {
        private readonly IPersoninfoService _PersoninfoService;
        public PersoninfoController(IPersoninfoService PersoninfoService)
        {
            _PersoninfoService = PersoninfoService;
        }
        [HttpGet("GetPersoninfo")]
        public async Task<IActionResult> GetPersoninfo([FromQuery] GetPersoninfoQueryModel query)
        {
            var data = await _PersoninfoService.GetPersoninfoAsync(query);

            if (data == null || !data.Any())
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("AddNewPersoninfo")]
        public async Task<IActionResult> AddNewPersoninfo([FromBody] AddNewPersoninfoQueryModel query)
        {
            var result = await _PersoninfoService.AddNewPersoninfoAsync(query);

            if (result.Cmd == 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("DeletePersoninfo")]
        public async Task<IActionResult> DeletePersoninfo([FromBody] DeletePersoninfoQueryModel query)
        {
            var result = await _PersoninfoService.DeletePersoninfoAsync(query);

            if (result.Cmd == 0) // 假设 2 表示成功
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
