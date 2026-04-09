
using Inventario.Domain.Entities;
using Inventario.Domain.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inventario.Api.Controllers
{
    [EnableCors]
    [Route("api/v1/Product")]
    [ApiController]
    public class ProductController(ICategoryRepository categoryRepository, IProductRepository productRepository) : Controller
    {

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Route("Get")]
        public IActionResult Get(string? name = null, long? category = null, int pageNumber = 2, int pageSize = 10)
        {
            var result = productRepository.Get(name, category, pageNumber, pageSize);

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
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var result = productRepository.GetAll();

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
        [Route("GetById")]
        public IActionResult GetById(Guid id)
        {
            var result = productRepository.GetById(id);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Route("Add")]
        public IActionResult Add(Product product)
        {
            var result = productRepository.Add(product);

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
        public IActionResult Update(Product product)
        {
            var result = productRepository.Update(product);

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
        public IActionResult Delete(Guid id)
        {
            var result = productRepository.Delete(id);

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
        [Route("Category/GetAll")]
        public IActionResult CategoryGetAll()
        {
            var result = categoryRepository.GetAll();

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
