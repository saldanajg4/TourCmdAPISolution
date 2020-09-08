using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace TourCmdAPI.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase{
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(){
            return new string[] {"this","is", "hard", "codadfed"};
        }
    }
}