﻿using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using XPTechnicalInterview.Domain;
using XPTechnicalInterview.Exceptions;
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

        [HttpPost("buyInvestment")]
        [Consumes("application/json")] // Specify the consumed content type
        [ProducesResponseType(typeof(Investment), StatusCodes.Status200OK)] // Expected response type for success
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Expected response for bad request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Expected response for not found
        public ActionResult<Investment> BuyInvestment(BuyOrder buyOrder)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); //400
                }
                var createdInvestment = investmentService.handleBuyOrder(buyOrder);
                return CreatedAtRoute(new { id = createdInvestment.Id }, createdInvestment);//201
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message); //404
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("sellInvestment")]
        [ProducesResponseType(typeof(Investment), StatusCodes.Status200OK)] // Expected response type for success
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Expected response for bad request
        public ActionResult<Investment> SellInvestment(SellOrder sellOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400
            }

            var createdInvestment = investmentService.handleSellOrder(sellOrder);
            return Ok(createdInvestment);//200
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Investment), StatusCodes.Status200OK)] // Expected response type for success
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Expected response for not found
        public ActionResult<Investment> GetInvestmentById(int id)
        {
            try
            {
                var investment = investmentService.GetInvestmentById(id);
                if (investment == null)
                {
                    return NotFound(); //404
                }
                return Ok(investment); //200
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message); //404
            }
        }

        [HttpGet]
        [Route("getInvestmentsByClientId")]
        [ProducesResponseType(typeof(List<Investment>), StatusCodes.Status200OK)] // Expected response type for success
        public ActionResult<List<Investment>> GetInvestmentsByClientId(long clientId)
        {
            return Ok(investmentService.GetInvestmentsByClientId(clientId));//200
        }

        [HttpGet]
        [Route("getActiveInvestmentsByClientId")]
        [ProducesResponseType(typeof(List<Investment>), StatusCodes.Status200OK)] // Expected response type for success
        public ActionResult<List<Investment>> GetActiveInvestmentsByClientId(long clientId)
        {
            return Ok(investmentService.GetActiveInvestmentsByClientId(clientId));//200
        }

        [HttpGet]
        [Route("getSoldInvestmentsByClientId")]
        [ProducesResponseType(typeof(List<Investment>), StatusCodes.Status200OK)] // Expected response type for success
        public ActionResult<List<Investment>> GetSoldInvestmentsByClientId(long clientId)
        {
            return Ok(investmentService.GetSoldInvestmentsByClientId(clientId));//200
        }
        
        [HttpGet]
        [Route("getInvestmentsByProductId")]
        [ProducesResponseType(typeof(List<Investment>), StatusCodes.Status200OK)] // Expected response type for success
        public ActionResult<List<Investment>> GetInvestmentsByProductId(long productId)
        {
            return Ok(investmentService.GetInvestmentsByProductId(productId));//200
        }

        [HttpGet]
        [Route("getActiveInvestmentsByProductId")]
        [ProducesResponseType(typeof(List<Investment>), StatusCodes.Status200OK)] // Expected response type for success
        public ActionResult<List<Investment>> GetActiveInvestmentsByProductId(long productId)
        {
            return Ok(investmentService.GetActiveInvestmentsByProductId(productId));//200
        }
    }
}
