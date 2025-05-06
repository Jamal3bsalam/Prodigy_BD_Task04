using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task02.Core.Dtos;
using Task02.Core.Entities;

namespace Task02.Core.Service.Contracts
{
    public interface IUserService
    {
        public Task<UserDto> CreateUser(CreateUserDto userDto,Roles roles);
        Task<UserDto> LogInAsync(LoginDto loginDto);

        public Task<CurrentUserDto> GetCurrentUser();   
        public Task<IEnumerable<CurrentUserDto>> GetAllUsers();
        public Task<CurrentUserDto> UpdateUser(UpdateUserDto userDto);
        public Task<string> DeleteUser();
        
    }
}
