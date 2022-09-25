using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAPI.Entities;

namespace TodoAPI.DataAccess
{
    public static class AppDbInitializer
    {
        public static void SeedUsers(UserManager<AppUser> userManager)
        {
            if (userManager.FindByNameAsync("Jane").Result == null)
            {
                AppUser user = new AppUser
                {
                    UserName = "Jane",
                };
                IdentityResult result = userManager.CreateAsync(user, "Jane123*").Result;
            }
            if (userManager.FindByNameAsync("Joe").Result == null)
            {
                AppUser user = new AppUser
                {
                    UserName = "Joe",
                };
                IdentityResult result = userManager.CreateAsync(user, "Joe123*").Result;
            }
        }
    }
}
