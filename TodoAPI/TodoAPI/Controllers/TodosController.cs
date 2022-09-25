using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoAPI.DataAccess.Abstract;
using TodoAPI.Entities;

namespace TodoAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoContext;
        private readonly IAppUserRepository _userContext;

        public TodosController(ITodoRepository todoContext, IAppUserRepository userContext)
        {
            _todoContext = todoContext;
            _userContext = userContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var todos = _todoContext.GetAll(t => t.AppUserId == GetUser().Id);
            return Ok(todos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Todo todo)
        {
            todo.AppUserId = GetUser().Id;

            _todoContext.Add(todo);
            return Ok();
        }

        [HttpPatch]
        public IActionResult UpdateStatus([FromBody] Todo todo)
        {
            todo.IsCompleted = !todo.IsCompleted;
            _todoContext.Update(todo);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var todo = _todoContext.Get(id);
            if (todo == null)
            {
                return NotFound();
            }
            _todoContext.Remove(todo);
            return NoContent();
        }

        private AppUser GetUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userName = identity.FindFirst(ClaimTypes.Name).Value;

            // UserName field has index, so there won't be any performance loss running an
            // sql query where this field functions as a filter.
            return _userContext.GetAll(u => u.UserName == userName)[0];
        }
    }
}
