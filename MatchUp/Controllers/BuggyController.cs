using MatchUp.Data;
using MatchUp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MatchUp.Controllers
{
    public class BuggyController(DataContext context) : BaseController
    {
        [HttpGet("server-error")]
        public ActionResult<AppUser> GetServerError()
        {
            var thing = context.Users.Find(-1) ?? throw new Exception("A bad thing has happened");
            return thing;
        }

        [HttpGet("bad-request")]
        public ActionResult<AppUser> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
    }
}
