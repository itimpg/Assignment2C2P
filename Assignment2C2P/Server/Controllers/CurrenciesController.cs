using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Assignment2C2P.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Get data here

            var result = new List<string> { "USD", "THB", "JPY" };

            return Ok(result);
        }
    }
}
