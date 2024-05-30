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
        public IActionResult GetPersoninfo([FromQuery] GetPersoninfoQueryModel query)
        {
            var data = _PersoninfoService.GetPersoninfo(query);

            if (data == null || !data.Any())
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("AddNewPersoninfo")]
        public IActionResult AddNewPersoninfo([FromBody] AddNewPersoninfoQueryModel query)
        {
            var result = _PersoninfoService.AddNewPersoninfo(query);

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
        public IActionResult DeletePersoninfo([FromBody] DeletePersoninfoQueryModel query)
        {
            var result = _PersoninfoService.DeletePersoninfo(query);

            if (result.Cmd == 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("UpdatePersoninfo")]
        public IActionResult UpdatePersoninfo([FromBody] UpdatePersoninfoQueryModel query)
        {
            var result = _PersoninfoService.UpdatePersoninfo(query);

            if (result.Cmd == 0)
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
