using Inventario.Domain.Entities;
using Inventario.Domain.Interfaces;
using Inventario.Infrastructure.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inventario.Api.Controllers
{
    [EnableCors]
    [Route("api/v1/Transaction")]
    [ApiController]
    public class TransactionController(ITransactionRepository transactionRepository, ITransactionTypeRepository transactionTypeRepository) : Controller
    {
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Route("Add")]
        public IActionResult Add(Transaction transaction)
        {
            var result = transactionRepository.Add(transaction);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Route("Update")]
        public IActionResult Update(Transaction transaction)
        {
            var result = transactionRepository.Update(transaction);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Route("Delete")]
        public IActionResult Delete(long id)
        {
            var result = transactionRepository.Delete(id);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Route("Get")]
        public IActionResult Get(string start, string end, long? transaction = null, string? product = null, int pageNumber = 2, int pageSize = 10)
        {
            var result = transactionRepository.Get(start, end, transaction, product, pageNumber, pageSize);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Route("GetGroupByProduct")]
        public IActionResult GetGroupByProduct(string start, string end, long? transaction = null, int pageNumber = 2, int pageSize = 10)
        {
            var result = transactionRepository.GetGroupByProduct(start, end, transaction, pageNumber, pageSize);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Route("Type/GetAll")]
        public IActionResult CategoryGetAll()
        {
            var result = transactionTypeRepository.GetAll();

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

        }
    }
}
