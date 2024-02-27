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
        private readonly InvestmentService investmentService;

        public InvestmentController(InvestmentService _InvestmentService)
        {
            investmentService = _InvestmentService;
        }
        [HttpPost]
        [Route("buyInvestment")]
        public IActionResult BuyInvestment(BuyOrder buyOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400
            }

            var createdInvestment = investmentService.handleBuyOrder(buyOrder);
            return CreatedAtRoute(new { id = createdInvestment.Id }, createdInvestment);//201
        }

        [HttpPost]
        [Route("sellInvestment")]
        public IActionResult SellInvestment(SellOrder sellOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400
            }

            var createdInvestment = investmentService.handleSellOrder(sellOrder);
            return Ok(createdInvestment);//200
        }

        [HttpGet("{id}")]
        public IActionResult GetInvestmentById(int id)
        {
            var investment = investmentService.GetInvestmentById(id);
            if (investment == null)
            {
                return NotFound(); //404
            }
            return Ok(investment); //200
        }

        [HttpGet]
        [Route("getInvestmentsByClientId")]
        public IActionResult GetInvestmentsByClientId(long clientId)
        {
            return Ok(investmentService.GetInvestmentsByClientId(clientId));//200
        }

        [HttpGet]
        [Route("getActiveInvestmentsByClientId")]
        public IActionResult GetActiveInvestmentsByClientId(long clientId)
        {
            return Ok(investmentService.GetActiveInvestmentsByClientId(clientId));//200
        }

        [HttpGet]
        [Route("getSoldInvestmentsByClientId")]
        public IActionResult GetSoldInvestmentsByClientId(long clientId)
        {
            return Ok(investmentService.GetSoldInvestmentsByClientId(clientId));//200
        }

        [HttpGet]
        [Route("getInvestmentsByProductId")]
        public IActionResult GetInvestmentsByProductId(long productId)
        {
            return Ok(investmentService.GetInvestmentsByProductId(productId));//200
        }
    }
}
