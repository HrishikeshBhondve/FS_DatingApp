using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[Authorize]
public class UserController : BaseAPIController
{
    private readonly DataContext _context;

    public UserController(DataContext context)
    {
        _context = context;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        //all tables/db inside context
        var users = await _context.Users.ToListAsync();
        return users;
    }
    [AllowAnonymous]
    [HttpGet("{id}")]   // http://localhost:9090/api/user/2  
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        //all tables/db inside context
        return await _context.Users.FindAsync(id);
    }
}
