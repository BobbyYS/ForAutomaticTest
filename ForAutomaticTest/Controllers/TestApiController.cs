using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ForAutomaticTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestApiController : ControllerBase
    {
        /// <summary>
        /// 新增使用者
        /// </summary>
        [HttpPost("/api/user")]
        public IActionResult Post([FromBody] UserDto userDto)
        {
            return Ok(userDto);
        }

        /// <summary>
        /// 使用者物件
        /// </summary>
        public class UserDto
        {
            [Required] public string Name { get; set; }
            [MinLength(3)] public string Password { get; set; }
        }

        /// <summary>
        /// 取得使用者
        /// </summary>
        [HttpGet("/api/user")]
        public IActionResult Get(string Name)
        {
            UserDto user = new UserDto() { Name= Name, Password = Name+"1111" };
            return new JsonResult(user);
        }
    }
}
