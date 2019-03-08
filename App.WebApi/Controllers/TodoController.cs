namespace App.WebApi.Controllers
{
    using System;
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Model;

    public class TodoController : Controller
    {
        private readonly TodoContext context;

        public TodoController(TodoContext context)
        {
            this.context = context;
            if (!context.TodoItems.Any())
            {
                context.TodoItems.Add(new TodoItem { Name = "The API Demo", Description = "This is to demo about APIs in AspNet core."});
                context.SaveChanges();
            }
        }

        #region Get All Todos

        [HttpGet("todos")]
        public IActionResult GetAll()
        {
            try
            {
                return this.Ok(this.context.TodoItems.ToList());
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{id}/todo")]
        public IActionResult GetById(long id)
        {
            try
            {
                var item = this.context.TodoItems.FirstOrDefault(t => t.Id == id);
                if (item == null)
                {
                    return this.NotFound();
                }

                return this.Ok(item);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        #endregion

        #region Create Todo

        [HttpPost("todo")]
        public IActionResult Create([FromBody] TodoItem item)
        {
            try
            {
                if (item == null)
                {
                    return this.BadRequest();
                }

                this.context.TodoItems.Add(item);
                this.context.SaveChanges();

                return this.Ok(item);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        #endregion

        #region Update Todo

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            try
            {
                if (item == null || item.Id != id)
                {
                    return this.BadRequest();
                }

                var todo = this.context.TodoItems.FirstOrDefault(t => t.Id == id);
                if (todo == null)
                {
                    return this.NotFound();
                }

                todo.IsComplete = item.IsComplete;
                todo.Name = item.Name;

                this.context.TodoItems.Update(todo);
                this.context.SaveChanges();
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        #endregion

        #region Delete Todo

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var todo = this.context.TodoItems.FirstOrDefault(t => t.Id == id);
                if (todo == null)
                {
                    return this.NotFound();
                }

                this.context.TodoItems.Remove(todo);
                this.context.SaveChanges();
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        #endregion
    }
}