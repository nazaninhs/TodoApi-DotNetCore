using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly AppDbContext _context;

    public MessageController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return Ok(message);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Messages.ToListAsync());
    }
}