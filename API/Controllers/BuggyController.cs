using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseAPIController
{
    public readonly DataContext _context;
    public BuggyController(DataContext context)
    {
        _context = context;
    }
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetSecret()
    {
        return "Secret Text";     //401
    }
    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        var thing = _context.Users.Find(-1);   //404
        if (thing == null)
        {
            return NotFound();
        }
        return thing;
    }
    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
        var thing = _context.Users.Find(-1);
        var thingToReturn = thing.ToString(); //nullexception might occur
        return thingToReturn;

    }
    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequestError()
    {
        return BadRequest("This was Bad Requst");   //400 error
    }
}
