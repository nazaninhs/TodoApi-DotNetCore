using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly AppDbContext _context;

    public TodoController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/todo
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var todos = await _context.TodoItems.ToListAsync();
        return Ok(todos);
    }

    // GET: api/todo/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var todo = await _context.TodoItems.FindAsync(id);

        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    // POST: api/todo
    [HttpPost]
    public async Task<IActionResult> Create(TodoItem todo)
    {
        _context.TodoItems.Add(todo);
        await _context.SaveChangesAsync();

        return Ok(todo);
    }

    // PUT: api/todo/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TodoItem todo)
    {
        var existing = await _context.TodoItems.FindAsync(id);

        if (existing == null)
            return NotFound();

        existing.Title = todo.Title;
        existing.IsComplete = todo.IsComplete;

        await _context.SaveChangesAsync();

        return Ok(existing);
    }

    // DELETE: api/todo/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var todo = await _context.TodoItems.FindAsync(id);

        if (todo == null)
            return NotFound();

        _context.TodoItems.Remove(todo);
        await _context.SaveChangesAsync();

        return Ok();
    }
}