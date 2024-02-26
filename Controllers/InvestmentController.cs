using Microsoft.AspNetCore.Mvc;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Interfaces;
using XPTechnicalInterview.Repositories;
using XPTechnicalInterview.Services;

namespace XPTechnicalInterview.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvestmentController : ControllerBase
    {
        private readonly InvestmentService _InvestmentService;
        private readonly InvestmentRepository _InvestmentRepository;

        public InvestmentController(InvestmentService InvestmentService, InvestmentRepository InvestmentRepository)
        {
            _InvestmentService = InvestmentService;
            _InvestmentRepository = InvestmentRepository;
        }
        [HttpPost]
        [Route("buyInvestment")]
        public IActionResult buyInvestment(BuyOrder buyOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns a 400 status code with validation errors
            }

            var createdInvestment = _InvestmentService.handleBuyOrder(buyOrder);
            return CreatedAtRoute(new { id = createdInvestment.Id }, createdInvestment);
        }

        [HttpPost]
        [Route("sellInvestment")]
        public IActionResult sellInvestment(SellOrder sellOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns a 400 status code with validation errors
            }

            var createdInvestment = _InvestmentService.handleSellOrder(sellOrder);
            return Ok(createdInvestment);
        }

        [HttpGet("{id}")]
        public IActionResult GetInvestmentById(int id)
        {
            var investment = _InvestmentRepository.GetById(id);
            if (investment == null)
            {
                return NotFound(); // Returns a 404 status code if client not found
            }
            return Ok(investment); // Returns a 200 status code with the specific client
        }

        [HttpGet]
        [Route("getInvestmentsByClientId")]
        public IActionResult getInvestmentsByClientId(long clientId)
        {
            return Ok(_InvestmentRepository.GetByClientId(clientId));
        }

        [HttpGet]
        [Route("getActiveInvestmentsByClientId")]
        public IActionResult getActiveInvestmentsByClientId(long clientId)
        {
            return Ok(_InvestmentRepository.GetActiveByClientId(clientId));
        }

        [HttpGet]
        [Route("getInvestmentsByProductId")]
        public IActionResult getInvestmentsByProductId(long productId)
        {
            return Ok(_InvestmentRepository.GetByProductId(productId));
        }
    }
}
