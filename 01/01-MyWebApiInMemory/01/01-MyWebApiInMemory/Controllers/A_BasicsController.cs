using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _01_MyWebApiInMemory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class A_BasicsController : ControllerBase
    {
        [HttpGet("hello")]
        public IActionResult GetHello()
        {
            return Ok("Hello World!"); 
        }

        [HttpGet("time")]
        public IActionResult GetCurrentTime()
        {
            return Ok($"The current server time is: {DateTime.Now}");
        }

        [HttpGet("number")]
        public IActionResult GetNumber()
        {
            return Ok(42); // Just a static number for example
        }
    }
}
