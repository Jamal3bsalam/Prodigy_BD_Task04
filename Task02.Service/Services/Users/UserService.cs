using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task02.Core;
using Task02.Core.Dtos;
using Task02.Core.Entities;
using Task02.Core.Service.Contracts;

namespace Task02.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContext;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager,ITokenService tokenService,IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _httpContext = httpContext;
        }
        public async Task<UserDto> CreateUser(CreateUserDto createUserDto,Roles roles)
        {
            if (createUserDto == null) return null;
            var user = new User()
            {
                Name = createUserDto.Name,
                UserName = createUserDto.UserName,
                Email = createUserDto.Email,
                Age = createUserDto.Age,

            };
           var result = await _userManager.CreateAsync(user,createUserDto.Password);

            if (!result.Succeeded) return null;
            await _userManager.AddToRoleAsync(user,roles.ToString());
            var userDto = new UserDto()
            {
                Name = user.Name,
                Email = user.Email,
                Age = user.Age,
                Token = await _tokenService.CreateToken(user,_userManager)
            };

            return userDto;
        }

        public async Task<UserDto> LogInAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return null;
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return null;

            var userDto = new UserDto()
            {
                Name = user.Name,
                Email = user.Email,
                Age= user.Age,  
                Token = await _tokenService.CreateToken(user, _userManager)
            };

            return userDto;
        }

        public async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

        public async Task<string> DeleteUser()
        {
            var userId = _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return null;

            var user = await _userManager.FindByIdAsync(userId);

            await _userManager.DeleteAsync(user);

            return "User Deleted Succssefully";
        }

        public async Task<IEnumerable<CurrentUserDto>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users == null) return null;
            var usersDto = users.Select(u => new CurrentUserDto()
            {
                Name = u.Name,
                Email = u.Email,
                Age = u.Age,
            });

            return usersDto;
        }

        public async Task<CurrentUserDto> GetCurrentUser()
        {
            var userId = _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return null;

            var user = await _userManager.FindByIdAsync(userId);

            var userDto = new CurrentUserDto()
            {
                Name = user.Name,
                Email = user.Email,
                Age = user.Age,
            };
            return userDto;
        }

        public async Task<CurrentUserDto> UpdateUser(UpdateUserDto userDto)
        {
            var userId = _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return null;

            var user = await _userManager.FindByIdAsync(userId);

            user.Name = userDto.Name;
            user.UserName = userDto.UserName;
            user.NormalizedUserName = userDto.UserName.ToUpper();
            user.Age = userDto.Age;

            await _userManager.UpdateAsync(user);

            var usersDto = new CurrentUserDto()
            {
                Name = user.Name,
                Email = user.Email,
                Age = user.Age,
            };

            return usersDto;

        }
    }
}
