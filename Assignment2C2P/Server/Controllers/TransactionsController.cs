using Assignment2C2P.Business.Manager.Interface;
using Assignment2C2P.Shared;
using Assignment2C2P.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace Assignment2C2P.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionManager _manager;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ITransactionManager manager, ILogger<TransactionsController> logger)
        {
            _manager = manager;
            _logger = logger;
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

            string extension = Path.GetExtension(file.FileName).ToLower();
            string[] allowedExtension = { ".csv", ".xml" };

            if (!allowedExtension.Contains(extension))
            {
                return BadRequest("File extension");
            }

            try
            {
                _manager.ImportTransactions(file.OpenReadStream(), extension);
            }
            catch (TransactionValidateErrorException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (UnKnowFormatException)
            {
                return BadRequest("Unknown format");
            }

            return Ok();
        }
    }
}
