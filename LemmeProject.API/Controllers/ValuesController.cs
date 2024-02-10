using Microsoft.AspNetCore.Mvc;

namespace LemmeProject.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerFeedbackController : ControllerBase
    {
        private static int satisfiedCount = 0;
        private static int dissatisfiedCount = 0;

        [HttpPost]
        [Route("like")]
        public IActionResult Like()
        {
            satisfiedCount++;
            return Ok(new { message = "Thanks for your feedback!" });
        }

        [HttpPost]
        [Route("dislike")]
        public IActionResult Dislike()
        {
            dissatisfiedCount++;
            return Ok(new { message = "Thanks for your feedback!" });
        }

        [HttpGet]
        [Route("customer-satisfaction")]
        public IActionResult GetCustomerSatisfaction()
        {
            return Ok(new
            {
                satisfied_customers = satisfiedCount,
                dissatisfied_customers = dissatisfiedCount
            });
        }
    }
}
