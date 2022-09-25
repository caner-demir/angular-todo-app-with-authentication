using TodoAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAPI.DataAccess.Abstract
{
    public interface ITodoRepository : IRepository<Todo>
    {
    }
}
