using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LemmeProject.API.Controllers.SkinType
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinTypeController : ControllerBase
    {
        [HttpGet("DetermineSkinType")]
        public string DetermineSkinType(int countOfA, int countOfB, int countOfC)
        {
            // Determine the skin type based on the counts
            if (countOfA > countOfB && countOfA > countOfC)
            {
                return "Quru";
            }
            else if (countOfB > countOfA && countOfB > countOfC)
            {
                return "Yağlı";
            }
            else
            {
                return "Karma";
            }
            
        }
    }
}
