using Microsoft.AspNetCore.Mvc;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Interfaces;
using XPTechnicalInterview.Repositories;
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
        public IActionResult GetFinancialProductById(int id)
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
        public IActionResult GetProductsNearExpiration(int days)
        {
            var FinancialProduct = financialProductService.GetProductsNearExpiration(days);
            if (FinancialProduct == null)
            {
                return NotFound(); //404
            }
            return Ok(FinancialProduct); //200
        }

        // POST: api/FinancialProducts
        [HttpPost]
        public IActionResult CreateFinancialProduct([FromBody] FinancialProduct FinancialProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400
            }

            var createdFinancialProduct = financialProductService.CreateFinancialProduct(FinancialProduct);
            return CreatedAtRoute(new { id = createdFinancialProduct.FinancialProductId }, createdFinancialProduct); //201
        }

        // PUT: api/FinancialProducts
        [HttpPut]
        public IActionResult UpdateFinancialProduct([FromBody] FinancialProduct FinancialProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400
            }

            var updated = financialProductService.UpdateFinancialProduct(FinancialProduct);
            return Ok(updated); //200
        }

        // DELETE: api/FinancialProducts/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteFinancialProduct(int id)
        {
            financialProductService.DeleteFinancialProduct(id);
            return Ok(); //200
        }
    }
}
