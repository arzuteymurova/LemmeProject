using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LemmeProject.API.Controllers.SkinType
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinTypeController : ControllerBase
    {
        [HttpGet("DetermineSkinType/{countsOfABC}")]
        public IActionResult DetermineSkinType(int countsOfABC)
        {
            int countOfA = countsOfABC.ToString()[0];
            int countOfB = countsOfABC.ToString()[1];
            int countOfC = countsOfABC.ToString()[2];

            // Determine the skin type based on the counts
            if (countOfA > countOfB && countOfA > countOfC)
            {
                var type = new { Type = "Quru" };
                return Ok(type);
            }
            else if (countOfB > countOfA && countOfB > countOfC)
            {
                var type = new { Type = "Yağlı" };
                return Ok(type);
            }
            else
            {
                var type = new { Type = "Karma" };
                return Ok(type);
            }

        }
    }
}
