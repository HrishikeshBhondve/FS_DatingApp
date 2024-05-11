using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]   //api/user(UserController)
public class UserController : ControllerBase
{
    private readonly DataContext _context;

    public UserController(DataContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        //all tables/db inside context
        var users = await _context.Users.ToListAsync();
        return users;
    }
    [HttpGet("{id}")]   //api/user/2
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        //all tables/db inside context
        return await _context.Users.FindAsync(id);
    }
}
