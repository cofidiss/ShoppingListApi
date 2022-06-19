using Microsoft.AspNetCore.Mvc;

namespace ShoppingListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Errors : ControllerBase
    {

  
        [HttpGetAttribute("/error")]
        public IActionResult Error([FromServices] IHostEnvironment hostEnvironment)
        {
            return Content("sa");
        }
    }
}
