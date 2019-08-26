using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SwaggerTemplate.Models;

namespace SwaggerTemplate.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TodoItemController : Controller
    {
        // In-memory TodoList
        private static readonly Dictionary<int, Todo> TodoStore = new Dictionary<int, Todo>();

        public TodoItemController()
        {
            // Pre-populate with sample data
            if (TodoStore.Count == 0)
            {
                TodoStore.Add(1, new Todo() { Id = 1,  Title = "Pick up groceries" });
                TodoStore.Add(2, new Todo() { Id = 2,Title = "Finish invoice report" });
            }
        }

        // GET: api/values
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Todo>), 200)]
        public IEnumerable<Todo> Get()
        {
            return TodoStore.Values;
        }

        // GET: api/values
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="id">The item id.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(typeof(Todo), 200)]
        public Todo Get(int id)
        {
            return TodoStore.Values.FirstOrDefault(t => t.Id == id);
        }

        // POST api/values
        /// <summary>
        /// Creates new item.
        /// </summary>
        /// <param name="todo">The item.</param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>   
        [HttpPost]
        [ProducesResponseType(typeof(Todo), 201)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Todo todo)
        {
            var id = TodoStore.Values.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            var todonew = new Todo() { Id = id};
            TodoStore.Add(id, todonew);

            return Ok(todo);
        }

        // PUT api/values
        /// <summary>
        /// Updates the item.
        /// </summary>
        /// <param name="id">The item id.</param>
        /// <param name="todo">The item</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Todo), 200)]
        public IActionResult Put(int id, [FromBody] Todo todo)
        {
            if (id != todo.Id)
            {
                return NotFound();
            }

            if (TodoStore.Values.FirstOrDefault(x => x.Id == id) == null)
            {
                return NotFound();
            }

            TodoStore.Remove(id);
            TodoStore.Add(id, todo);

            return Ok(todo);
        }

        // PATCH api/values
        /// <summary>
        /// Updates the item.
        /// </summary>
        /// <param name="id">The item id.</param>
        /// <param name="todo">The item</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(Todo), 200)]
        public IActionResult Patch(int id, [FromBody] Todo todo)
        {
            if (id != todo.Id)
            {
                return NotFound();
            }

            if (TodoStore.Values.FirstOrDefault(x => x.Id == id) == null)
            {
                return NotFound();
            }

            TodoStore.Remove(id);
            TodoStore.Add(id, todo);

            return Ok(todo);
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="id">The item id.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public void Delete(int id)
        {
            TodoStore.Remove(id);
        }
    }
}