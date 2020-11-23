using Microsoft.AspNetCore.Mvc;
using TourCmdAPI.TokenAuthentication;

namespace TourCmdAPI.Controllers
{
    //look at other videos explaning filter pipeline MVC invokation pipeline
    //Use dependency injection to inject ITokenManager interface
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ITokenManager tokenManager;
        public AuthenticateController(ITokenManager tokenManager)
        {
            this.tokenManager = tokenManager;

        }
        //Modal binding needs to match fro uri params into controller authenticate params
        public IActionResult Authenticate(string user, string pwd)
        {
            if (tokenManager.Authenticate(user, pwd))
            {
                return Ok(new { Token = tokenManager.NewToken() });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "Not autorized.");
                return Unauthorized(ModelState);
            }
        }
    }
}