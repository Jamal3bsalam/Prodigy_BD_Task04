using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task02.Core.Entities;

namespace Task02.Core.Service.Contracts
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user, UserManager<User> userManager);
    }
}
