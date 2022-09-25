using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAPI.Entities
{
    public class AppUser : IdentityUser
    {
        public List<Todo> Todos { get; set; }
    }
}
