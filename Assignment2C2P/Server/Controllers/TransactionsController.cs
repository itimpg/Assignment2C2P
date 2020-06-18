using Assignment2C2P.Business.Manager.Interface;
using Assignment2C2P.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace Assignment2C2P.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private ITransactionManager _manager;

        public TransactionsController(ITransactionManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult Get(
            string currencyCode,
            string statusCode,
            string dateFrom,
            string dateTo)
        {
            var from = DateHelper.ToDate(dateFrom);
            var to = DateHelper.ToDate(dateTo);

            var result = _manager.SearchTransactions(currencyCode, statusCode, from, to);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Upload a file");
            }

            string extension = Path.GetExtension(file.FileName);
            string[] allowedExtension = { ".csv", ".xml" };

            if (!allowedExtension.Contains(extension))
            {
                return BadRequest("File extension");
            }

            // TODO: read file and insert data here

            return Ok();
        }
    }
}
