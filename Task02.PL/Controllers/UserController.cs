using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task02.Core.Dtos;
using Task02.Core.Entities;
using Task02.Core.Service.Contracts;
using Task02.PL.Attributes;

namespace Task02.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LogIn(LoginDto loginDto)
        {
            var user = await _userService.LogInAsync(loginDto);
            if (user == null) return BadRequest("Invalid Operationn");
            return Ok(user);
        }


        [HttpPost("registraion")]
        public async Task<ActionResult<UserDto>> Register(CreateUserDto userDto,Roles roles)
        {
            if (userDto == null) return BadRequest("Invalid Operation");
            var user = await _userService.CreateUser(userDto,roles);
            if (user == null) return BadRequest("Invalid Operation");
            return Ok(user);
        }

        [HttpPut("updateUser")]
        [Authorize(AuthenticationSchemes = "Bearer",Roles = "User")]
        public async Task<ActionResult<UserDto>> UpdateUser(UpdateUserDto userDto)
        {
            if (userDto == null) return BadRequest("Invalid Operation");
            var user = await _userService.UpdateUser(userDto);
            if (user == null) return BadRequest("Invalid Operation");
            return Ok(user);
        }

        [HttpDelete("deleteUser")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<ActionResult<string>>DeleteUser()
        {
            var result =  await _userService.DeleteUser();
            if (result == null) return NotFound();  
            return Ok(result);
        }

        [HttpGet("allUsers")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Employee,Admin")]
        [Cached(100)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users == null) return BadRequest();
            return Ok(users);
        }

        [HttpGet("currentUser")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserById()
        {
            var user = await _userService.GetCurrentUser();
            if (user == null) return BadRequest();
            return Ok(user);
        }
    }
}
