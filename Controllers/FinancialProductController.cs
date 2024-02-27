using Microsoft.AspNetCore.Mvc;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.DTO;
using XPTechnicalInterview.Exceptions;
using XPTechnicalInterview.Services;

namespace XPTechnicalInterview.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinancialProductController : ControllerBase
    {
        private readonly FinancialProductService financialProductService;
        public FinancialProductController(FinancialProductService _FinancialProductService)
        {
            financialProductService = _FinancialProductService;
        }

        // GET: api/FinancialProducts
        [HttpGet]
        public IActionResult GetFinancialProducts()
        {
            var FinancialProducts = financialProductService.GetFinancialProducts();
            return Ok(FinancialProducts); //200
        }
        
        // GET: api/FinancialProducts/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // Expected response type for success
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Expected response for not found
        public ActionResult<FinancialProduct> GetFinancialProductById(int id)
        {
            var FinancialProduct = financialProductService.GetFinancialProductById(id);
            if (FinancialProduct == null)
            {
                return NotFound(); //404
            }
            return Ok(FinancialProduct); //200
        }

        // GET: api/FinancialProducts/days/{days}
        [HttpGet("days/{days}")]
        [ProducesResponseType(typeof(List<FinancialProduct>), StatusCodes.Status200OK)] // Expected response type for success
        public ActionResult<List<FinancialProduct>> GetProductsNearExpiration(int days)
        {
            try
            {
                var FinancialProduct = financialProductService.GetProductsNearExpiration(days);
                if (FinancialProduct == null)
                {
                    return NotFound(); //404
                }
                return Ok(FinancialProduct); //200
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message); // Set 404 status code
            }
        }

        // POST: api/FinancialProducts
        [HttpPost]
        [Consumes("application/json")] // Specify the consumed content type
        [ProducesResponseType(typeof(FinancialProduct), StatusCodes.Status200OK)] // Expected response type for success
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Expected response for bad request
        public ActionResult<FinancialProduct> CreateFinancialProduct([FromBody] FinancialProductDTO financialProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400
            }

            var createdFinancialProduct = financialProductService.CreateFinancialProduct(financialProductDto);
            return CreatedAtRoute(new { id = createdFinancialProduct.FinancialProductId }, createdFinancialProduct); //201
        }

        // PUT: api/FinancialProducts
        [HttpPut]
        [Consumes("application/json")] // Specify the consumed content type
        [ProducesResponseType(typeof(FinancialProduct), StatusCodes.Status200OK)] // Expected response type for success
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Expected response for bad request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Expected response for bad request
        public ActionResult<FinancialProduct> UpdateFinancialProduct([FromBody] FinancialProduct FinancialProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); //400
                }

                var updated = financialProductService.UpdateFinancialProduct(FinancialProduct);
                return Ok(updated); //200
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message); // Set 404 status code
            }
        }

        // DELETE: api/FinancialProducts/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // Expected response type for success
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Expected response for bad request
        public IActionResult DeleteFinancialProduct(int id)
        {
            try
            {
                financialProductService.DeleteFinancialProduct(id);
                return Ok(); //200
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message); // Set 404 status code
            }
        }
    }
}
