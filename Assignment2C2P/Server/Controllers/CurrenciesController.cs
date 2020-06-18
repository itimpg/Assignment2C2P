using Assignment2C2P.Business.Manager.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2C2P.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private ICurrencyManager _manager;

        public CurrenciesController(ICurrencyManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var currencies = _manager.GetCurrencies();
            return Ok(currencies);
        }
    }
}
