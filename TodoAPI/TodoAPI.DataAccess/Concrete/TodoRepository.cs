using TodoAPI.DataAccess.Abstract;
using TodoAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAPI.DataAccess.Concrete
{
    public class TodoRepository : EFRepository<Todo>, ITodoRepository
    {
        public TodoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
