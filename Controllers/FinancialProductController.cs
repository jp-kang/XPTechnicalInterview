using Microsoft.AspNetCore.Mvc;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Interfaces;
using XPTechnicalInterview.Repositories;

namespace XPTechnicalInterview.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinancialProductController : ControllerBase
    {
        private readonly FinancialProductRepository _FinancialProductRepository;
        public FinancialProductController(FinancialProductRepository FinancialProductRepository)
        {
            _FinancialProductRepository = FinancialProductRepository;
        }

        // GET: api/FinancialProducts
        [HttpGet]
        public IActionResult GetFinancialProducts()
        {
            var FinancialProducts = _FinancialProductRepository.ListAll();
            return Ok(FinancialProducts); //200
        }

        // GET: api/FinancialProducts/{id}
        [HttpGet("{id}")]
        public IActionResult GetFinancialProductById(int id)
        {
            var FinancialProduct = _FinancialProductRepository.GetById(id);
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

            var createdFinancialProduct = _FinancialProductRepository.Create(FinancialProduct);
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

            var updated = _FinancialProductRepository.Update(FinancialProduct);
            return Ok(updated); //200
        }

        // DELETE: api/FinancialProducts/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteFinancialProduct(int id)
        {
            _FinancialProductRepository.Delete(id);
            return Ok(); //200
        }
    }
}
