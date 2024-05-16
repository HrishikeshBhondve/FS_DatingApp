using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AccountController : BaseAPIController
{
    private readonly DataContext _context;
    private readonly object _userManager;

    public AccountController(DataContext context)
    {
        _context = context;
    }
    [HttpPost("register")]  //post- http://localhost:9090/api/account
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username))
        {
            return BadRequest("UserName is already taken");
        }
        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
    [HttpPost("Login")]
    public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
        if (user == null) return Unauthorized();
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
        for (int i = 0; i < ComputedHash.Length; i++)
        {
            if (ComputedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }
        return user;
    }
    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username.ToLower()); //if UN exist in DB = True
    }
}
