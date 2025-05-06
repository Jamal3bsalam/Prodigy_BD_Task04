using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task02.Core.Entities;

namespace Task02.Repository
{
    public class UserDbContextSeed
    {
        public async static Task SeedAppUser(UserManager<User> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new User()
                {
                    Email = "gamalwork81@gmail.com",
                    Name = "Jamal Abdelsalam Mohamed",
                    Age =  22,
                    UserName = "Jamal_11",
                    PhoneNumber = "123456789",
                    
                };
                await _userManager.CreateAsync(user, "Jamal@123");
                await _userManager.AddToRoleAsync(user, "Admin");
            }

        }
    }
}
